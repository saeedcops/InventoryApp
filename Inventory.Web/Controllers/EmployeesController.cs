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
    public class EmployeesController : Controller
    {
        private readonly IGenericRepository<Employee> _repository;

        public EmployeesController(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        // GET: Categories/Details/5
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

        // GET: Categories/Create
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

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Categories/Edit/5
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

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Categories/Delete/5
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

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _repository.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
