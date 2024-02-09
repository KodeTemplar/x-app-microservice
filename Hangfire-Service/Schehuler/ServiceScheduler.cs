using Hangfire_Service.Service;

namespace Schehuler
{
    public class ServiceScheduler
    {
        private readonly ISendEmailService _sendEmailService;
        public ServiceScheduler(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }
        public async Task SendEmailAsync()
        {
            await _sendEmailService.SendEmail();
        }
    }
}
