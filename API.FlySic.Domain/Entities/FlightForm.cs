using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities.Base;
using API.FlySic.Domain.Enum;
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
        public FlightFormStatus Status { get; private set; } = FlightFormStatus.Aberta;

        public ICollection<FlightFormInterest> Interests { get; private set; } = new List<FlightFormInterest>();

        private FlightForm() { }

        public static FlightForm New(NewFlightFormCommand request)
        {
            var flightForm = new FlightForm();
            request.Adapt(flightForm);
            flightForm.DepartureDate = flightForm.DepartureDate.ToUniversalTime();
            flightForm.DepartureTime = flightForm.DepartureTime.ToUniversalTime();
            flightForm.ArrivalDate = flightForm.ArrivalDate.ToUniversalTime();
            flightForm.ArrivalTime = flightForm.ArrivalTime.ToUniversalTime();
            flightForm.Status = FlightFormStatus.Aberta;
            return flightForm;
        }

        public void Update(UpdateFlightFormCommand request)
        {
            DepartureDate = request.DepartureDate.ToUniversalTime();
            DepartureTime = request.DepartureTime.ToUniversalTime();
            DepartureAirport = request.DepartureAirport;
            DepartureManualLocation = request.DepartureManualLocation;

            ArrivalDate = request.ArrivalDate.ToUniversalTime();
            ArrivalTime = request.ArrivalTime.ToUniversalTime();
            ArrivalAirport = request.ArrivalAirport;
            ArrivalManualLocation = request.ArrivalManualLocation;

            AircraftType = request.AircraftType;
            FlightComment = request.FlightComment;
            HasOvernight = request.HasOvernight;
            SetUpdatedAt();
        }

        public void UpdateStatus(FlightFormStatus status)
        {
            Status = status;
            SetUpdatedAt();
        }
    }
}
