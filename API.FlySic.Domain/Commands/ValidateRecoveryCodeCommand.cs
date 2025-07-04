using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Commands
{
    public class ValidateRecoveryCodeCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
        public string RecoveryCode { get; set; } = string.Empty;
    }
}
