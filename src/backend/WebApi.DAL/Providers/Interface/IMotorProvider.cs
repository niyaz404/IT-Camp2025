using WebApi.DAL.Models.Implementation.Motors;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Провайдер для ЭД
/// </summary>
public interface IMotorProvider
{
    /// <summary>
    /// Получение всех ЭД по стенду
    /// </summary>
    Task<IEnumerable<Motor>> GetByStandIdAsync(long standId);

    /// <summary>
    /// Получение по идентификатору
    /// </summary>
    Task<MotorWithDefects> GetByIdAsync(long id);
}