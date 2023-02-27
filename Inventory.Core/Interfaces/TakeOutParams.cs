using Inventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces
{ 
    public class TakeOutParams
    {
        public int Count { get; set; }
        public int Status { get; set; }
        public Category Category { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
