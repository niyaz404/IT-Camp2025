using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Models.Implementation.Stands;
using WebApi.BLL.Models.Implementation.Users;
using WebApi.DAL.Models.Implementation.Users;
using WebApi.Models.Auth;
using WebApi.Models.Stands;
using WebApi.Models.Users;

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
        
        CreateMap<UserInfoModel, UserInfoDto>();
        CreateMap<UserInfoDto, UserInfoModel>();
        
        CreateMap<StandDto, StandModel>();
        CreateMap<StandModel, StandDto>();
    }

}