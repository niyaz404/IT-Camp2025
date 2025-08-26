using Newtonsoft.Json;
using Share.Helpers;
using WebApi.DAL.Models.Implementation.Motors;
using WebApi.DAL.Providers.Interface;

namespace WebApi.DAL.Providers.Implementation;

public class MotorProvider : IMotorProvider
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    
    public MotorProvider(string url, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<Motor>> GetByStandIdAsync(long standId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/api/motors/getall?standId={standId}");

        var response = await _httpClient.SendAsync(request);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var standsResponse = JsonConvert.DeserializeObject<IEnumerable<Motor>>(responseContent);

        return standsResponse;
    }
    
    public async Task<MotorWithDefects> GetByIdAsync(long id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/api/motors/get?id={id}");

        var response = await _httpClient.SendAsync(request);

        await HttpResponseInspector.EnsureSuccessAsync(response);
            
        var responseContent = await response.Content.ReadAsStringAsync();
        var standsResponse = JsonConvert.DeserializeObject<MotorWithDefects>(responseContent);

        return standsResponse;
    }
}