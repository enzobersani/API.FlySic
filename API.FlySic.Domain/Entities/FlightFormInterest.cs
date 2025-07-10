using API.FlySic.Domain.Entities.Base;
using API.FlySic.Domain.Enum;
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
        public FlightFormInterestStatus Status { get; private set; } = FlightFormInterestStatus.Pending;

        private FlightFormInterest() { }

        public static FlightFormInterest Create(Guid flightFormId, Guid interestedUserId)
        {
            return new FlightFormInterest
            {
                FlightFormId = flightFormId,
                InterestedUserId = interestedUserId,
                Status = FlightFormInterestStatus.Pending
            };
        }

        public void Accept()
        {
            Status = FlightFormInterestStatus.Accepted;
        }
        public void Reject()
        {
            Status = FlightFormInterestStatus.Rejected;
        }
    }
}
