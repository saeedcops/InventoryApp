using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Implementation;
using Inventory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Inventory.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IGenericRepository<Item> _genericRepository;

        public HomeController(IGenericRepository<Item> iTemRepository)
        {
            _genericRepository = iTemRepository;
        }

        public async Task< IActionResult> Index()
        {
            var storeCount = await _genericRepository.GetAllAsync();
            var store =storeCount.Where(x => x.Status == Core.Entity.Status.STORE).Count();
            var sold =storeCount.Where(x => x.Status == Core.Entity.Status.SOLD).Count();
            var borrow =storeCount.Where(x => x.Status == Core.Entity.Status.BORROW).Count();

            ViewData["Store"] = store;
            ViewData["Sold"] = sold;
            ViewData["Borrow"] = borrow;
            return View();
        }

       
    }
}