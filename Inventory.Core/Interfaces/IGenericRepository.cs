using Inventory.Core.Entity;


namespace Inventory.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> DeleteById(int id);
        Task<T> UpdateAsync(T T);
        Task<T> AddAsync(T T);
        Task<bool> TakeOut(TakeOutParams takeOutParams);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<Item>> GetAllItemAsync();
        Task<IReadOnlyList<T>> GetAllByStatusAsync(int status);
        Task<IReadOnlyList<T>> GetAllSearchAsync(string search);

    }
}
