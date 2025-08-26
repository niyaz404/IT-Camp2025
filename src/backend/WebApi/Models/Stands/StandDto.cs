using System;
using WebApi.BLL.Enums;
using WebApi.Models.Users;

namespace WebApi.Models.Stands;

/// <summary>
/// Модель стенда
/// </summary>
public class StandDto
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
    public KeycloakUserDto ResponsiblePerson { get; set; }
    
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