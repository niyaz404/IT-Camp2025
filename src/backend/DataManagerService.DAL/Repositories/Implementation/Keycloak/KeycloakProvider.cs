using System.Net.Http.Headers;
using System.Text.Json;
using DataManagerService.DAL.Models.Users;
using DataManagerService.DAL.Repositories.Interface.Keycloak;
using Newtonsoft.Json;

namespace DataManagerService.DAL.Repositories.Implementation.Keycloak;

public class KeycloakProvider : IKeycloakProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _realm;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public KeycloakProvider(HttpClient httpClient, string baseUrl, string realm, string clientId, string clientSecret)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        _realm = realm ?? throw new ArgumentNullException(nameof(realm));
        _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
        _clientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
    }
    
    public async Task<string?> GetServiceTokenAsync()
    {
        var url = $"{_baseUrl}/realms/{_realm}/protocol/openid-connect/token";

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _clientId,
                ["client_secret"] = _clientSecret
            })
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("access_token").GetString();
    }

    public async Task<KeycloakUser> GetUserAsync(Guid userId)
    {
        var token = await GetServiceTokenAsync();
        if (string.IsNullOrEmpty(token)) return null;

        var url = $"{_baseUrl}/admin/realms/{_realm}/users/{userId}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<KeycloakUser>(content);
    }

    public async Task<IEnumerable<KeycloakUser>> GetUsersAsync()
    {
        var token = await GetServiceTokenAsync();
        if (string.IsNullOrEmpty(token)) return null;

        var url = $"{_baseUrl}/admin/realms/{_realm}/users";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<KeycloakUser>>(content);
    }
}
    