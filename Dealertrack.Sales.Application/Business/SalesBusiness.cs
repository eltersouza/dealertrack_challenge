using Dealertrack.Sales.Application.Interfaces;
using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Dealertrack.Sales.Infrastructure.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dealertrack.Sales.Application.Business
{
    public class SalesBusiness : ISalesBusiness
    {
        public SalesBusiness() { }
        public SalesBusiness(ISaleRepository saleRepository, CSVConverter converter)
        {
            _saleRepository = saleRepository;
            Converter = converter;
        }

        private ISaleRepository _saleRepository { get; }
        private CSVConverter Converter { get; }

        public List<Sale> ConvertCSVToSales(Stream stream)
        {
            List<Sale> sales = Converter.ConvertCSVToSales(stream);
            return sales;
        }

        public async Task<bool> PersistSales(List<Sale> sales)
        {
            bool result = false;
            try
            {
                for(int i = 0, j=sales.Count; i<j; i++)
                {
                    _saleRepository.Add(sales[i]);
                }
                
                int persistedCount = await _saleRepository.UnitOfWork.SaveChangesAsync();

                result = persistedCount >= 1 ? true : false;

            } catch(Exception ex)
            {
                result = false;
            }

            return result;
        }

        public List<Sale> GetSalesList()
        {
            List<Sale> sales = new List<Sale>();

            try
            {
                sales = _saleRepository.GetSales();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
        }
    }
}
