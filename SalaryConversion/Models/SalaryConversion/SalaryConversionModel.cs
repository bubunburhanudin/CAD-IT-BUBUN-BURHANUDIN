using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models.SalaryConversion
{
    public class SalaryConversionModel
    {
        public int id { get; set; }        
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public double salaryidr { get; set; }
        public double salaryusd { get; set; }
        public useraddress address { get; set; }
        public geo location { get; set; }
        public company usercompany { get; set; }

        public class useraddress
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }

        }
        public class geo
        {
            public string lat { get; set; }
            public string lng { get; set; }

        }
        public class company
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }

        }
        public class Array
        {
            public int id { get; set; }
            public double salaryInIDR { get; set; }            

        }
        public class Root
        {
            public List<Array> array { get; set; }
        }
    }
}
