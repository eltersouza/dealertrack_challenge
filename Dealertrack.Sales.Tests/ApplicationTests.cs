using Dealertrack.Sales.Application.Business;
using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Dealertrack.Sales.Infrastructure.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Dealertrack.Sales.Tests
{
    public class ApplicationTests
    {
        [Fact]
        public void TestCsvFile()
        {
            CSVConverter converter = new CSVConverter();

            string csvFile = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\", "Aux_Files", "Dealertrack-CSV-Example.csv"));

            Stream stream = new FileStream(csvFile, FileMode.Open);

            List<Sale> sales = converter.ConvertCSVToSales(stream);

            Assert.True(sales.Count == 13);
        }
    }
}
