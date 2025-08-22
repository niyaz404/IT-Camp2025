using AutoMapper;
using DataManagerService.BLL.Models.Stands;
using DataManagerService.BLL.Models.Users;
using DataManagerService.DAL.Models.Stands;
using DataManagerService.DAL.Models.Users;

namespace DataManagerService.BLL.Mapping;

/// <summary>
/// Профиль для маппинга типов слоев Репозиториев и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Репозиториев и Бизнес-логики
        CreateMap<StandCompositeEntity, StandModel>();
        CreateMap<StandModel, StandCompositeEntity>();
        
        CreateMap<UserEntity, UserInfoModel>();
        CreateMap<UserInfoModel, UserEntity>();
    }
}