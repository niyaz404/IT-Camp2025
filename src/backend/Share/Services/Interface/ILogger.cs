namespace Share.Services.Interface;

public interface ILogger
{
    void Log(string message);

    void Log<T>(T entity, string message = null);
}