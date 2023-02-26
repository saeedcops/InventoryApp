using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Core.Entity;
using Inventory.Infrastructure.Data;
using Inventory.Core.Interfaces;

namespace Inventory.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IITemRepository _itemRepository;

        public ReportesController(IITemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }


        // GET: Reportes
        public async Task<IActionResult> SoldReport()
        {
            var data = await _itemRepository.GetItemListAsync(2);
            ViewData["Name"] = "Sold";

            return View("Index",data);
        }

        public async Task<IActionResult> StoreReport()
        {
            var data = await _itemRepository.GetItemListAsync(0);
            ViewData["Name"] = "In Store";

            return View("Index", data);
        }

        public async Task<IActionResult> BorrowReport()
        {
            var data = await _itemRepository.GetItemListAsync(1);
            ViewData["Name"] = "Borrowed";
            return View("Index", data);
        }



    }
}
