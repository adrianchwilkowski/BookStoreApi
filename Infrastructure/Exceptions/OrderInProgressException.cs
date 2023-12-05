using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class OrderInProgressException : Exception
    {
        public OrderInProgressException(string message) :base(message) { }
    }
}
