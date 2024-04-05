namespace Shop.Domain.Options;

public class VnPayOptions
{
    public const string Method = "VNPAY";
    
    public string vnp_Version { get; set; }
    
    public string vnp_Command { get; set; }
    
    public string vnp_Locale { get; set; }
    
    public string vnp_CurrCode { get; set; }
    
    public string vnp_ReturnUrl { get; set; }
    
    public string RequestUri { get; set; }
    
    public string vnp_ExpireDate { get; set; }
    public string vnp_TmnCode { get; set; }
    public string vnp_HashSecret { get; set; }
}