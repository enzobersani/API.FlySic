using API.FlySic.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Queries
{
    public class GetFlightInterestsQuery : IRequest<List<FlightInterestResponse>>
    {
        public Guid FlightFormId { get; set; }
    }
}
