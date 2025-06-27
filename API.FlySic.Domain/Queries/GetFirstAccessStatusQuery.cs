using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Queries
{
    public class GetFirstAccessStatusQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public GetFirstAccessStatusQuery(Guid id)
        {
            UserId = id;
        }
    }
}
