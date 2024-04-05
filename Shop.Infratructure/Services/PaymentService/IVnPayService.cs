using Microsoft.AspNetCore.Http;

namespace Shop.Infratructure.Services.PaymentService;

public interface IVnPayService
{
    public string GenerateUrl(double amount, string message, HttpContext context);

    public void AddResponse(string key, string value);

    public bool CheckHash(string inputHash, string secretKey);
}