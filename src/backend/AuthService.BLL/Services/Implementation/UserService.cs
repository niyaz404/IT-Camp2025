using AuthService.BLL.Models;
using AuthService.BLL.Services.Interface;
using AuthService.DAL.Repositories.Interface;
using AutoMapper;

namespace AuthService.BLL.Services.Implementation;

/// <summary>
/// Сервис работы с опльзователями
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IMapper? mapper, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public async Task<UserInfoModel> GetInfoByIdAsync(Guid userId)
    {
        var userInfoEntity = await _userRepository.SelectById(userId);
        var userInfo = _mapper.Map<UserInfoModel>(userInfoEntity);
        
        return userInfo;
    }
}