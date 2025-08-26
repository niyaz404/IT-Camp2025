using AutoMapper;
using WebApi.BLL.Models.Auth;
using WebApi.BLL.Models.Defects;
using WebApi.BLL.Models.Motors;
using WebApi.BLL.Models.Stands;
using WebApi.BLL.Models.Users;
using WebApi.DAL.Models.Implementation.Auth;
using WebApi.DAL.Models.Implementation.Defects;
using WebApi.DAL.Models.Implementation.Motors;
using WebApi.DAL.Models.Implementation.Stands;
using WebApi.DAL.Models.Implementation.Users;

namespace WebApi.BLL.Mappings;

/// <summary>
/// Профиль для маппинга типов слоев Бизнес-логики и Провайдеров\Репозиториев
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Бизнес-логики и Провайдеров\Репозиториев
        CreateMap<UserCredentials, UserCredentialsModel>();
        CreateMap<UserCredentialsModel, UserCredentials>();
        
        CreateMap<User, UserModel>();
        CreateMap<UserModel, User>();
        
        CreateMap<Stand, StandModel>();
        CreateMap<StandModel, Stand>();
        
        CreateMap<Stand, StandWithMotorsModel>();
        CreateMap<StandWithMotorsModel, Stand>();
        
        CreateMap<UserInfo, UserInfoModel>();
        CreateMap<UserInfoModel, UserInfo>();
        
        CreateMap<KeycloakUser, KeycloakUserModel>();
        CreateMap<KeycloakUserModel, KeycloakUser>();
        
        CreateMap<Motor, MotorModel>();
        CreateMap<MotorModel, Motor>();
        
        CreateMap<MotorDefect, MotorDefectModel>();
        CreateMap<MotorDefectModel, MotorDefect>();
        
        CreateMap<MotorWithDefects, MotorWithDefectsModel>();
        CreateMap<MotorWithDefectsModel, MotorWithDefects>();
    }

    public byte[] Convert2(string s)
    {
        // Преобразование строки Base64 в массив байтов
        if (string.IsNullOrEmpty(s))
        {
            return null;
        }
        try
        {
            return Convert.FromBase64String(s);
        }
        catch (FormatException)
        {
            // Обработка ошибки в случае некорректной строки Base64
            return null;
        }
    }
}