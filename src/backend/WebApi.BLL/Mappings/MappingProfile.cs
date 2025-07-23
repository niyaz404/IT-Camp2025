using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.DLL.Models.Implementation.Auth;

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