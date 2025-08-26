using DataManagerService.DAL.Models.Base;
using DataManagerService.DAL.Models.Users;
using DataManagerService.Common.Enums;

namespace DataManagerService.DAL.Models.Stands;

/// <summary>
/// Сущность стенда
/// </summary>
public class StandCompositeEntity : CompositeEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Состояние
    /// </summary>
    public StandState State { get; set; }
    
    /// <summary>
    /// Местоположение
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    /// Количество электродвигателей на стенде
    /// </summary>
    public int MotorsCount { get; set; }
    
    /// <summary>
    /// Ответственный
    /// </summary>
    public Guid? ResponsiblePersonId { get; set; }
    
    /// <summary>
    /// Дата создания (UTC)
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата последнего обновления (UTC)
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Количество дефектов
    /// </summary>
    public long DefectsCount { get; set; }
}