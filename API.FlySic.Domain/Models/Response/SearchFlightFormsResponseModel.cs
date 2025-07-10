using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Models.Response
{
    public class SearchFlightFormsResponseModel
    {
        public Guid Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DepartureTime { get; set; }
        public string? DepartureAirport { get; set; }
        public string? DepartureManualLocation { get; set; }

        public DateTime ArrivalDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string? ArrivalAirport { get; set; }
        public string? ArrivalManualLocation { get; set; }

        public string AircraftType { get; set; } = string.Empty;
        public bool HasOvernight { get; set; }
        public string? FlightComment { get; set; }
        public bool UserAlreadyInterested { get; set; }
        public PilotResponseModel Pilot { get; set; } = null!;
    }
}
