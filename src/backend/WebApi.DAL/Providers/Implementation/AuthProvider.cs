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
    
    public async Task<TokenPair> Login(UserCredentials userCredentials)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/generatetoken", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenPair>(responseContent);

        return tokenResponse;
    }

    public async Task<TokenPair> Register(User user)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/register", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenPair>(responseContent);

        return tokenResponse;
    }
    
    public async Task<TokenPair> RefreshToken(string userId, string refreshToken)
    {
        var requestContent = new StringContent(
            JsonConvert.SerializeObject(new { UserId = userId, RefreshToken = refreshToken }),
            Encoding.UTF8, 
            "application/json");

        var response = await _httpClient.PostAsync($"{_url}/api/auth/refreshtoken", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenPair>(responseContent);

        return tokenResponse;
    }

    public async Task ResetPassword(UserCredentials userCredentials)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync($"{_url}/api/auth/resetpassword", requestContent);

        await HttpResponseInspector.EnsureSuccessAsync(response);
    }
}