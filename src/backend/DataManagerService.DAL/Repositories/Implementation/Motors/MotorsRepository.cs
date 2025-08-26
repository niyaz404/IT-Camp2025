using Dapper;
using DataManagerService.DAL.Models.Motors;
using DataManagerService.DAL.Repositories.Abstract;
using DataManagerService.DAL.Repositories.Interface.Motors;
using Npgsql;

namespace DataManagerService.DAL.Repositories.Implementation.Motors;

public class MotorsRepository(string connectionString) : Repository(connectionString, "motors"), IMotorsRepository
{
    public async Task<IEnumerable<MotorCompositeEntity>> SelectByStandIdAsync(long standId)
    {
        var sql = $@"
        select 
            m.id,
            m.stand_id,
            m.manufacturer,
            m.factory_number,
            m.description,
            m.state,
            m.installed_at,
            m.created_at,
            m.updated_at,
            mm.name,
            mm.rated_power,
            mm.rated_frequency,
            mm.rated_current,
            mm.voltage,
            mm.phases_count,
            mm.type,
            count(d.id) as defectscount,
            coalesce(max(d.severity), 0) as maxseverity
        from {_mainTableName} m
        join {_schemaName}.motor_models mm on m.model_id = mm.id
        left join {_schemaName}.motor_defects d on d.motor_id = m.id
        where m.stand_id = @standId
        group by 
            m.id,
            m.stand_id,
            m.manufacturer,
            m.factory_number,
            m.description,
            m.state,
            m.installed_at,
            m.created_at,
            m.updated_at,
            mm.name,
            mm.rated_power,
            mm.rated_frequency,
            mm.rated_current,
            mm.voltage,
            mm.phases_count,
            mm.type
        order by m.id;";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<MotorCompositeEntity>(sql, new { standId });
    }

    public async Task<MotorCompositeEntity> SelectByIdAsync(long id)
    {
        var sql = $@"
            select 
                m.id,
                m.stand_id,
                m.manufacturer,
                m.factory_number,
                m.description,
                m.state,
                m.installed_at,
                m.created_at,
                m.updated_at,
                mm.name,
                mm.rated_power,
                mm.rated_frequency,
                mm.rated_current,
                mm.voltage,
                mm.phases_count,
                mm.type
            from {_mainTableName} m
            join {_schemaName}.motor_models mm on m.model_id = mm.id
            where m.id = :id;";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstAsync<MotorCompositeEntity>(sql, new { id });
    }
}