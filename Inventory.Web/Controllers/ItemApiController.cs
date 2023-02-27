using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItemApiController:ControllerBase
    {
        private readonly IGenericRepository<Item> _genericRepository;

        public ItemApiController( IGenericRepository<Item> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<ActionResult<List<ItemToReturnDto>>> SearchItems(string search)
        {
            var items = await _genericRepository.GetAllSearchAsync(search);

            var itemToReturn = items.Select(x =>
                    new ItemToReturnDto()
                    {
                        Name = x.Name,
                        BorrowedDate = x.BorrowedDate != null ? x.BorrowedDate.ToString() : "NA",
                        Borrower = x.Borrower != null ? x.Borrower.Name : "NA",
                        Brand = x.Brand,
                        Buyer = x.Buyer != null ? x.Buyer.Name : "NA",
                        Category = x.Category.Name,
                        Description = x.Description,
                        Price = x.Price,
                        SoldDate =  x.SoldDate != null ? x.SoldDate.ToString() : "NA",
                        Status = x.Status.ToString(),
                        StoredDate = x.StoredDate.ToString()

                    }).ToList();

            return Ok(itemToReturn);
        }
    }
}
