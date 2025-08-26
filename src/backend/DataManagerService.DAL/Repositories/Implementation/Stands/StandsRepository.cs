using Dapper;
using DataManagerService.DAL.Models.Stands;
using DataManagerService.DAL.Repositories.Abstract;
using DataManagerService.DAL.Repositories.Interface.Stands;
using Npgsql;

namespace DataManagerService.DAL.Repositories.Implementation.Stands;

/// <summary>
/// Репозиторий для Связи Пользователя и Роли
/// </summary>
public class StandsRepository(string connectionString) : Repository(connectionString, "stands"), IStandsRepository
{
    public async Task<IEnumerable<StandCompositeEntity>> SelectAllAsync()
    {
        var sql = @$"
            select 
                s.id, 
                s.name, 
                s.description, 
                s.state, 
                s.location,
                s.created_at createdat, 
                s.updated_at updatedat,
                s.user_id as responsiblepersonid,
                count(distinct m.id) as motorscount,
                count(d.id) as defectscount
            from {_mainTableName} s
            left join {_schemaName}.motors m on m.stand_id = s.id
            left join {_schemaName}.motor_defects d on d.motor_id = m.id
            group by 
                s.id, s.name, s.description, s.state, s.location,
                s.created_at, s.updated_at, s.user_id
            order by s.id;";

        await using var connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<StandCompositeEntity>(sql);
    }
}