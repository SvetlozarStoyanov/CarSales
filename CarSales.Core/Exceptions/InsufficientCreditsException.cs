using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Core.Exceptions
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string? message) : base(message)
        {

        }
    }
}
