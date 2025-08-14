using WebApi.BLL.Models.Implementation.Stands;

namespace WebApi.BLL.Services.Interfaces.Stands;

/// <summary>
/// Сервис для работы со стендами
/// </summary>
public interface IStandService
{
    /// <summary>
    /// Получить список стендов
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<StandModel>> GetAll();
}