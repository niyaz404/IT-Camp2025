using System.Security.Authentication;

namespace Share.Exceptions;

/// <summary>
/// Пользователь не найден
/// </summary>
public class UserNotFoundException() : AuthenticationException("User does not exist.");