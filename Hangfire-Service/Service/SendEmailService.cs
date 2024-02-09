using EmailServices;

namespace Hangfire_Service.Service
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IEmailService _emailService;
        public SendEmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmail()
        {
            try
            {
                //FETCH RECORDS FROM THE DATABASE THAT HAVE INCOMPLETE ORDER
                List<string> emails = new List<string>
                {
                    "john.doe@example.com",
                    "jane.smith@example.com",
                    "michael.jackson@example.com",
                    "emily.wilson@example.com",
                    "alexander.graham@example.com",
                    "samantha.white@example.com",
                    "peter.parker@example.com",
                    "olivia.brown@example.com",
                    "david.jones@example.com",
                    "sophia.robinson@example.com"
                };
                string body = $"We want to remind you that your order is still pending completion on our platform";
                foreach (var mail in emails)
                {
                    try
                    {
                        _emailService.SendEmail(mail, "X-App", "Hangfire Test Service", body);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Email wasn't successful for {mail}.");
                    }

                }
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
