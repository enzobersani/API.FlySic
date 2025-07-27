using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities.Base;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Entities
{
    public class FlightRating : BaseEntity
    {
        public Guid FlightFormId { get; private set; }
        public FlightForm FlightForm { get; private set; } = null!;
        public Guid EvaluatorId { get; private set; }
        public User Evaluator { get; private set; } = null!;
        public Guid EvaluatedId { get; private set; }
        public User Evaluated { get; private set; } = null!;
        public int Rating { get; private set; }
        public string? Comment { get; private set; }

        private FlightRating() { }

        public static FlightRating New(FinishFlightFormCommand request)
        {
            var flightRating = new FlightRating();
            request.Adapt(flightRating);
            return flightRating;
        }
    }
}
