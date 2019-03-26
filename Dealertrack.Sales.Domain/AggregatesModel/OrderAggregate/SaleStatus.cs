using Dealertrack.Sales.Domain.Exceptions;
using Dealertrack.Sales.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate
{
    public class SaleStatus : Enumeration
    {
        public static SaleStatus Paid = new SaleStatus(1, nameof(Paid).ToLowerInvariant());
        public static SaleStatus Pending = new SaleStatus(2, nameof(Pending).ToLowerInvariant());
        public static SaleStatus Cancelled = new SaleStatus(3, nameof(Cancelled).ToLowerInvariant());

        protected SaleStatus() { }

        public SaleStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<SaleStatus> List() => new[] { Paid, Pending, Cancelled };

        public static SaleStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new SaleDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static SaleStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new SaleDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
