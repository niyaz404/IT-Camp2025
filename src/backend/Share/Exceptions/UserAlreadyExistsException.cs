using System.Security.Authentication;

namespace Share.Exceptions;

/// <summary>
/// Пользователь с таким логином уже существует
/// </summary>
public class UserAlreadyExistsException() : AuthenticationException("User already exists.");