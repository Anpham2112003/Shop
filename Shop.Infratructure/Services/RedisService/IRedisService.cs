using System.Collections;

namespace Shop.Infratructure.Services.RedisService;

public interface IRedisService
{
    Task<bool> SetCacheAsync(string? key, string data,DateTime dateTime,CancellationToken cancellationToken);
    Task<bool> RemoveCache(string key,CancellationToken cancellationToken);

    Task<TResult?> GetCachesAsync<TResult>(string key, CancellationToken cancellationToken)
        where TResult : IEnumerable;

    Task<TResult?> GetCacheAsync<TResult>(string key, CancellationToken cancellationToken);
    Task<string> GetCacheAsync(string key,CancellationToken cancellationToken);
}