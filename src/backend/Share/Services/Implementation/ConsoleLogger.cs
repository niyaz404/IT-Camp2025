using Share.Services.Interface;
using Newtonsoft.Json;

namespace Share.Services.Implementation;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }

    public void Log<T>(T entity, string message = null)
    {
        Console.WriteLine($"{message}{Environment.NewLine}{JsonConvert.SerializeObject(entity)}");
    }
}