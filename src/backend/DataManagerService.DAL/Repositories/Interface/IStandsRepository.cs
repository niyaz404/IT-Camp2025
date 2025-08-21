using DataManagerService.DAL.Models.Stands;

namespace DataManagerService.DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория Стендов
/// </summary>
public interface IStandsRepository
{
    /// <summary>
    /// Получение стендов
    /// </summary>
    public Task<IEnumerable<StandCompositeEntity>> SelectAllAsync();
}