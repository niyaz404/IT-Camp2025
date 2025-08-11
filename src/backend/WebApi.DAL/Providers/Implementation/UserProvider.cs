using System.Text;
using Newtonsoft.Json;
using Share.Helpers;
using WebApi.DAL.Models.Implementation.Users;
using WebApi.DAL.Providers.Interface;

namespace WebApi.DAL.Providers.Implementation;

/// <summary>
/// Провайдер для работы с пользователями
/// </summary>
public class UserProvider : IUserProvider
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    
    public UserProvider(string url, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;
    }
    
    public async Task<UserInfo> GetCurrentUserInfoAsync(string bearerToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/api/users/me");
        
        request.Headers.Add("Authorization", bearerToken);

        var response = await _httpClient.SendAsync(request);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var userInfoResponse = JsonConvert.DeserializeObject<UserInfo>(responseContent);

        return userInfoResponse;
    }
}