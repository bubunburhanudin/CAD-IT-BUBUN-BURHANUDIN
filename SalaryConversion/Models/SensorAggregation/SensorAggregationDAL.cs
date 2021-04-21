using Newtonsoft.Json;
using SalaryConversion.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SalaryConversion.Models.SensorAggregation
{
    public class SensorAggregationDAL: ISensorAggregation
    {
        public DataTableResultModel GetDataSensor(DataTableBindingModel Args, string sensor)
        {
            List<SensorAggregationModel> Result = new List<SensorAggregationModel>();
            int RecordsTotal = 0;
            try
            {
                SensorAggregationModel.Root sensorObj = JsonConvert.DeserializeObject<SensorAggregationModel.Root>(sensor);
                DataTable dt = DataTableBL.ToDataTable(sensorObj.array);
                DataView view = new DataView(dt);
                DataTable dtroomArea = view.ToTable(true, "roomarea");

                for (int i=0; i < dtroomArea.Rows.Count; i++)
                {
                    double mintemp, maxtemp, avgtemp, medtemp;
                    double minhum, maxhum, avghum, medhum;
                    string roomarea = dtroomArea.Rows[i]["roomarea"].ToString();


                    DataTable dtbak = new DataTable();
                    dtbak = dt.Clone();
                    DataRow[] dr = dt.Select("roomarea = '" + roomarea + "'");
                    foreach (DataRow row in dr)
                    {
                        dtbak.ImportRow(row);
                    }
                    
                    mintemp = Convert.ToDouble(dtbak.AsEnumerable().Min(row => row["temperature"]));
                    maxtemp = Convert.ToDouble(dtbak.AsEnumerable().Max(row => row["temperature"]));
                    avgtemp = Convert.ToDouble(dtbak.AsEnumerable().Average(row => Convert.ToDouble(row["temperature"])));

                    minhum = Convert.ToDouble(dtbak.AsEnumerable().Min(row => row["humidity"]));
                    maxhum = Convert.ToDouble(dtbak.AsEnumerable().Max(row => row["humidity"]));
                    avghum = Convert.ToDouble(dtbak.AsEnumerable().Average(row => Convert.ToDouble(row["humidity"])));


                    dtbak.Columns.Remove("id");
                    dtbak.Columns.Remove("roomarea");
                    dtbak.AcceptChanges();

                    double[] tempvalues = Array.ConvertAll<DataRow, double>(
                    dtbak.Select(),
                    delegate (DataRow row)
                    {
                        return (double)row["temperature"];
                    });

                    double[] humvalues = Array.ConvertAll<DataRow, double>(
                    dtbak.Select(),
                    delegate (DataRow row)
                    {
                       return (double)row["humidity"];
                    });

                    medtemp = GetMedianFromArray(tempvalues);
                    medhum = GetMedianFromArray(humvalues);


                    Result.Add(new SensorAggregationModel
                    {
                        roomarea = roomarea,
                        mintemperature = mintemp,
                        maxtemperature = maxtemp,
                        avgtemperature = avgtemp,
                        medtemperature = medtemp,
                        minhumidity = minhum,
                        maxhumidity = maxhum,
                        avghumidity = avghum,
                        medhumidity = medhum
                    });

                }

                RecordsTotal = Result.Count;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
            return new DataTableResultModel()
            {
                data = Result,
                draw = Args.draw,
                recordsTotal = RecordsTotal,
                recordsFiltered = RecordsTotal,
                error = null
            };
        }       
        public double GetMedianFromArray(double[] values)
        {
            double median;
            Array.Sort(values);
            if (values.Length % 2 != 0)
            {
                median = values[values.Length / 2];
            }
            else
            {
                int middle = values.Length / 2;
                double first = values[middle];
                double second = values[middle - 1];
                median = (first + second) / 2;
            }
            return median;
        }
    }
}
