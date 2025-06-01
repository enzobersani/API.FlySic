using API.FlySic.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Commands
{
    public class AuthCommand : IRequest<AuthResponseModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
