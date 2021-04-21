using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models
{
    public class DataTableColumnBindingModel
    {
        public DataTableColumnBindingModel()
        {
            search = new DataTableSearchBindingModel();
        }
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DataTableSearchBindingModel search { get; set; }
    }
}
