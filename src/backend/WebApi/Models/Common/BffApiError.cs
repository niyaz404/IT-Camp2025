namespace WebApi.Models.Common;

/// <summary>
/// Ошибка в ответе
/// </summary>
public record BffApiError(string Code, string Message);