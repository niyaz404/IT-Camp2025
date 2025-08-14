using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.BLL.Helpers;
using AuthService.BLL.Models;
using AuthService.BLL.Services.Interface;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Interface;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Share.Exceptions;

namespace AuthService.BLL.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserToRoleRepository _userToRoleRepository;
    private readonly IMapper _mapper;
    private readonly byte[] _key = Encoding.UTF8.GetBytes("SuperStrongAndLongJwtSecretKey_123456!");

    public AuthService(IUserRepository userRepository, IUserToRoleRepository userToRoleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _userToRoleRepository = userToRoleRepository;
        _mapper = mapper;
    }

    public async Task<TokenPair> GenerateToken(UserCredentialsModel userCredentials)
    {
        var user = await _userRepository.SelectByLogin(userCredentials.Login)
                   ?? throw new UserNotFoundException();

        var hashedPassword = PasswordHelper.HashPassword(userCredentials.Password, user.Salt);
        if (user.Hash != hashedPassword)
        {
            throw new InvalidPasswordException();
        }

        var userInfo = new UserInfoEntity { Id = user.Id, Username = user.Username, Roles = user.Roles };

        var accessToken = GenerateJwt(userInfo, TimeSpan.FromMinutes(15));
        var refreshToken = GenerateJwt(userInfo, TimeSpan.FromDays(7));

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task Register(UserModel user)
    {
        if (await _userRepository.SelectByLogin(user.Login) != null)
        {
            throw new UserAlreadyExistsException();
        }

        var userEntity = _mapper.Map<UserEntity>(user);

        using var connection = _userRepository.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        var userId = await _userRepository.InsertInTransaction(userEntity, connection, transaction);
        await _userToRoleRepository.InsertInTransaction(
            new UserToRoleEntity { UserId = userId, RoleId = user.RoleId }, connection, transaction);

        transaction.Commit();
    }

    public async Task ResetPassword(UserCredentialsModel userCredentials)
    {
        if (await _userRepository.SelectByLogin(userCredentials.Login) == null)
        {
            throw new UserNotFoundException();
        }
        
        var userEntity = _mapper.Map<UserEntity>(new UserModel { Login = userCredentials.Login, Password = userCredentials.Password });
        await _userRepository.UpdatePassword(userEntity);
    }

    public async Task<TokenPair> RefreshToken(Guid userId, string refreshToken)
    {
        var user = await _userRepository.SelectById(userId);
        if (user == null)
        {
            throw new SecurityTokenException("User not found");
        }

        var principal = ValidateToken(refreshToken);
        if (principal == null || principal.FindFirst(ClaimTypes.NameIdentifier)?.Value != user.Id.ToString())
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        var newAccessToken = GenerateJwt(user, TimeSpan.FromMinutes(15));
        var newRefreshToken = GenerateJwt(user, TimeSpan.FromDays(7));

        return new TokenPair
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "AuthService",
            ValidAudience = "WebAPI",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_key),
            ValidateLifetime = true
        };

        try
        {
            return tokenHandler.ValidateToken(token, parameters, out var _);
        }
        catch
        {
            return null;
        }
    }

    private ClaimsIdentity GenerateClaimsIdentity(UserInfoEntity user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username)
        };

        foreach (var role in user.Roles.Split(','))
            claims.Add(new Claim(ClaimTypes.Role, role.Trim()));

        return new ClaimsIdentity(claims);
    }

    private string GenerateJwt(UserInfoEntity user, TimeSpan lifetime)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(user),
            Expires = DateTime.UtcNow.Add(lifetime),
            Issuer = "AuthService",
            Audience = "WebAPI",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
