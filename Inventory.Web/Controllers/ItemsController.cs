
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using NToastNotify;
using Inventory.Web.ViewModel;
using System.Text.RegularExpressions;
using Inventory.Web.Filter;

namespace Inventory.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IToastNotification toastNotification;

        public ItemsController( IGenericRepository<Category> categoryRepository, IGenericRepository<Customer> customerRepository, IGenericRepository<Employee> employeeRepository, IToastNotification toastNotification, IGenericRepository<Item> itemRepository)
        {
            _categoryRepository = categoryRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            this.toastNotification = toastNotification;
            _itemRepository = itemRepository;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var items =await _itemRepository.GetAllItemAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemRepository.GetByIdAsync((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Create()
        {

            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAllAsync().Result, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel item)
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAllAsync().Result, "Id", "Name");

            var itemToAdd = new Item {
                Name = item.Name,Brand=item.Brand,
                Price=item.Price,Description=item.Description,
                Category=item.Category,CategoryId=item.CategoryId };

            if (!InputFilter.ItemCreateFilter(item))
            {
                toastNotification.AddErrorToastMessage("Please dont enter specail char");
                return View();
            }

            await _itemRepository.AddAsync(itemToAdd);
            toastNotification.AddSuccessToastMessage("Item added successfully!");
            return RedirectToAction(nameof(Index));

        }
      

        public async Task<IActionResult> TakeOut()
        {
         
            ViewData["Status"] = new SelectList(new[] { Status.SOLD, Status.BORROW });
            ViewData["Category"] = new SelectList(_categoryRepository.GetAllAsync().Result, "Id", "Name");
            ViewData["Employee"] = new SelectList(_employeeRepository.GetAllAsync().Result, "Id", "Name");
            ViewData["Customer"] = new SelectList(_customerRepository.GetAllAsync().Result, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeOut(TakeOutParams item,int? category,int? customer,int? employee)
        {
            ViewData["Status"] = new SelectList(new[] { Status.SOLD, Status.BORROW });
            ViewData["Category"] = new SelectList(_categoryRepository.GetAllAsync().Result, "Id", "Name");
            ViewData["Employee"] = new SelectList(_employeeRepository.GetAllAsync().Result, "Id", "Name");
            ViewData["Customer"] = new SelectList(_customerRepository.GetAllAsync().Result, "Id", "Name");

            if (!InputFilter.TakeOutFilter(item) && item != null)
            {
                item.Category = new Category();
                item.Customer = new Customer();
                item.Employee = new Employee();

                item.Category.Id =(int) category;
                item.Customer.Id =(int) customer;
                item.Employee.Id =(int) employee;
                var result = await _itemRepository.TakeOut(item);

                if(result)
                    toastNotification.AddSuccessToastMessage("Item updated successfully!");
                else
                {

                 toastNotification.AddAlertToastMessage("No enough items in a store");
                    
                    return View();

                }

                return RedirectToAction(nameof(Index));
            }

            toastNotification.AddErrorToastMessage("Something went wrong!");

            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemRepository.GetByIdAsync((int)id);
            if (item == null)
            {
                return NotFound();
            }

            ViewData["Status"] = new SelectList(new[] { Status.SOLD, Status.BORROW, Status.STORE }, item.Status);
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
            ViewData["Status"] = new SelectList(new[] { Status.SOLD, Status.BORROW, Status.STORE }, item.Status);
            var categories = _categoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories.Result, "Id", "Name");
            ViewData["BorrowerID"] = new SelectList(_employeeRepository.GetAllAsync().Result, "Id", "Name", item.BorrowerID);
            ViewData["BuyerId"] = new SelectList(_customerRepository.GetAllAsync().Result, "Id", "Name", item.BuyerId);

            if (id != item.Id)
            {
                return NotFound();
            }
            
            if(!InputFilter.ItemCreateFilter(item)) 
            {
                toastNotification.AddErrorToastMessage("Something went wrong with validations!");

                return View();
            }
            
            await _itemRepository.UpdateAsync(item);
            toastNotification.AddSuccessToastMessage("Item updated successfully!");

            return RedirectToAction(nameof(Index));
            
        }
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var item = await _itemRepository.GetByIdAsync((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var item = await _itemRepository.DeleteById((int)id);
            toastNotification.AddSuccessToastMessage("Item deleted successfully!");

            return RedirectToAction(nameof(Index));
        }

    }
}
