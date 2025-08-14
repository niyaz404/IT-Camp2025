using Share.Services.Implementation;
using Share.Services.Interface;
using Unity;

namespace WebApi.DI;

public static class UnityConfig
{
    static IUnityContainer Configure(IUnityContainer container)
    {
        container.RegisterSingleton<ILogger, ConsoleLogger>();

        return container;
    }
}