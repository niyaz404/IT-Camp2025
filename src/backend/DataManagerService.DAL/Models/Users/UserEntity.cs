using DataManagerService.DAL.Models.Base;

namespace DataManagerService.DAL.Models.Users;

/// <summary>
/// Сущность пользователя
/// </summary>
public class UserEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// ФИО
    /// </summary>
    public string Username { get; set; }
}