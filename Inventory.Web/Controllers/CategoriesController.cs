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
using NToastNotify;

namespace Inventory.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IToastNotification toastNotification;

        public CategoriesController(IGenericRepository<Category> repository, IToastNotification toastNotification)
        {
            _repository = repository;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _repository.GetByIdAsync((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Category category)
        {
            if (ModelState.IsValid)
            {
               await _repository.AddAsync(category);
                toastNotification.AddSuccessToastMessage("Category added successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var category = await _repository.GetByIdAsync((int)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
               await _repository.UpdateAsync(category);
                toastNotification.AddSuccessToastMessage("Category updated successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _repository.GetByIdAsync((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _repository.DeleteById(id);
            toastNotification.AddSuccessToastMessage("Customer deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

    }
}
