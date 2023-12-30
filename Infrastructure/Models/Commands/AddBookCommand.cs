using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Commands
{
    public class AddBookCommand
    {
        public string Title { get; private set; } = null!;
        public string Author { get; private set; } = null!;
        public string? Description { get; private set; }
        public int Pages { get; private set; }
    }
}
