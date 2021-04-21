using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SalaryConversion.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalaryConversion.Models.SalaryConversion
{
    public class SalaryConversionDAL: ISalaryConversion
    {
        public SalaryConversionDAL(IConfiguration configuration)
        {            
            UserWebApi = configuration.GetValue<string>("WebApi:Users");
            ConvertWebApi = configuration.GetValue<string>("WebApi:CurrencyConverterAPI");
            ConvertWebKey = configuration.GetValue<string>("WebApi:CurrencyConverterKey");
        }
        public string UserWebApi { get; }
        public string ConvertWebApi { get; }
        public string ConvertWebKey { get; }

        public async Task<DataTableResultModel> GetDataSalaryAsync(DataTableBindingModel Args, string salary)
        {
            List<SalaryConversionModel> Result = new List<SalaryConversionModel>();
            int RecordsTotal = 0;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(UserWebApi))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        double usd =  ExchangeRate(CurrencyConverterEnum.IDR, CurrencyConverterEnum.USD);
                                                
                        Result = JsonConvert.DeserializeObject<List<SalaryConversionModel>>(apiResponse);
                        RecordsTotal = Result.Count;
                        SalaryConversionModel.Root salaryObj = JsonConvert.DeserializeObject<SalaryConversionModel.Root>(salary);
                        for (int i = 0; i < Result.Count; i++)
                        {
                            int idUser = Result[i].id;
                            for (int ii = 0; ii < salaryObj.array.Count; ii++)
                            {
                                int idSalary = salaryObj.array[ii].id;
                                if (idUser == idSalary)
                                {
                                    Result[i].salaryidr = salaryObj.array[ii].salaryInIDR;
                                    Result[i].salaryusd = salaryObj.array[ii].salaryInIDR * usd;
                                    break;
                                }
                            }
                        }                        
                    }
                }
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

        public double ExchangeRate(CurrencyConverterEnum from, CurrencyConverterEnum to)
        {
            string url;
            url = ConvertWebApi + "convert?q=" + from + "_" + to + "&compact=y&apiKey=" + ConvertWebKey;
            var jsonString = GetResponse(url);
            return JObject.Parse(jsonString).First.First["val"].ToObject<double>();
        }
        private static string GetResponse(string url)
        {
            string jsonString;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }
            return jsonString;
        }
    }
}
