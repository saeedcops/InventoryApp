using Inventory.Core.Entity;

namespace Inventory.Web.ViewModel
{
    public class ItemToReturnViewModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string StoredDate { get; set; }
        public string BorrowedDate { get; set; }
        public string SoldDate { get; set; }
        public string Status { get; set; }
        public string Borrower { get; set; }
        public string Buyer{ get; set; }
        public string Category { get; set; }
    }
}
