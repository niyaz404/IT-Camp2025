using AutoMapper;
using Unity;
using WebApi.DLL.Providers.Implementation;
using WebApi.DLL.Providers.Interface;

namespace WebApi.BLL.DI;

/// <summary>
/// Настройка внедрения зависимостей
/// </summary>
public static class UnityConfig
{
    static IUnityContainer Configure(IUnityContainer container)
    {
        container.RegisterType<IMapper, Mapper>();
        
        container.RegisterType<IAuthProvider, AuthProvider>();

        return container;
    }
}