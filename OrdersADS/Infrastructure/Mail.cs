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
        }
    }
}