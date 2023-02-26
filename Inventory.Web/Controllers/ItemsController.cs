using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Core.Entity;
using Inventory.Web.Dtos;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IITemRepository _iTemRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;


        public ItemsController(IITemRepository iTemRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<Customer> customerRepository, IGenericRepository<Employee> employeeRepository)
        {
            _iTemRepository = iTemRepository;
            _categoryRepository = categoryRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var items =await _iTemRepository.GetItemListAsync();
            return View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _iTemRepository.GetItemByIdAsync((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {

            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAllAsync().Result, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemDto item)
        {
            var itemToAdd = new Item {
                Name = item.Name,Brand=item.Brand,
                Price=item.Price,Description=item.Description,
                Category=item.Category,CategoryId=item.CategoryId };

            await _iTemRepository.AddItemAsync(itemToAdd);
            return RedirectToAction(nameof(Index));

            //var categories = _categoryRepository.GetAllAsync();
            //ViewData["CategoryId"] = new SelectList(categories.Result, "Id", "Name");
            //return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var item = await _iTemRepository.GetItemByIdAsync((int)id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(new[] {Status.SOLD,Status.BORROW,Status.STORE}, item.Status);
            var categories = _categoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories.Result, "Id", "Name");
            ViewData["BorrowerID"] = new SelectList(_employeeRepository.GetAllAsync().Result, "Id", "Name", item.BorrowerID);
            ViewData["BuyerId"] = new SelectList(_customerRepository.GetAllAsync().Result, "Id", "Name", item.BuyerId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            await _iTemRepository.UpdateItemAsync(item);


            return RedirectToAction(nameof(Index));
            //ViewData["Status"] = new SelectList(new[] { Status.SOLD, Status.BORROW, Status.STORE }, item.Status);
            //var categories = _categoryRepository.GetAllAsync();
            //ViewData["CategoryId"] = new SelectList(categories.Result, "Id", "Name");
            //ViewData["BorrowerID"] = new SelectList(_employeeRepository.GetAllAsync().Result, "Id", "Name", item.BorrowerID);
            //ViewData["BuyerId"] = new SelectList(_customerRepository.GetAllAsync().Result, "Id", "Name", item.BuyerId);
            //return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var item = await _iTemRepository.GetItemByIdAsync((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var item = await _iTemRepository.DeleteItemByIdAsync((int)id);


            return RedirectToAction(nameof(Index));
        }

    }
}
