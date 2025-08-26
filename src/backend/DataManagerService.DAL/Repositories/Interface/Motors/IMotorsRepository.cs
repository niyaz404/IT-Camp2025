using DataManagerService.DAL.Models.Motors;

namespace DataManagerService.DAL.Repositories.Interface.Motors;

public interface IMotorsRepository
{
    /// <summary>
    /// Получение по идентификатору стенда
    /// </summary>
    Task<IEnumerable<MotorCompositeEntity>> SelectByStandIdAsync(long standId);
    
    /// <summary>
    /// Получение по идентификатору стенда
    /// </summary>
    Task<MotorCompositeEntity> SelectByIdAsync(long id);
}