using Inventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Specification
{
    public class ItemSpecification : BaseSpecification<Item>
    {
        public ItemSpecification(ItemSpecificationParams itemSpec)
            : base(x =>
            (string.IsNullOrEmpty(itemSpec.Search) || x.Name.ToLower().Contains(itemSpec.Search)) &&
            (string.IsNullOrEmpty(itemSpec.Category) || x.Category.Name.ToLower().Contains(itemSpec.Category)) &&
            (string.IsNullOrEmpty(itemSpec.Brand) || x.Brand.ToLower().Contains(itemSpec.Brand)) &&
            (string.IsNullOrEmpty(itemSpec.Status) || x.Status.ToString() == itemSpec.Status) &&
            (string.IsNullOrEmpty(itemSpec.Customer) || x.Buyer.Name.ToLower().Contains(itemSpec.Customer) ))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Buyer);
            AddOrder(x => x.Name);

            if (!string.IsNullOrEmpty(itemSpec.Sort))
            {
                Console.WriteLine(itemSpec.Sort);
                switch (itemSpec.Sort)
                {
                    case "price":
                        AddOrder(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderDesc(x => x.Price);
                        break;
                    default:
                        AddOrder(x => x.Name);
                        break;
                }
            }
        }

        public ItemSpecification(string search) : base(x =>
             x.Name.ToLower().StartsWith(search) &&
            //( x.Category.Name.ToLower().Contains(search)) &&
             x.Brand.ToLower().StartsWith(search))
            //( x.Status.ToString() == search) )
            //( x.Buyer.Name.ToLower().Contains(search)))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Buyer);
        }

    }
}
