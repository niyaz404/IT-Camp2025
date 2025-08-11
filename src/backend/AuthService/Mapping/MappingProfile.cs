using AuthService.BLL.Models;
using AuthService.DAL.Models;
using AuthService.Models;
using AutoMapper;

namespace AuthService.Mapping;

/// <summary>
/// Профиль для маппинга типов слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Контроллеров и Бизнес-логики
        CreateMap<UserDto, UserModel>();
        CreateMap<UserModel, UserDto>();
        
        CreateMap<UserCredentialsDto, UserCredentialsModel>();
        CreateMap<UserCredentialsModel, UserCredentialsDto>();
        
        CreateMap<RoleDto, RoleModel>();
        CreateMap<RoleModel, RoleDto>();

        CreateMap<UserInfoDto, UserInfoModel>();
        CreateMap<UserInfoModel, UserInfoDto>();
    }
}