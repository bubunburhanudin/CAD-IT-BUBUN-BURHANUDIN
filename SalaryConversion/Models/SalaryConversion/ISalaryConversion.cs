using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models.SalaryConversion
{
    public interface ISalaryConversion
    {
        Task<DataTableResultModel> GetDataSalaryAsync(DataTableBindingModel Args, string salary);
    }
}
