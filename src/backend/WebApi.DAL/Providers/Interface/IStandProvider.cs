using WebApi.DAL.Models.Implementation.Stands;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Провайдер для стендов
/// </summary>
public interface IStandProvider
{
    /// <summary>
    /// Получение всех стендов
    /// </summary>
    Task<IEnumerable<Stand>> GetAllAsync();
}