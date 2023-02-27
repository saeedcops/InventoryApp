using Inventory.Core.Entity;

namespace Inventory.Web.ViewModel
{
    public class TakeOutViewModel
    {
       
        public int Count { get; set; }
        public int Status { get; set; }
        public Category Category { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
