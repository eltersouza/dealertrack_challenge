using System;
using System.Collections.Generic;
using System.Text;

namespace Dealertrack.Sales.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class SaleDomainException : Exception
    {
        public SaleDomainException()
        { }

        public SaleDomainException(string message)
            : base(message)
        { }

        public SaleDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
