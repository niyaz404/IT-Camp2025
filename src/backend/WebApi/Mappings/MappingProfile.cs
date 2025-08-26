using AutoMapper;
using WebApi.BLL.Models.Auth;
using WebApi.BLL.Models.Defects;
using WebApi.BLL.Models.Motors;
using WebApi.BLL.Models.Stands;
using WebApi.BLL.Models.Users;
using WebApi.Models.Auth;
using WebApi.Models.Defects;
using WebApi.Models.Motors;
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
        
        CreateMap<StandWithMotorsDto, StandWithMotorsModel>();
        CreateMap<StandWithMotorsModel, StandWithMotorsDto>();
        
        CreateMap<KeycloakUserDto, KeycloakUserModel>();
        CreateMap<KeycloakUserModel, KeycloakUserDto>();
        
        CreateMap<MotorDto, MotorModel>();
        CreateMap<MotorModel, MotorDto>();
        
        CreateMap<MotorDefectDto, MotorDefectModel>();
        CreateMap<MotorDefectModel, MotorDefectDto>();
        
        CreateMap<MotorWithDefectsDto, MotorWithDefectsModel>();
        CreateMap<MotorWithDefectsModel, MotorWithDefectsDto>();
    }

}