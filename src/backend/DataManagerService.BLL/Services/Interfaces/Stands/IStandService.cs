using DataManagerService.BLL.Models.Stands;

namespace DataManagerService.BLL.Services.Interfaces.Stands;

/// <summary>
/// Сервис для работы со стендами
/// </summary>
public interface IStandService
{
    /// <summary>
    /// Получить список стендов
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<StandModel>> GetAllAsync();
}