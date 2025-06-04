using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Models
{
    public class AirportModel
    {
        public string CountryCode { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string IATA { get; set; } = string.Empty;
        public string ICAO { get; set; } = string.Empty;
        public string Airport { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}
