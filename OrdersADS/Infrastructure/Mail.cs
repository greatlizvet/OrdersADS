using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;
using OrdersADS.Models;
using System.Net;

namespace OrdersADS.Infrastructure
{
    public class Mail
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<AppUser> Users { get; set; }

        public Mail(string title = "Неизвестно", string messageBody = "Неизвестно", IEnumerable<AppUser> users = null)
        {
            Title = title;
            Body = messageBody;
            Users = users;
        }

        public async Task Send(AppUser admin)
        {
            MailAddress from = new MailAddress(admin.Email.ToString());
            foreach(var u in Users)
            {
                var to = new MailAddress(u.Email.ToString());
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = Title;
                mail.Body = Body;
                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru", 587);
                smtpClient.Credentials = new NetworkCredential(admin.Email.ToString(), "rtvthjdj0227");
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}