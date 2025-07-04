using API.FlySic.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Queries
{
    public class SearchFlightFormsQuery : IRequest<List<SearchFlightFormsResponseModel>>
    {
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? DepartureLocation { get; set; }
        public string? ArrivalLocation { get; set; }
    }
}
