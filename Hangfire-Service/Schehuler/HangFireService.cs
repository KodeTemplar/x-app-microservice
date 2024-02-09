using Hangfire;

namespace Schehuler
{
    public class HangFireService
    {
        [AutomaticRetry(Attempts = 10)]
        public static void InitializeService()
        {
            const string jobId = "483dbab9d666-523abfe6c2c6b781216G";

            RecurringJob.AddOrUpdate<ServiceScheduler>(
                jobId,
                x => x.SendEmailAsync(),
                Cron.Minutely());
        }
    }
}