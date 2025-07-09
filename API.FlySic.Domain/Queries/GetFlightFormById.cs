using API.FlySic.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Queries
{
    public class GetFlightFormById : IRequest<SearchFlightFormsResponseModel>
    {
        public Guid FlightFormId { get; set; }
        public GetFlightFormById(Guid flightFormId)
        {
            FlightFormId = flightFormId;
        }
    }
}
