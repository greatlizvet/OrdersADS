using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;
using OrdersADS.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace OrdersADS.Infrastructure
{
    public class Mail
    {
        public async Task Send(string email, string title, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация", "laleshk@mail.ru"));
            emailMessage.To.Add(new MailboxAddress(" ", email));
            emailMessage.Subject = title;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync("laleshk@mail.ru", "rtvthjdj0227");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}