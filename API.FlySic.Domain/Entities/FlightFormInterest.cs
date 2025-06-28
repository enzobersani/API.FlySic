using API.FlySic.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Entities
{
    public class FlightFormInterest : BaseEntity
    {
        public Guid FlightFormId { get; private set; }
        public FlightForm FlightForm { get; private set; } = null!;
        public Guid InterestedUserId { get; private set; }
        public User InterestedUser { get; private set; } = null!;

        private FlightFormInterest() { }

        public static FlightFormInterest Create(Guid flightFormId, Guid interestedUserId)
        {
            return new FlightFormInterest
            {
                FlightFormId = flightFormId,
                InterestedUserId = interestedUserId
            };
        }
    }
}
