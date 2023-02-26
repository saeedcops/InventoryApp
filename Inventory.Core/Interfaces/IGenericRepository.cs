using Inventory.Core.Entity;
using Inventory.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> DeleteById(int id);
        Task<T> UpdateAsync(T T);
        Task<T> AddAsync(T T);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
