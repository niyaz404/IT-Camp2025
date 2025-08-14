namespace AuthService.Models;

/// <summary>
/// Dto для обновления токена
/// </summary>
public class RefreshTokenRequestDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Refresh токен
    /// </summary>
    public string RefreshToken { get; set; }
}