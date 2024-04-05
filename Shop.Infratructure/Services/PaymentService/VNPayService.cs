using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shop.Domain.Options;
using Shop.Infratructure.Services.PaymentService;
using VNPAY_CS_ASPX;

namespace Shop.Infratructure.Services.VnPaySevice;

public class VNPayService:IVnPayService
{
    private readonly IOptions<VnPayOptions> _options;
    private readonly VnPayLibrary _vnPayLibrary = new VnPayLibrary();
    public VNPayService(IOptions<VnPayOptions> options)
    {
        _options = options;
    }

    public string GenerateUrl(double amount, string message,HttpContext context)
    {
        var vnpay = _vnPayLibrary;
        
        vnpay.AddRequestData("vnp_Version", _options.Value.vnp_Version);
        vnpay.AddRequestData("vnp_Command", _options.Value.vnp_Command);
        vnpay.AddRequestData("vnp_TmnCode", _options.Value.vnp_TmnCode);
        vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
        vnpay.AddRequestData("vnp_BankCode", "");
        vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        vnpay.AddRequestData("vnp_CurrCode", "VND");
        vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
        vnpay.AddRequestData("vnp_Locale", "vn");
        vnpay.AddRequestData("vnp_OrderInfo", message);
        vnpay.AddRequestData("vnp_OrderType", "billpayment"); //default value: other
        vnpay.AddRequestData("vnp_ReturnUrl", _options.Value.vnp_ReturnUrl);
        vnpay.AddRequestData("vnp_TxnRef", Random.Shared.Next(1000,1000000).ToString()); // Mã tham chiếu của giao dịch tại hệ th
        
        var requestUrl = vnpay.CreateRequestUrl(_options.Value.RequestUri, _options.Value.vnp_HashSecret);

        return requestUrl;
    }

    public void AddResponse(string key, string value)
    {
        _vnPayLibrary.AddResponseData(key,value);
    }

    public bool CheckHash(string inputHash, string secretKey)
    {
        return _vnPayLibrary.ValidateSignature(inputHash, secretKey);
    }
}