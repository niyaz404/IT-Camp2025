namespace Share.Enums;

/// <summary>
/// Коды ошибок
/// </summary>
public enum ErrorCode
{
    UnknownError = 0,
    
    UserNotFound = 1,
    
    UserAlreadyExists = 2,
    
    InvalidPassword = 3,
    
    InvalidRefreshToken = 4,
}