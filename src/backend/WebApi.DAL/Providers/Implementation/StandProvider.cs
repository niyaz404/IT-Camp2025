using Newtonsoft.Json;
using Share.Helpers;
using WebApi.DAL.Enums;
using WebApi.DAL.Models.Implementation.Stands;
using WebApi.DAL.Models.Implementation.Users;
using WebApi.DAL.Providers.Interface;

namespace WebApi.DAL.Providers.Implementation;

/// <summary>
/// Провайдер для стендов
/// </summary>
public class StandProvider : IStandProvider
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    
    public StandProvider(string url, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<Stand>> GetAllAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/api/stands/getall");

        var response = await _httpClient.SendAsync(request);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var standsResponse = JsonConvert.DeserializeObject<IEnumerable<Stand>>(responseContent);

        return standsResponse;
    }
}