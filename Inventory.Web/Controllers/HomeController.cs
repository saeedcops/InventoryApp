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
        private readonly IGenericRepository<Category> _categoryRepository;

        public HomeController(IGenericRepository<Item> iTemRepository, IGenericRepository<Category> categoryRepository)
        {
            _genericRepository = iTemRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task< IActionResult> Index()
        {
            var storeCount = await _genericRepository.GetAllAsync();
            var category = await _categoryRepository.GetAllAsync();
            Dictionary<string,int> storeResult =new Dictionary<string,int>();
            Dictionary<string,int> soldResult =new Dictionary<string,int>();
            Dictionary<string,int> borrowResult =new Dictionary<string,int>();
            foreach (var cat in category)
            {
                var stored = storeCount.Where(s => s.Status == Status.STORE && s.CategoryId == cat.Id);
                if(stored.Count() > 0)
                {
                    if(storeResult.ContainsKey(cat.Name))
                    {
                        storeResult.TryGetValue(cat.Name, out int val);
                        storeResult.Add(cat.Name, val + stored.Count());
                    }
                    else 
                    storeResult.Add(cat.Name,stored.Count());
                }

                var soled = storeCount.Where(s => s.Status == Status.SOLD && s.CategoryId == cat.Id);
                if (soled.Count() > 0)
                {
                    if (soldResult.ContainsKey(cat.Name))
                    {
                        soldResult.TryGetValue(cat.Name, out int val);
                        soldResult.Add(cat.Name, val + soled.Count());
                    }
                    else
                        soldResult.Add(cat.Name, soled.Count());
                }

                var borrow = storeCount.Where(s => s.Status == Status.BORROW && s.CategoryId == cat.Id);
                if (borrow.Count() > 0)
                {
                    if (borrowResult.ContainsKey(cat.Name))
                    {
                        borrowResult.TryGetValue(cat.Name, out int val);
                        borrowResult.Add(cat.Name, val + borrow.Count());
                    }
                    else
                        borrowResult.Add(cat.Name, borrow.Count());
                }
            }

         
            ViewData["Store"] = storeResult;
            ViewData["Sold"] = soldResult;
            ViewData["Borrow"] = borrowResult;

            return View();
        }

       
    }
}