using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using WebApi.BLL.Helpers;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Services.Interfaces.Auth;
using WebApi.BLL.Services.Interfaces.Cache;
using WebApi.DAL.Models.Implementation.Auth;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Auth;

/// <summary>
/// Сервис для работы с авторизацией
/// </summary>
public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IAuthProvider _authProvider;
    private readonly IRefreshStoreService _refreshStoreService;
    
    public AuthService(IMapper mapper, IAuthProvider authProvider, IRefreshStoreService refreshStoreService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
        _refreshStoreService = refreshStoreService ?? throw new ArgumentNullException(nameof(refreshStoreService)); 
    }
    
    public async Task<LoginResponseModel> Login(UserCredentialsModel userCredentials)
    {
        var tokens = await _authProvider.Login(_mapper.Map<UserCredentials>(userCredentials));

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(tokens.AccessToken) as JwtSecurityToken;
        var roles = jsonToken?.Claims
            .Where(claim => claim.Type == "role")
            .Select(x => x.Value)
            .ToArray() ?? Array.Empty<string>();

        var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await _refreshStoreService.SaveAsync(userId, tokens.RefreshToken);
        }

        return new LoginResponseModel
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            Roles = roles
        };
    }


    public async Task<LoginResponseModel> Register(UserModel user)
    {
        var tokens = await _authProvider.Register(_mapper.Map<User>(user));

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(tokens.AccessToken) as JwtSecurityToken;
        var roles = jsonToken?.Claims.Where(claim => claim.Type == "role").Select(x => x.Value).ToArray() ?? Array.Empty<string>();

        var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await _refreshStoreService.SaveAsync(userId, tokens.RefreshToken);
        }

        return new LoginResponseModel
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            Roles = roles
        };
    }
    
    public async Task<TokenPair> RefreshToken(string refreshToken)
    {
        ClaimsPrincipal principal;
        try
        {
            principal = JwtHelper.ValidateToken(refreshToken);
        }
        catch (Exception)
        {
            throw new SecurityTokenException("Invalid refresh token format or signature");
        }

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (string.IsNullOrEmpty(userId))
            throw new SecurityTokenException("Invalid refresh token");

        var storedRefreshToken = await _refreshStoreService.GetAsync(userId);
        if (storedRefreshToken != refreshToken)
            throw new SecurityTokenException("Refresh token is invalid or expired");

        var newTokens = await _authProvider.RefreshToken(userId, refreshToken);

        await _refreshStoreService.SaveAsync(userId, newTokens.RefreshToken);

        return newTokens;
    }

    public async Task ResetPassword(UserCredentialsModel userCredentials)
    {
        await _authProvider.ResetPassword(_mapper.Map<UserCredentials>(userCredentials));
    }
}