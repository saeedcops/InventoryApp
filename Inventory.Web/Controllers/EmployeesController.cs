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
    public class EmployeesController : Controller
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IToastNotification toastNotification;
        public EmployeesController(IGenericRepository<Employee> repository, IToastNotification toastNotification)
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

            var employee = await _repository.GetByIdAsync((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(employee);
                toastNotification.AddSuccessToastMessage("Employee added successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetByIdAsync((int)id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _repository.UpdateAsync(employee);
                toastNotification.AddSuccessToastMessage("Employee updated successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetByIdAsync((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteById(id);
            toastNotification.AddSuccessToastMessage("Employee deleted successfully!");
            return RedirectToAction(nameof(Index));
        }
    }
}
