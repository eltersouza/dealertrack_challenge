using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Dealertrack.Sales.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealertrack.Sales.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public List<Sale> GetSales()
        {
            List<Sale> items;
            items = _context.Sales.Take(1000).ToList();
            return items;
        }

        public List<SaleStatus> GetSaleStatuses()
        {
            List<SaleStatus> items;
            items = _context.SaleStatuses.Take(1000).ToList();
            return items;
        }

        public SaleRepository(SalesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Sale Add(Sale order)
        {
            _context.Entry(order.SaleStatus).State = EntityState.Unchanged;
            return _context.Sales.Add(order).Entity;
        }

        public SaleStatus Add(SaleStatus saleStatus)
        {
            return _context.SaleStatuses.Add(saleStatus).Entity;
        }

        public async Task<Sale> GetAsync(int orderId)
        {
            var sale = await _context.Sales.FindAsync(orderId);
            if (sale != null)
            {
                await _context.Entry(sale)
                    .Reference(i => i.SaleStatus).LoadAsync();
            }

            return sale;
        }

        public async Task<SaleStatus> GetSaleStatusAsync(int saleStatusId)
        {
            var saleStatus = await _context.SaleStatuses.FindAsync(saleStatusId);
            return saleStatus;
        }

        public bool Update(Sale order)
        {
            bool result = false;
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Update(SaleStatus saleStatus)
        {
            bool result = false;
            try
            {
                _context.Entry(saleStatus).State = EntityState.Modified;
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
