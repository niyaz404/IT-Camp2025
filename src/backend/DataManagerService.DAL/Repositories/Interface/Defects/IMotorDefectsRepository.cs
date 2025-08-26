using DataManagerService.DAL.Models.Defects;

namespace DataManagerService.DAL.Repositories.Interface.Defects;

public interface IMotorDefectsRepository
{
    /// <summary>
    /// Получить список дефектов по двигателю
    /// </summary>
    Task<IEnumerable<MotorDefectEntity>> SelectByMotorIdAsync(long motorId);
}