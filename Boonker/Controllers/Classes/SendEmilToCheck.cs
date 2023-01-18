using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Boonker.Controllers.Classes
{
    public class SendEmilToCheck
    {
        public void SendEmail(string to, int shortcode)
        {
            using(MailMessage message = new MailMessage(
                            new MailAddress("rasulqwertter@mail.ru"), 
                            new MailAddress(to)))
            {

                message.Subject = "Good morning, Your Boonker's account shortcode";
                message.Body = shortcode.ToString();

                SmtpClient client = new SmtpClient("smtp.mail.ru"){
                    Credentials = new NetworkCredential("rasulqwertter@mail.ru", "NdubBVDvbBSQenkSkAvu"),
                    EnableSsl = true
                };
                try {
                    client.Send(message);
                }
                catch (Exception e){
                    Console.WriteLine(e);
                }
            };
        }
    }
}
