using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using NToastNotify;
using ClosedXML.Excel;

namespace Inventory.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IToastNotification toastNotification;
        private static int _lastReport = -1;
        public ReportesController(IGenericRepository<Item> itemRepository, IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
            _itemRepository = itemRepository;
        }
        [HttpGet("DownloadReport")]
        public async Task< IActionResult> DownloadReport()
        {
            if (_lastReport == -1)
            {
                toastNotification.AddErrorToastMessage("No items to generate a report");
                return View("Index");
            }
            else
            {
                var items = await _itemRepository.GetAllByStatusAsync(_lastReport);
                if(items == null || items.Count == 0)
                {
                    toastNotification.AddErrorToastMessage("No items to generate a report");
                    return View("Index",items);
                }

            

                using var workbook = new XLWorkbook();

                var workSheet = workbook.Worksheets.Add("Reports");

                var curRow = 1;
                workSheet.Cell("A1").Value = "Name";
                workSheet.Cell("B1").Value = "Brand";
                workSheet.Cell("C1").Value = "Description";
                workSheet.Cell("D1").Value = "Price";
                workSheet.Cell("E1").Value = "StoredDate";
                workSheet.Cell("F1").Value = "BorrowedDate";
                workSheet.Cell("G1").Value = "SoldDate";
                workSheet.Cell("H1").Value = "Status";
                workSheet.Cell("I1").Value = "Borrower";
                workSheet.Cell("J1").Value = "Buyer";
                workSheet.Cell("K1").Value = "Category";

                foreach (var item in items)
                {
                    curRow++;
                    workSheet.Cell("A" + curRow).Value = item.Name;
                    workSheet.Cell("B" + curRow).Value = item.Brand;
                    workSheet.Cell("C" + curRow).Value = item.Description;
                    workSheet.Cell("D" + curRow).Value = item.Price;
                    workSheet.Cell("E2" + curRow).Value = item.StoredDate;
                    workSheet.Cell("F2" + curRow).Value = item.BorrowedDate;
                    workSheet.Cell("G2" + curRow).Value = item.SoldDate;
                    workSheet.Cell("H2" + curRow).Value = item.Status;
                    workSheet.Cell("I2" + curRow).Value = item.Borrower;
                    workSheet.Cell("J2" + curRow).Value = item.Buyer;
                    workSheet.Cell("K2" + curRow).Value = item.Category;
                }

                using var stream = new MemoryStream();

                workbook.SaveAs(stream);

                var content = stream.ToArray();
                //workbook.SaveAs("Report.xlsx");
                return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Report.xlsx"
                    );
            }
        }

        public async Task<IActionResult> SoldReport()
        {
            _lastReport = 2;

            ViewData["Name"] = "Sold";

            return View("Index", await _itemRepository.GetAllByStatusAsync(2));
        }

        public async Task<IActionResult> StoreReport()
        {
            _lastReport = 0;
            ViewData["Name"] = "In Store";

            return View("Index", await _itemRepository.GetAllByStatusAsync(0));
        }

        public async Task<IActionResult> BorrowReport()
        {
            _lastReport = 1;
            ViewData["Name"] = "Borrowed";
            return View("Index", await _itemRepository.GetAllByStatusAsync(1));
        }



    }
}
