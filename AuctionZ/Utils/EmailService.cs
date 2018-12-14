using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace AuctionZ.Utils
{
    public class EmailService : IEmailSender
    {
        public EmailService()
        {
            
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("AuctionZ", "z@qnet.li"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.host",25, false); //smtp.host/port/turn_on_ssl
                await client.AuthenticateAsync("login", "password"); //login/password
                try
                {
                    await client.SendAsync(emailMessage);
                }
                catch (Exception) { }
                finally
                {
                   await client.DisconnectAsync(true);
                }
            }
        }
    }
}
