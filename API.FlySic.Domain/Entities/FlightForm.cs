using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities.Base;
using Mapster;

namespace API.FlySic.Domain.Entities
{
    public class FlightForm : BaseEntity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public DateTime DepartureDate { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public string? DepartureAirport { get; private set; }
        public string? DepartureManualLocation { get; private set; }

        public DateTime ArrivalDate { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public string? ArrivalAirport { get; private set; }
        public string? ArrivalManualLocation { get; private set; }

        public string AircraftType { get; private set; } = string.Empty;
        public string? FlightComment { get; private set; }
        public bool HasOvernight { get; private set; }

        private FlightForm() { }

        public static FlightForm New(NewFlightFormCommand request)
        {
            var flightForm = new FlightForm();
            request.Adapt(flightForm);
            flightForm.DepartureDate = flightForm.DepartureDate.ToUniversalTime();
            flightForm.DepartureTime = flightForm.DepartureTime.ToUniversalTime();
            flightForm.ArrivalDate = flightForm.ArrivalDate.ToUniversalTime();
            flightForm.ArrivalTime = flightForm.ArrivalTime.ToUniversalTime();
            return flightForm;
        }
    }
}
