using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalaryConversion.Models;
using SalaryConversion.Models.SalaryConversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryConversion.Controllers
{

    [Route("salary-conversion")]
    public class SalaryConversionController : Controller
    {
        private readonly ISalaryConversion _Interface;
        private readonly ILogger<SalaryConversionController> _Logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SalaryConversionController(ILogger<SalaryConversionController> logger, ISalaryConversion intface, IWebHostEnvironment hostingEnvironment)
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
        public async Task<IActionResult> GetDataSourceAsync(DataTableBindingModel Args)
        {
            var salaryFile = Path.Combine(Directory.GetCurrentDirectory(), _hostingEnvironment.WebRootPath, "sourcefile/salary_data.json");
            string JSON = System.IO.File.ReadAllText(salaryFile);
          
            return Json(await _Interface.GetDataSalaryAsync(Args, JSON));
        }
    }
}
