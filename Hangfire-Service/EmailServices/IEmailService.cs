namespace EmailServices
{
    public interface IEmailService
    {
        void SendEmail(string toEmail, string toName, string Subject, string Message);
    }
}