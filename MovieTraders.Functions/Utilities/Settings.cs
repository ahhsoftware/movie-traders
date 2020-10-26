using MovieTraders.Functions.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTraders.Functions.Utilities
{
    public class Settings
    {

        public static EmailSettings EmailSettings
        {
            get
            {
                return new EmailSettings
                {
                    FromEmail = Environment.GetEnvironmentVariable(nameof(EmailSettings.FromEmail)),
                    FromName = Environment.GetEnvironmentVariable(nameof(EmailSettings.FromName)),
                    Password = Environment.GetEnvironmentVariable(nameof(EmailSettings.Password)),
                    Host = Environment.GetEnvironmentVariable(nameof(EmailSettings.Host)),
                    Port = Convert.ToInt32(Environment.GetEnvironmentVariable(nameof(EmailSettings.Port))),
                    SiteUrl = Environment.GetEnvironmentVariable(nameof(EmailSettings.SiteUrl))
                };
            }
        }

        public static string ExtendedConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable(nameof(ExtendedConnectionString));
            }

        }

        public static string CrudConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable(nameof(CrudConnectionString));
            }

        }

        public static string TokenString
        {
            get
            {
                return Environment.GetEnvironmentVariable(nameof(TokenString));
            }
        }
    }
}
