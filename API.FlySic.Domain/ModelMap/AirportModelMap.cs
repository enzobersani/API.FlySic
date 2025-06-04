using API.FlySic.Domain.Models;
using CsvHelper.Configuration;

namespace API.FlySic.Domain.ModelMap
{
    public class AirportModelMap : ClassMap<AirportModel>
    {
        public AirportModelMap()
        {
            Map(m => m.CountryCode).Name("country_code");
            Map(m => m.RegionName).Name("region_name");
            Map(m => m.IATA).Name("iata");
            Map(m => m.ICAO).Name("icao");
            Map(m => m.Airport).Name("airport");
            Map(m => m.Latitude).Name("latitude");
            Map(m => m.Longitude).Name("longitude");
        }
    }
}
