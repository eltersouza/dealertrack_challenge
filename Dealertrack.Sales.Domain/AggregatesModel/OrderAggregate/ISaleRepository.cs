using Dealertrack.Sales.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate
{
    public interface ISaleRepository : IRepository<Sale>
    {
        List<Sale> GetSales();
        List<SaleStatus> GetSaleStatuses();

        Sale Add(Sale order);
        bool Update(Sale order);
        Task<Sale> GetAsync(int orderId);

        SaleStatus Add(SaleStatus saleStatus);
        bool Update(SaleStatus saleStatus);
        Task<SaleStatus> GetSaleStatusAsync(int saleStatusId);
    }
}
