using AutoMapper;
using Unity;
using WebApi.DAL.Providers.Implementation;
using WebApi.DAL.Providers.Interface;

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