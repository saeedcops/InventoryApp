
using Microsoft.AspNetCore.Mvc;
using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using NToastNotify;

namespace Inventory.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IToastNotification toastNotification;
        public CustomersController(IGenericRepository<Customer> repository, IToastNotification toastNotification)
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
        public async Task<IActionResult> Create([Bind("Name,Id")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(customer);
                toastNotification.AddSuccessToastMessage("Customer added successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _repository.UpdateAsync(customer);
                toastNotification.AddSuccessToastMessage("Customer updated successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

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
