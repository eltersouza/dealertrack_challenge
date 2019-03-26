using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dealertrack.Sales.Application.Interfaces
{
    public interface ISalesBusiness
    {
        List<Sale> ConvertCSVToSales(Stream stream);
        Task<bool> PersistSales(List<Sale> sales);
        List<Sale> GetSalesList();
    }
}
