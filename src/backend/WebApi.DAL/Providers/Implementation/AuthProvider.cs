using System.Net;
using System.Security.Authentication;
using System.Text;
using Newtonsoft.Json;
using Share;
using Share.Exceptions;
using Share.Helpers;
using WebApi.DAL.Models.Implementation.Auth;
using WebApi.DAL.Providers.Interface;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace WebApi.DAL.Providers.Implementation;

/// <summary>
/// Провайдер для работы с Auth-сервисом
/// </summary>
public class AuthProvider : IAuthProvider
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    
    public AuthProvider(string url, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;
    }
    
    public async Task<string> Login(UserCredentials userCredentials)
    {
        // Создаем HTTP-запрос для AuthService
        var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/generatetoken", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        return tokenResponse.Token;
    }

    public async Task<string> Register(User user)
    {
        // Создаем HTTP-запрос для AuthService
        var requestContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/register", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        return tokenResponse.Token;
    }

    public async Task ResetPassword(UserCredentials userCredentials)
    {
        // Создаем HTTP-запрос для AuthService
        var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/resetpassword", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);
    }

    /// <summary>
    /// Модель ответа от сервиса авторизации
    /// </summary>
    class TokenResponse
    {
        public string Token { get; set; }
    }
}