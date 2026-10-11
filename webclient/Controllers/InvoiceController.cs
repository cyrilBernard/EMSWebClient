using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly WebApi _wa;
        public InvoiceController(WebApi wa)
        {
            _wa = wa;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, int pageNumber)
        {
            PageFilter pf = new PageFilter();
            pf.PageNumber = pageNumber;
            pf.Value = currentFilter;
            pf.PageSize = 10;
            var res = await _wa.Response(pf, "api/invoice/getinvoicelist", "");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<PagingResult<Invoice>>(res);

                List<Invoice> list = data2.Items.ToList();
                PagingResult<Invoice> paging = new PagingResult<Invoice>();
                paging.Items = list;
                paging.Result = data2.Result;
                if (!string.IsNullOrEmpty(currentFilter))
                    ViewData["currentFilter"] = currentFilter;
                return View(paging);
            }
            return View();
        }
        public async Task<IActionResult> InvoiceDetails(string itemid)
        {
            var res = await _wa.Response(itemid, "api/invoice/getinvoicedetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                Invoice data2 = JsonConvert.DeserializeObject<Invoice>(res);
                return View(data2);
            }
            return View(new Invoice());
        }
    }
}
