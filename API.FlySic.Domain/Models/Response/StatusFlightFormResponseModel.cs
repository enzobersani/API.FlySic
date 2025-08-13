using API.FlySic.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Models.Response
{
    public class StatusFlightFormResponseModel
    {
        public FlightFormStatus Status { get; set; }
        public Guid? EvaluatedId { get; set; }
    }
}
