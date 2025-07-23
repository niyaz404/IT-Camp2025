using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.Models.Implementation.Auth;

namespace WebApi.Mappings;

/// <summary>
/// Профиль для маппинга типов слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Контроллеров и Бизнес-логики
        CreateMap<UserCredentialsDto, UserCredentialsModel>();
        CreateMap<UserCredentialsModel, UserCredentialsDto>();
        
        CreateMap<UserDto, UserModel>();
        CreateMap<UserModel, UserDto>();
    }

}