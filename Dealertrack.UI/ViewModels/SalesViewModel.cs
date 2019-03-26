using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Dealertrack.UI.ViewModels
{
    public class SalesViewModel
    {
        [Display(Name ="Deal Number")]
        [Required]
        public int DealNumber { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        public string CustomerName { get; set; }

        [Display(Name = "Dealership Name")]
        [Required]
        public string DealershipName { get; set; }

        [Required]
        public string Vehicle { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string priceFormated {
            get
            {
                return string.Format("CAD{0}", Price.ToString("C", CultureInfo.CreateSpecificCulture("en-CA")));
            }
        }

        [Required]
        public DateTime Date { get; set; }
        public string DateFormated
        {
            get
            {
                return Date.ToString("dd/MM/yyyy");
            }
        }

        public static SalesViewModel ConvertToViewModel(Sale sale)
        {
            SalesViewModel vm = new SalesViewModel();
            vm.DealNumber = sale.DealNumber;
            vm.CustomerName = sale.CustomerName;
            vm.DealershipName = sale.DealershipName;
            vm.Vehicle = sale.Vehicle;
            vm.Price = sale.Price;
            vm.Date = sale.Date;

            return vm;
        }

        public static List<SalesViewModel> ConvertToViewModel(List<Sale> saleList)
        {
            List<SalesViewModel> listVm = new List<SalesViewModel>();
            saleList.ForEach(sale => {
                SalesViewModel vm = new SalesViewModel();
                vm.DealNumber = sale.DealNumber;
                vm.CustomerName = sale.CustomerName;
                vm.DealershipName = sale.DealershipName;
                vm.Vehicle = sale.Vehicle;
                vm.Price = sale.Price;
                vm.Date = sale.Date;

                listVm.Add(vm);
            });

            return listVm;
        }
    }
}
