namespace MovieTraders.Functions.Notifications
{
    public class EmailBuilder
    {
        private const string TradeRequest = @"
<html>
    <head></head>
    <body>
        <p>Hello {{requestUserName}}! </p>
        <p>A request has been made to trade movies with {{responseUserName}}. </p>
        <br/>
        <p>Please take a minute to review the request.</p>
        <br/>
        <p>Sincerely, <br/>The Movie Traders</p>
    </body>
</html>";

        private string GetTradeRequestEmail(string requestUserName, string responseUserName)
        {
            string result = TradeRequest;
            result = result.Replace("{{requestUserName}}", requestUserName);
            result = result.Replace("{{responseUserName}}", responseUserName);
            return result;
        }

        private const string TradeOver = @"
<html>
    <head></head>
    <body>
        <p>Hello {{name}}! </p>
        <p>Looks like the movie trade between {{requestUser}} and {{responseUser}} is expiring. </p>
        <br/>
        <p>Please take a minute to make arrangements to reverse the trade.</p>
        <br/>
        <p>Sincerely, <br/>The Movie Traders</p>
    </body>
</html>";

        public static string GetTraderOverEmail(string requestUser, string responseUser)
        {
            string result = TradeOver;
            result = result.Replace("{{requestUser}}", requestUser);
            result = result.Replace("{{responseUser}}", responseUser);
            return result;
        }
        private const string InvitationTemplate = @"
<html>
    <head></head>
    <body>
        <p>Hello {{userName}}! </p>
        <p>An account was created for you at <a href='{{url}}'>The Movie Traders</a></p>
        <p>You can login to your new account with the following credentials:</p>
        <p><b>Email:</b><br/>{{userEmail}}</p>
        <p><b>Password:</b><br/>{{password}}</p>
        <p>Once you have successfully logged in, you will be prompted to change your password.</p>
        <p>Sincerely, <br/>The Movie Traders</p>
    </body>
</html>
";
        public static string GetInvitationEmail(string userName, string url, string userEmail, string password)
        {
            string result = InvitationTemplate;
            result = result.Replace("{{userName}}", userName);
            result = result.Replace("{{url}}", url);
            result = result.Replace("{{userEmail}}", userEmail);
            result = result.Replace("{{password}}", password);
            return result;
        }

        private const string ForgotPasswordTemplate = @"
<html>
    <head></head>
    <body>
        <p>Hello {{name}}! </p>
        <p>A rest password request was received for your Movie Traders account. Please contact us at: {{fromEmail}} if you did not initiate this process.</p>
        <p>You can log in with the following credentials.</p>        
        <p><b>Email:</b><br/>{{userEmail}}</p>
        <p><b>Password:</b><br/>{{password}}</p>
        <p>Once you have successfully logged in, you will be prompted to change your password.</p>
        <p>Sincerely, <br/>The Movie Traders</p>
    </body>
</html>
";
        public static string GetForgotPasswordEmail(string fromEmail, string userEmail, string name, string password )
        {
            string result = ForgotPasswordTemplate;
            result = result.Replace("{{fromEmail}}", fromEmail);
            result = result.Replace("{{userEmail}}", userEmail);
            result = result.Replace("{{name}}", name);
            result = result.Replace("{{password}}", password);
            return result;
        }
    }
}
