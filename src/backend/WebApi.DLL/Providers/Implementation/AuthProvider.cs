using System.Text;
using Newtonsoft.Json;
using WebApi.DLL.Models.Implementation.Auth;
using WebApi.DLL.Providers.Interface;

namespace WebApi.DLL.Providers.Implementation;

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
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid username or password.");
        }
            
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
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid username or password.");
        }
            
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
            
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid username or password.");
        }
    }

    /// <summary>
    /// Модель ответа от сервиса авторизации
    /// </summary>
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}