using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Specification
{
    public class ItemSpecificationParams
    {
        private string _search;
        public string? Name { get; set; }
        public string? Sort { get; set; }
        public string? Status { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Customer { get; set; }
        public string? Employee { get; set; }
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
