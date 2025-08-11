using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using AuthService.BLL.Helpers;
using AuthService.BLL.Models;
using AuthService.BLL.Services.Interface;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Interface;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Share.Exceptions;

namespace AuthService.BLL.Services.Implementation;

/// <summary>
/// Сервис аутентификации на уровне бизнес-логики
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserToRoleRepository _userToRoleRepository;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IUserToRoleRepository userToRoleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _userToRoleRepository = userToRoleRepository;
        _mapper = mapper;
    }
    
    public async Task<string> GenerateToken(UserCredentialsModel userCredentials)
    {
        var user = await _userRepository.SelectByLogin(userCredentials.Login);
        
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        
        var hashedPassword = PasswordHelper.HashPassword(userCredentials.Password, user.Salt);
        
        if (user.Hash != hashedPassword)
        {
            throw new InvalidPasswordException();
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "SuperStrongAndLongJwtSecretKey_123456!"u8.ToArray();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(user),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "AuthService",
            Audience = "WebAPI",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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

        await _userToRoleRepository.InsertInTransaction(new UserToRoleEntity
            { UserId = userId, RoleId = user.RoleId }, connection, transaction);
        
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

    private ClaimsIdentity GenerateClaimsIdentity(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username)
        };
        
        foreach (var role in user.Roles.Split(','))
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
        }
        
        return new ClaimsIdentity(claims);
    }
}