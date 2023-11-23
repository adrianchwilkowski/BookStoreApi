using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class BookInfo
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
