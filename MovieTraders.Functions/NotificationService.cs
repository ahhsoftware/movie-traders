using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MovieTraders.Functions
{
    public static class NotificationService
    {
        [FunctionName("NotificationService")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            //var output = ServiceFactory.AuthService(log).Notifications();
            //if (output.ReturnValue == Data.Auth.Models.NotificationsOutput.Returns.NoRecords)
            //{
            //    return;
            //}
            //var result = output.ResultData;
            //foreach (var record in result)
            //{
            //    EmailService service = new EmailService(Settings.EmailSettings);
            //    service.Send(new EmailMessage
            //    {
            //        Email = record.Email,
            //        Name = record.Name,
            //        Subject = "LL Game Update Alert",
            //        MessageHtml = EmailBuilder.GetMatchOverEmail(record.Name, record.HomeTeam, record.AwayTeam, record.Link)
            //    });
            //}
        }
    }
}
