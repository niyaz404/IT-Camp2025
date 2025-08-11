using System.Net;
using System.Security.Authentication;
using Newtonsoft.Json;
using Share.Enums;
using Share.Exceptions;
using Share.Models;

namespace Share.Helpers;

/// <summary>
/// Хелпер для проверки ответа http
/// </summary>
public static class HttpResponseInspector
{
    /// <summary>
    /// Проверяет успешность операции. В случае ошибки выкидывает нужно исключение
    /// </summary>
    /// <param name="response">Http-ответ</param>
    public static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }
        
        var body = await response.Content.ReadAsStringAsync();
        var error = JsonConvert.DeserializeObject<ResponseError>(body);
        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ThrowUnauthorizedException(error);
        }
        
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Выбрасывает подходящее исключение аутентификации 
    /// </summary>
    private static void ThrowUnauthorizedException(ResponseError error)
    {
        if (error == null)
        {
            throw new AuthenticationException();
        }
            
        switch (error.Code)
        {
            case ErrorCode.InvalidPassword:
            {
                throw new InvalidPasswordException();
            }
            case ErrorCode.UserAlreadyExists:
            {
                throw new UserAlreadyExistsException();
            }
            case ErrorCode.UserNotFound:
            {
                throw new UserNotFoundException();
            }
            default:
            {
                throw new AuthenticationException(error.Message);
            }
        }
    }
}