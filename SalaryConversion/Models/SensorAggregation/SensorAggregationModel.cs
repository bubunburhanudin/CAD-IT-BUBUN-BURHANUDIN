using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models.SensorAggregation
{
    public class SensorAggregationModel
    {
        public int id { get; set; }
        public string roomarea { get; set; }
        public double timestamp { get; set; }
        public double mintemperature { get; set; }
        public double minhumidity { get; set; }
        public double maxtemperature { get; set; }
        public double maxhumidity { get; set; }
        public double avgtemperature { get; set; }
        public double avghumidity { get; set; }
        public double medtemperature { get; set; }
        public double medhumidity { get; set; }

        public class Array
        {
            public int id { get; set; }
            public string roomarea { get; set; }
            public double timestamp { get; set; }
            public double temperature { get; set; }
            public double humidity { get; set; }

        }
        public class Root
        {
            public List<Array> array { get; set; }
        }
    }
    
}
