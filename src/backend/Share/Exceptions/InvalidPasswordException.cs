namespace Share.Exceptions;

/// <summary>
/// Неверный логин или пароль
/// </summary>
public class InvalidPasswordException() : Exception("Password is incorrect.");