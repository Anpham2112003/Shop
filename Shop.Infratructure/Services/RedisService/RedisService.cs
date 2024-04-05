using System.Collections;
using System.Data;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;


namespace Shop.Infratructure.Services.RedisService;

public class RedisService:IRedisService
{
    private readonly IDistributedCache _cache;
    
    public RedisService(IDistributedCache cache)
    {
        _cache = cache;
        
    }

    public async Task<bool> SetCacheAsync(string? key, string data,DateTime dateTime, CancellationToken cancellationToken)
    {
        try
        {
        
            
            await _cache.SetStringAsync(key, data,new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = dateTime,
            }, cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            throw new RedisException("cant not set cache");
        }
    }

    public async Task<bool> RemoveCache(string key,CancellationToken cancellationToken)
    {
        var checkCache = await _cache.GetStringAsync(key,cancellationToken);
        
        if (string.IsNullOrEmpty(checkCache)) return false;
        
        await _cache.RemoveAsync(key,cancellationToken);
        
        return true;
    }

    public async Task<TResult?> GetCachesAsync<TResult>(string key, CancellationToken cancellationToken) where TResult : IEnumerable
    {
        var cacheResult = await _cache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cacheResult)) return default;

        var result = JsonSerializer.Deserialize<TResult>(cacheResult);
            
        return result;
    }


    public async Task<TResult?> GetCacheAsync<TResult>(string key,CancellationToken cancellationToken) 
    {
        var cacheResult = await _cache.GetStringAsync(key, cancellationToken);
            
        if (string.IsNullOrEmpty(cacheResult)) return default;

        var result = JsonSerializer.Deserialize<TResult>(cacheResult);
        return result;
    }
    
    

    public async Task<string> GetCacheAsync(string key,CancellationToken cancellationToken)
    {
       
            var result =await _cache.GetStringAsync(key, cancellationToken);
            
            if (string.IsNullOrEmpty(result)) return string.Empty;
            
            return result;
        
    }
}