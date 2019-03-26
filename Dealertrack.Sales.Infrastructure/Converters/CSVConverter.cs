using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dealertrack.Sales.Infrastructure.Converters
{
    public class CSVConverter
    {
        public CSVConverter() { }
        public CSVConverter(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        private ISaleRepository _saleRepository { get; }

        public List<Sale> ConvertCSVToSales(Stream stream)
        {
            List<Sale> sales = new List<Sale>();
            using (var reader = new StreamReader(stream))
            {
                int lineCount = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (lineCount != 1)
                    {
                        var values = treatGotchas(line);

                        int dealNumber = int.Parse(values[0]);
                        string customerName = values[1];
                        string dealershipName = values[2];
                        string vehicle = values[3];
                        decimal price = decimal.Parse(values[4].Replace(",", "."));
                        DateTime date = DateTime.Parse(values[5]);

                        Sale sale = new Sale(dealNumber, customerName, dealershipName, vehicle, price, date);
                        sale.SetPaidStatus();

                        sales.Add(sale);
                    }
                    lineCount++;
                }
            }

            return sales;
        }

        private string[] treatGotchas(string line)
        {
            List<string> items = new List<string>();
            string word = string.Empty;
            bool ignoreNextOcurrence = false;
            for (int i = 0, j = line.Length; i < j; i++)
            {
                if (line[i] != ',')
                {
                    if (line[i] == '"' && line[i + 1] != ',')
                        ignoreNextOcurrence = true;

                    word = word + line[i];
                }
                else
                {
                    if (ignoreNextOcurrence)
                    {
                        word = word + line[i];
                        ignoreNextOcurrence = false;
                    }
                    else
                    {
                        items.Add(word.Replace("\"", ""));
                        word = string.Empty;
                    }
                }
            }

            items.Add(word.Replace("\"", ""));

            return items.ToArray();
        }
    }
}
