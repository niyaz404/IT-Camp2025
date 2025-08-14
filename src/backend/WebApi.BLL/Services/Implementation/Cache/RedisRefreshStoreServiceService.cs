using StackExchange.Redis;
using WebApi.BLL.Services.Interfaces.Cache;

namespace WebApi.BLL.Services.Implementation.Cache;

/// <summary>
/// Сервис для работы с хранилищем редис
/// </summary>
public class RedisRefreshStoreServiceService : IRefreshStoreService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisRefreshStoreServiceService(IConnectionMultiplexer redis)
    {
        _redis = redis ?? throw new ArgumentNullException(nameof(redis));
    }

    public async Task SaveAsync(string userId, string refreshToken)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync($"refresh:{userId}", refreshToken, TimeSpan.FromDays(30));
    }

    public async Task<string?> GetAsync(string userId)
    {
        var db = _redis.GetDatabase();
        return await db.StringGetAsync($"refresh:{userId}");
    }
}