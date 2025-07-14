using API.FlySic.Domain.Entities.Base;
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

        public FlightRating(Guid flightFormId, Guid evaluatorId, Guid evaluatedId, int rating, string? comment)
        {
            FlightFormId = flightFormId;
            EvaluatorId = evaluatorId;
            EvaluatedId = evaluatedId;
            Rating = rating;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
