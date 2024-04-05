using Microsoft.AspNetCore.Http;

namespace Shop.Infratructure.Services.Aws3Service;

public interface IAwsSevice
{
    Task Upload(IFormFile file,string bucket, string key,CancellationToken cancellationToken);
    Task<bool> Delete(string bucket, string key);
}