using Inventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces
{
    public interface IITemRepository
    {
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<Item> DeleteItemByIdAsync(int id);
        Task<Item> GetItemByIdAsync(int id);
        Task<IReadOnlyList<Item>> GetItemListAsync();
        Task<IReadOnlyList<Item>> GetItemListAsync(int status);

    }
}
