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
        var result = new List<Stand>
        {
            new()
            {
                Id = 1, Name = "Стенд 1", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 3, ResponsiblePerson = new UserInfo { Username = "Иванов Иван Иванович" },
                State = StandState.On
            },
            new()
            {
                Id = 2, Name = "Стенд 2", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 3, ResponsiblePerson = new UserInfo { Username = "Галиев Нияз Рафисович" },
                State = StandState.Off
            },
            new()
            {
                Id = 3, Name = "Стенд 3", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 3, ResponsiblePerson = new UserInfo { Username = "Иванов Иван Иванович" },
                State = StandState.On
            },
            new()
            {
                Id = 4, Name = "Стенд 4", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 1, ResponsiblePerson = new UserInfo { Username = "Иванов Иван Иванович" },
                State = StandState.Off
            },
            new()
            {
                Id = 5, Name = "Стенд 5", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 1, ResponsiblePerson = new UserInfo { Username = "Иванов Иван Иванович" },
                State = StandState.On
            },
        };
        
        return result;
    }
}