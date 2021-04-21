using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Models.SensorAggregation
{
   public interface ISensorAggregation
    {
        DataTableResultModel GetDataSensor(DataTableBindingModel Args, string sensor);
    }
}
