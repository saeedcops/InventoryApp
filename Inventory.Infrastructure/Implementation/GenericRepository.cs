using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


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

        public async Task<IReadOnlyList<Item>> GetAllItemAsync()
        {
            return await _dbContext.Items
                 .Include(x => x.Buyer)
                 .Include(x => x.Borrower)
                 .Include(x => x.Category).ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllByStatusAsync(int status)
        {
            return (IReadOnlyList<T>) await _dbContext.Items
                .Where(i => (int)i.Status == status)
                .Include(x => x.Buyer)
                .Include(x => x.Borrower)
                .Include(x => x.Category).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllSearchAsync(string search)
        {
            return (IReadOnlyList<T>) await _dbContext.Items
                .Include(x => x.Buyer)
                .Include(x => x.Borrower)
                .Include(x => x.Category)
                .Where(x => x.Name.StartsWith(search) ||
                        x.Brand.StartsWith(search) ||
                        x.Category.Name.StartsWith(search) ||
                        x.StoredDate.ToString().StartsWith(search) ||
                        x.Description.StartsWith(search)).ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> UpdateAsync(T T)
        {
             _dbContext.Update(T);
            await _dbContext.SaveChangesAsync();

            return T;

        }

        public async Task< bool> TakeOut(TakeOutParams TakeOutParams)
        {
            var items = await _dbContext.Items
                .Where(x => x.Status == Status.STORE && x.CategoryId == TakeOutParams.Category.Id ).ToListAsync();

            if (items==null || items.Count < TakeOutParams.Count)
                return false;

            for (int i = 0; i < TakeOutParams.Count; i++)
            {
                items[i].Status =(Status) (TakeOutParams.Status + 1);
                items[i].BuyerId =TakeOutParams.Customer.Id;
                items[i].BorrowerID =TakeOutParams.Employee.Id;
                await _dbContext.SaveChangesAsync();

            }
            return true;
        }
    }
}
