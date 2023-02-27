using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Core.Specification;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T T)
        {
           await _dbContext.AddAsync(T);
           await _dbContext.SaveChangesAsync();
            return T;
        }

        public async Task<T> DeleteById(int id)
        {
            var itemToDelete = await GetByIdAsync(id);
              _dbContext.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();

            return itemToDelete;  

        }

        
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>
                        .GetQuery(_dbContext.Set<T>()
                        .AsQueryable(), spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> UpdateAsync(T T)
        {
            var itemToUpdate=  _dbContext.Update(T);
            await _dbContext.SaveChangesAsync();

            return T;

        }

        
    }
}
