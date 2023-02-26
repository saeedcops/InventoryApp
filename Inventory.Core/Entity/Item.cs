using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Entity
{
    public class Item:BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StoredDate { get; set; } = DateTime.Now.Date;

        public DateTime? BorrowedDate { get; set; }

        public DateTime? SoldDate { get; set; } 
        public Status Status { get; set; } = Status.STORE;

        public int? BorrowerID { get; set; }
        public Employee? Borrower { get; set; }
        public int? BuyerId { get; set; }
        public Customer? Buyer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
