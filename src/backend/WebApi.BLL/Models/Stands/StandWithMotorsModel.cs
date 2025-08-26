using WebApi.BLL.Enums;
using WebApi.BLL.Models.Motors;
using WebApi.BLL.Models.Users;

namespace WebApi.BLL.Models.Stands;

/// <summary>
/// Модель стенда
/// </summary>
public class StandWithMotorsModel
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
    /// ЭД на стенде
    /// </summary>
    public IEnumerable<MotorModel> Motors { get; set; }
    
    /// <summary>
    /// Ответственный
    /// </summary>
    public KeycloakUserModel ResponsiblePerson { get; set; }
    
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