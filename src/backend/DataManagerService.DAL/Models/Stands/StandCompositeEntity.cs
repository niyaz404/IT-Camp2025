using DataManagerService.DAL.Models.Base;
using DataManagerService.DAL.Models.Users;
using DataManagerSevice.Common.Enums;

namespace DataManagerService.DAL.Models.Stands;

/// <summary>
/// Сущность стенда
/// </summary>
public class StandCompositeEntity : BaseEntity
{
    /// <summary>
    /// Иденитфикатор
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
    /// Количество фаз
    /// </summary>
    public long PhasesCount { get; set; }
    
    /// <summary>
    /// Частота дискретизации, кГц
    /// </summary>
    public decimal Frequency { get; set; }
    
    /// <summary>
    /// Мощность, кВт
    /// </summary>
    public decimal Power { get; set; }
    
    /// <summary>
    /// Состояние
    /// </summary>
    public StandState State { get; set; }
    
    /// <summary>
    /// Ответственный
    /// </summary>
    public UserEntity ResponsiblePerson { get; set; }
}