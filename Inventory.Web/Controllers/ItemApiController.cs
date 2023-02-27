using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItemApiController:ControllerBase
    {
        private readonly IItemRepository _iTemRepository;
        private readonly IGenericRepository<Item> genericRepository;

        public ItemApiController(IItemRepository iTemRepository, IGenericRepository<Item> genericRepository)
        {
            _iTemRepository = iTemRepository;
            this.genericRepository = genericRepository;
        }

        public async Task<ActionResult<List<Item>>> SearchItems(string search)
        {
            ItemSpecification itemSpecification = new ItemSpecification(search);
            var items = await _iTemRepository.GetItemBySearchAsync(search);
            return Ok(items);
        }
    }
}
