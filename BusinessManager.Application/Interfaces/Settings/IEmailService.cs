using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Interfaces.Settings
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string toName, string subject, string plainTextContent, string htmlContent);
        Task SendLoginEmailAsync(string email, string firstName, string lastName, string password);
    }
}
