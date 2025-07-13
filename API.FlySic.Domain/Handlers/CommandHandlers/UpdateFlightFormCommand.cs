using API.FlySic.Domain.Models.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Handlers.CommandHandlers
{
    public class UpdateFlightFormCommand : IRequest<BaseUpdateResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("partidaData")]
        public DateTime DepartureDate { get; set; }

        [JsonPropertyName("partidaHora")]
        public DateTime DepartureTime { get; set; }

        [JsonPropertyName("aeroportoPartida")]
        public string? DepartureAirport { get; set; }

        [JsonPropertyName("localPartidaManual")]
        public string? DepartureManualLocation { get; set; }

        [JsonPropertyName("chegadaData")]
        public DateTime ArrivalDate { get; set; }

        [JsonPropertyName("chegadaHora")]
        public DateTime ArrivalTime { get; set; }

        [JsonPropertyName("aeroportoChegada")]
        public string? ArrivalAirport { get; set; }

        [JsonPropertyName("localChegadaManual")]
        public string? ArrivalManualLocation { get; set; }

        [JsonPropertyName("tipoAeronave")]
        public string AircraftType { get; set; } = string.Empty;

        [JsonPropertyName("comentarioVoo")]
        public string? FlightComment { get; set; }

        [JsonPropertyName("checkboxPernoite")]
        public bool HasOvernight { get; set; }
    }
}
