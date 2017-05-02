using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:MailToAdress"];
        private string _mailFrom = Startup.Configuration["mailSettings:MailFromAdress"];
        public void Send(string subject, string message)
        {
            // send mail - output debug window

            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CloudMailService ");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Subject {message}");
        }
    }
}
