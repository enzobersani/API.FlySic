using API.FlySic.Domain.Commands;
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
    }
}
