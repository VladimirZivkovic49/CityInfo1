using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Services
{
    public class LocalMailService:IMailService
    {
        private string _mailTo = Startup.Configuration  ["mailSettings:mailToAdress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
        public void Send(string subject, string message)
        {
            // send mail - output debug window

            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService ");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Subject {message}");

        }
    }
}
