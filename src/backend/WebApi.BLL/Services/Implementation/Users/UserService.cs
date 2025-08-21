using System.Drawing;
using AutoMapper;
using Share.Services.Interface;
using WebApi.BLL.Models.Users;
using WebApi.BLL.Services.Interfaces.Users;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Users;

/// <summary>
/// Сервис работы с опльзователями
/// </summary>
public class UserService : IUserService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    
    public UserService(ILogger logger, IMapper mapper, IUserProvider userProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
    }
    
    public async Task<UserInfoModel> GetCurrentUserInfoAsync(string bearerToken)
    {
        var userInfo = await _userProvider.GetCurrentUserInfoAsync(bearerToken);
        
        return _mapper.Map<UserInfoModel>(userInfo);
    }
}