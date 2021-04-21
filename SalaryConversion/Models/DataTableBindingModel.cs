using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models
{
    public class DataTableBindingModel
    {
        public DataTableBindingModel()
        {
            order = new List<DataTableOrderBindingModel>();
            search = new DataTableSearchBindingModel();
            columns = new List<DataTableColumnBindingModel>();
        }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public List<DataTableOrderBindingModel> order { get; set; }
        public DataTableSearchBindingModel search { get; set; }
        public List<DataTableColumnBindingModel> columns { get; set; }
    }
}
