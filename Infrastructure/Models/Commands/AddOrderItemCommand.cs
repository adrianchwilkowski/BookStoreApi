using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Commands
{
    public class AddOrderItemCommand
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
