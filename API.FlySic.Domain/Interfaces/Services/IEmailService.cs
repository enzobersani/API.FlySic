using API.FlySic.Domain.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendNewUserEmailAsync(string toEmail, NewUserCommand form);
        Task SendEmailAsync(string toEmail, string subject, string htmlBody, string textBody = "", IFormFile? attachment = null);
    }
}
