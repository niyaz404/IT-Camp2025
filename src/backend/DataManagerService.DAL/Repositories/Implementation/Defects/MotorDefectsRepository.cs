using Dapper;
using DataManagerService.DAL.Models.Defects;
using DataManagerService.DAL.Repositories.Abstract;
using DataManagerService.DAL.Repositories.Interface.Defects;
using Npgsql;

namespace DataManagerService.DAL.Repositories.Implementation.Defects;

public class MotorDefectsRepository(string connectionString) : Repository(connectionString, "motor_defects"), IMotorDefectsRepository
{
    public async Task<IEnumerable<MotorDefectEntity>> SelectByMotorIdAsync(long motorId)
    {
        var sql = $@"
                select 
                    id,
                    motor_id,
                    defect_type,
                    severity,
                    detection_date,
                    detector_version,
                    note,
                    created_at,
                    updated_at
                from {_mainTableName}
                where motor_id = :motorId
                order by detection_date desc;";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<MotorDefectEntity>(sql, new { motorId });
    }
}