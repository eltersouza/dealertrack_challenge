using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dealertrack.Sales.Application.Interfaces;
using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Dealertrack.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dealertrack.UI.Controllers
{
    public class SalesController : Controller
    {
        public SalesController(ISalesBusiness salesBusiness)
        {
            this._salesBusiness = salesBusiness;
        }
        private readonly ISalesBusiness _salesBusiness;

        static IEnumerable<SalesViewModel> Sales { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            if (files.Count == 0)
                return BadRequest("No file was uploaded.");

            IFormFile file = files.First();
            if(file.Length == 0)
                return BadRequest("The file was empty.");

            // full path to file in temp location
            string filePath = Path.GetTempFileName();

            var sales = _salesBusiness.ConvertCSVToSales(file.OpenReadStream());
            bool isPersisted = await _salesBusiness.PersistSales(sales);

            var salesVm = SalesViewModel.ConvertToViewModel(sales);
            return Ok(new { salesVm });
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                List<SalesViewModel> salesVm = SalesViewModel.ConvertToViewModel(_salesBusiness.GetSalesList());

                return Ok(new { salesVm });
            } catch(Exception ex)
            {
                return BadRequest("Something bad happened during the reading of the file.");
            }
        }
    }
}