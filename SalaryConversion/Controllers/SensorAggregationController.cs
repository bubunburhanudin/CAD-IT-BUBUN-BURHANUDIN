using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalaryConversion.Models;
using SalaryConversion.Models.SensorAggregation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Controllers
{
    [Route("sensor-aggregation")]
    public class SensorAggregationController : Controller
    {
        private readonly ISensorAggregation _Interface;
        private readonly ILogger<SensorAggregationController> _Logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SensorAggregationController(ILogger<SensorAggregationController> logger, ISensorAggregation intface, IWebHostEnvironment hostingEnvironment)
        {
            _Interface = intface;
            _Logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("get-datasource")]
        public IActionResult GetDataSourceAsync(DataTableBindingModel Args)
        {
            var sensorFile = Path.Combine(Directory.GetCurrentDirectory(), _hostingEnvironment.WebRootPath, "sourcefile/sensor_data.json");
            string JSON = System.IO.File.ReadAllText(sensorFile);

            return Json(_Interface.GetDataSensor(Args, JSON));
        }
    }
}
