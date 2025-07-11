using API.FlySic.Domain.Models.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Commands
{
    public class AcceptFlightInterestCommand : IRequest<BaseResponse>
    {
        public Guid FlightFormId { get; set; }
        public Guid InterestId { get; set; }
        public Guid PilotId { get; set; }
    }
}
