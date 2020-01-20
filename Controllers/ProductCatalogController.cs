using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogUI.Factories;
using ProductCatalogUI.Models;
using ProductCatalogUI.Services;
using ProductCatalogUI.Helpers;

namespace ProductCatalogUI.Controllers
{
    public class ProductCatalogController : Controller
    {
        private readonly IProductCatalogService _service;

        public ProductCatalogController(IProductCatalogService service)
        {
            _service = service;
        }

        // GET: ProductCatalog
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            // Save filter string as empty to use when exporting excel
            HttpContext.Session.SetString("FilterStr", "");

            return View(await _service.GetAllProducts());
        }


        // GET: ProductCatalog/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            return View(await _service.GetProduct(id));
        }

        // GET: ProductCatalog/Filter
        [HttpGet]
        public ActionResult Filter()
        {
            return View();
        }

        // POST: ProductCatalog/Filter
        [HttpPost]
        public async Task<ActionResult> Filter(FilterProductsViewModel filterProductsViewModel)
        {
            var filterStr = ProductFactory.Make(filterProductsViewModel);

            // Save filter string in session to use when exporting excel
            HttpContext.Session.SetString("FilterStr", filterStr);

            ViewBag.filterText = !string.IsNullOrEmpty(filterStr) ? "Filtered products for " + filterStr.Replace("?", "").Replace("&", ",") : "";

            return View("Index", await _service.GetProductsByFilter(filterStr));
        }

        // GET: ProductCatalog/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCatalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductViewModel createProductViewModel) //IFormCollection collection
        {
            try
            {
                await _service.CreateProduct(ProductFactory.Make(createProductViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Description = exc?.Message });
            }
        }

        // GET: ProductCatalog/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            return View(ProductFactory.Make(await _service.GetProduct(id).ConfigureAwait(false)));
        }

        // POST: ProductCatalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditProductViewModel editProductViewModel)
        {
            try
            {
                await _service.UpdateProduct(id, ProductFactory.Make(editProductViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Description = exc?.Message });
            }
        }

        // GET: ProductCatalog/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            return View(await _service.GetProduct(id));
        }

        // POST: ProductCatalog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Product product)
        {
            try
            {
                await _service.DeleteProduct(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Description = exc?.Message });
            }
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsCodeUnique(string code, Guid id)
        {
            if (_service.GetAllProducts().Result.Where(p => p.Code == code && p.Id != id).Any())
            {
                return Json($"Code '{code}' is already in use.");
            }

            return Json(true);
        }

        public ActionResult ExportToExcel()
        {
            // Read session for filterstring .To get same table result on index when export is clicked.
            var filterStr = HttpContext.Session.GetString("FilterStr");

            var stream = ExcelCreator.CreateExcelFileStream(_service.GetProductsByFilter(filterStr).Result.Select(p => new { p.Code, p.Name, p.Price }));

            var responseStream = new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"Export.xlsx"
            };

            return responseStream;
        }
    }
}