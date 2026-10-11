using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly WebApi _wa;
        public SettingsController(WebApi wa) 
        {
            _wa = wa;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, int pageNumber)
        {
            PageFilter pf = new PageFilter();
            pf.PageNumber = pageNumber;
            pf.Value = currentFilter;
            pf.PageSize = 10;
            var res = await _wa.Response(pf, "api/settings/getcategorylist", "");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<PagingResult<ProductsCategory>>(res);

                List<ProductsCategory> list = data2.Items.ToList();
                PagingResult<ProductsCategory> product = new PagingResult<ProductsCategory>();
                product.Items = list;
                product.Result = data2.Result;
                if (!string.IsNullOrEmpty(currentFilter))
                    ViewData["currentFilter"] = currentFilter;
                return View(product);
            }
            return View();
        }
        public async Task<IActionResult> CategoryDetails(Guid itemid)
        {
            var res = await _wa.Response(itemid.ToString(), "api/settings/getcategorydetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                ProductsCategory data2 = JsonConvert.DeserializeObject<ProductsCategory>(res);
                return View(data2);
            }
            return View(new ProductsCategory());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDetails(Models.ProductsCategory product)
        {
            var res = await _wa.Response(product, "api/settings/setcategory", "");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<bool>(res);
                if (data2 == true)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "Successful update record";
                }
                return View(product);
            }
            ViewBag.Success = false;
            ViewBag.Message = "Failed to update record";
            return View(new ProductsCategory());
        }
    }
}
