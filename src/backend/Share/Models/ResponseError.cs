using Share.Enums;

namespace Share.Models;

/// <summary>
/// Ошибка в ответе
/// </summary>
public record ResponseError(ErrorCode Code, string Message);