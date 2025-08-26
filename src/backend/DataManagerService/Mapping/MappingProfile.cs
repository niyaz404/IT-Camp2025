using AutoMapper;
using DataManagerService.BLL.Models.Defects;
using DataManagerService.BLL.Models.Motors;
using DataManagerService.BLL.Models.Stands;
using DataManagerService.BLL.Models.Users;
using DataManagerService.Models.Defects;
using DataManagerService.Models.Motors;
using DataManagerService.Models.Stands;
using DataManagerService.Models.Users;

namespace DataManagerService.Mapping;

/// <summary>
/// Профиль для маппинга типов слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Контроллеров и Бизнес-логики
        CreateMap<StandDto, StandModel>();
        CreateMap<StandModel, StandDto>();
        
        CreateMap<UserInfoDto, UserInfoModel>();
        CreateMap<UserInfoModel, UserInfoDto>();
        
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