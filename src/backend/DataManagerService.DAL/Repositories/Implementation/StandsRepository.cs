using Dapper;
using DataManagerService.DAL.Models.Stands;
using DataManagerService.DAL.Models.Users;
using DataManagerService.DAL.Repositories.Abstract;
using DataManagerService.DAL.Repositories.Interface;
using DataManagerSevice.Common.Enums;
using Npgsql;

namespace DataManagerService.DAL.Repositories.Implementation;

/// <summary>
/// Репозиторий для Связи Пользователя и Роли
/// </summary>
public class StandsRepository(string connectionString) : Repository(connectionString, "stands"), IStandsRepository
{
    public async Task<IEnumerable<StandCompositeEntity>> SelectAllAsync()
    {
        var result = new List<StandCompositeEntity>
        {
            new()
            {
                Id = 1, Name = "Стенд 1", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 3, ResponsiblePerson = new UserEntity { Username = "Иванов Иван Иванович" },
                State = StandState.On
            },
            new()
            {
                Id = 2, Name = "Стенд 2", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 3, ResponsiblePerson = new UserEntity { Username = "Галиев Нияз Рафисович" },
                State = StandState.Off
            },
            new()
            {
                Id = 3, Name = "Стенд 3", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 3, ResponsiblePerson = new UserEntity { Username = "Иванов Иван Иванович" },
                State = StandState.On
            },
            new()
            {
                Id = 4, Name = "Стенд 4", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 1, ResponsiblePerson = new UserEntity { Username = "Иванов Иван Иванович" },
                State = StandState.Off
            },
            new()
            {
                Id = 5, Name = "Стенд 5", Description = "Тестирование двигателей", Frequency = 25.6M, Power = 3,
                PhasesCount = 1, ResponsiblePerson = new UserEntity { Username = "Иванов Иван Иванович" },
                State = StandState.On
            },
        };
        
        return result;
        
        var sql = $"select * from {_mainTableName}";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<StandCompositeEntity>(sql);
    }
}