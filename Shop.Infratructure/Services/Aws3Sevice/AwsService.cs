using System.Net;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Shop.Infratructure.Services.Aws3Service;

public class AwsService:IAwsSevice
{
    private readonly IAmazonS3 _amazonS3;

    public AwsService(IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
    }

    public async Task Upload(IFormFile file,string bucket, string key,CancellationToken cancellationToken)
    {
        try
        {
            
              var request = new PutObjectRequest()
              {
                  BucketName = bucket,
                  InputStream = file.OpenReadStream(),
                  Key = key,
                  CannedACL = S3CannedACL.PublicRead,
                  AutoCloseStream = true
              };
              await _amazonS3.PutObjectAsync(request,cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception("Cant upload file");
        }
      
    }


    public async Task<bool> Delete(string bucket,string key)
    {
        try
        {
            var result=await  _amazonS3.DeleteObjectAsync(bucket, key);

            return result.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            throw new Exception("Cant delete file ");
        }

    

    }
}