using Dealertrack.Sales.Domain.Exceptions;
using Dealertrack.Sales.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate
{
    public class Sale : Entity, IAggregateRoot
    {
        public SaleStatus SaleStatus {
            get
            {
                return SaleStatus.From(_saleStatusId);
            }
            set {
                _saleStatusId = value.Id;
            }
        }
        private int _saleStatusId;

        public int DealNumber { get; set; }

        public string CustomerName { get; set; }

        public string DealershipName { get; set; }

        public string Vehicle { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        protected Sale() { }

        public Sale(int dealNumber, string customerName, string dealershipName, string vehicle, decimal price, DateTime date)
        {
            DealNumber = dealNumber;
            CustomerName = customerName;
            DealershipName = dealershipName;
            Vehicle = vehicle;
            Price = price;
            Date = date;
            SaleStatus = SaleStatus.Pending;
        }

        public void SetPaidStatus()
        {
            if (_saleStatusId == SaleStatus.Pending.Id)
            {
                _saleStatusId = SaleStatus.Paid.Id;
            }
        }

        public void SetPendingStatus()
        {
            if (_saleStatusId != SaleStatus.Pending.Id)
            {
                _saleStatusId = SaleStatus.Pending.Id;
            }
        }

        public void SetCancelledStatus()
        {
            if (_saleStatusId == SaleStatus.Paid.Id)
            {
                StatusChangeException(SaleStatus.Cancelled);
            }

            _saleStatusId = SaleStatus.Cancelled.Id;
        }

        private void StatusChangeException(SaleStatus saleStatusToChange)
        {
            throw new SaleDomainException($"Is not possible to change the order status from {SaleStatus.Name} to {saleStatusToChange.Name}.");
        }
    }
}
