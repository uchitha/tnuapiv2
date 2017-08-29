using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace WebApi.Services
{
    public interface IEmailService
    {
        Task SendSingleEmail(string from, string subject, string plainTextContent, string htmlContent = null);
    }
}
