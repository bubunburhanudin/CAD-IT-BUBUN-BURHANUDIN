using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models
{
    public class DataTableResultModel
    {
        public DataTableResultModel()
        {
            data = new object();
            columns = new List<object>();
        }
        public List<object> columns { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
        public string error { get; set; }
    }
}
