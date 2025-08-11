using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.DAL.Models.Implementation.Auth;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Auth;

/// <summary>
/// Сервис для работы с авторизацией
/// </summary>
public class AuthService : IAuthService
{
    private readonly IAuthProvider _authProvider;
    private readonly IMapper _mapper;
    
    public AuthService(IAuthProvider authProvider, IMapper mapper)
    {
        _authProvider = authProvider;
        _mapper = mapper;
    }
    
    public async Task<LoginResponseModel> Login(UserCredentialsModel userCredentials)
    {
        var token = await _authProvider.Login(_mapper.Map<UserCredentials>(userCredentials));
        // Проверка и обработка JWT-токена
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var roles = jsonToken?.Claims.Where(claim => claim.Type == "role").Select(x => x.Value).ToArray();

        return new LoginResponseModel { Token = token, Roles = roles };
    }


    public async Task<LoginResponseModel> Register(UserModel user)
    {
        var token = await _authProvider.Register(_mapper.Map<User>(user));
        // Проверка и обработка JWT-токена
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var roles = jsonToken?.Claims.Where(claim => claim.Type == "role").Select(x => x.Value).ToArray();

        return new LoginResponseModel { Token = token, Roles = roles };
    }

    public async Task ResetPassword(UserCredentialsModel userCredentials)
    {
        await _authProvider.ResetPassword(_mapper.Map<UserCredentials>(userCredentials));
    }
}