using DataManagerService.BLL.Models.Motors;

namespace DataManagerService.BLL.Services.Interfaces.Motors;

/// <summary>
/// Сервис для работы с ЭД
/// </summary>
public interface IMotorService
{
    /// <summary>
    /// Получение по идентификатору
    /// </summary>
    Task<MotorWithDefectsModel> GetMotorByIdAsync(long id);
    
    /// <summary>
    /// Получение по идентификатору стенда
    /// </summary>
    Task<IEnumerable<MotorModel>> GetMotorsByStandIdAsync(long standId);
}