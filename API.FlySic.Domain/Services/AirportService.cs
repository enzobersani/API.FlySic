using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.ModelMap;
using API.FlySic.Domain.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Services
{
    public class AirportService : IAirportService
    {
        private readonly List<AirportModel> _airports;

        public AirportService(string csvFilePath)
        {
            _airports = LoadAirportsFromCsv(csvFilePath);
        }

        public List<AirportModel>? GetAirports(string icao)
            => _airports.Where(x => x.ICAO.Contains(icao, StringComparison.OrdinalIgnoreCase)).ToList();
            //=> _airports.Where(x => x.ICAO.Equals(icao, StringComparison.OrdinalIgnoreCase)).ToList();
        

        #region Private Methods
        private List<AirportModel> LoadAirportsFromCsv(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            csv.Context.RegisterClassMap<AirportModelMap>();
            return csv.GetRecords<AirportModel>().ToList();
        }
        #endregion
    }
}
