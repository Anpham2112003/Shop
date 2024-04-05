namespace Shop.Infratructure.Services;

public static class EmailTemplate
{
    public static string VerifyEmail(string jwtCode)
    {
        return $@"
        <html>
             <h1>Verify Account</h1>
            <a href=`https://localhost:7052/api/auth/verify/account/{jwtCode}`>Click here</a>
        </html>";
    }
    
    public static string ResetPassword(string number)
    {
        return $@"
        <html>
             <h1> Code </h1>
             <p> code verify password {number} </p>
        </html>";
    }
}