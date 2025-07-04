using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Commands
{
    public class RecoveryCodeCommand : IRequest<Unit>
    {
        public string Email { get; set; } = string.Empty;
    }
}
