﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WebApi.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;
        private ISendGridClient _sendgridClient;
       

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _sendgridClient = new SendGridClient(_configuration.GetValue<string>("SendgridApiKey"));
        }

        public async Task<bool> SendSingleEmail(string from, string subject,string plainTextContent, string htmlContent = null)
        {

            var fromEmail = new EmailAddress(from);
            var adminEmails = _configuration.GetValue<string>("Emails:KidsCodeAdmin").Split(',');

            int sentCount = 0;
            foreach (var email in adminEmails)
            {
                var toEmail = new EmailAddress(email, "Kids Code Admin");

                var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, plainTextContent, htmlContent);

                var response = await _sendgridClient.SendEmailAsync(msg);

                Trace.TraceInformation($"Response received from sendgrid : {response.StatusCode}");

                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    sentCount++;
                }
            }

            //return sentCount == adminEmails.Length;
            return true;
        }
    }
}
