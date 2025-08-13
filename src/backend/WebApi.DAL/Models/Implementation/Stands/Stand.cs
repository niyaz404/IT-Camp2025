using WebApi.DAL.Enums;
using WebApi.DAL.Models.Implementation.Users;

namespace WebApi.DAL.Models.Implementation.Stands;

/// <summary>
/// Модель стенда
/// </summary>
public class Stand
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
    /// Ответственный
    /// </summary>
    public UserInfo ResponsiblePerson { get; set; }
    
    /// <summary>
    /// Состояние
    /// </summary>
    public StandState State { get; set; }
}