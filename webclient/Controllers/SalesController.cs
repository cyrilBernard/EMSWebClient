using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly WebApi _wa;
        public SalesController(WebApi wa)
        {
            _wa = wa;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, int pageNumber)
        {
            PageFilter pf = new PageFilter(){ PageNumber = pageNumber, Value = currentFilter, PageSize = 10 };
            var res = await _wa.Response(pf, "api/ws/getsaleslist", "");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<PagingResult<Sales>>(res);

                List<Sales> list = data2.Items.ToList();
                PagingResult<Sales> sale = new PagingResult<Sales>()
                {
                    Items = list,
                    Result = data2.Result
                };
                if (!string.IsNullOrEmpty(currentFilter))
                    ViewData["currentFilter"] = currentFilter;
                return View(sale);
            }
            return View();
        }
        public async Task<IActionResult> SalesDetails(string itemid)
        {
         
            List<SelectListItem> sl = new List<SelectListItem>();
            var product = await _wa.Response("", "api/utils/getproductselectlist", "");
            if (product != null)
            {
                sl = JsonConvert.DeserializeObject<List<SelectListItem>>(product);
            }
            var res = await _wa.Response(itemid, "api/invoice/getinvoicedetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                Models.Invoice data2 = JsonConvert.DeserializeObject<Models.Invoice>(res);
                data2.productlist = sl;
                if (data2.Invoicedate != null)
                    data2.Invoicedatestr = data2.Invoicedate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");

                return View(data2);
            }
            
            return View(new Models.Invoice() { productlist= sl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalesDetails(Models.Invoice salemodel)
        {
            List<SelectListItem> sl = new List<SelectListItem>();
            int quantity = 0;
            var product = await _wa.Response("", "api/utils/getproductselectlist", "");
            if (product != null)
            {
                sl = JsonConvert.DeserializeObject<List<SelectListItem>>(product);
            }
            var list = JsonConvert.DeserializeObject<List<Models.Invoicedetails>>(salemodel.invoicesdetailsliststr);
            list = list?.Where(p => !string.IsNullOrEmpty(p.Productid)).ToList();
            if(list.Count() >0)
                salemodel.invoicesdetailsliststr = JsonConvert.SerializeObject(list);
            
         
            var res = await _wa.Response(salemodel, "api/invoice/setinvoicedetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                
                formmodels data2 = JsonConvert.DeserializeObject<formmodels>(res);
                if (data2 != null && data2.Status == true)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "Successful update record";
                    ViewBag.invoiceid = data2.Data;
                    return View(new Models.Invoice() { productlist = sl });
                }
                string resp = data2.Data;
                if (resp.Contains("noquantity"))
                {
                    ViewBag.Success = false;
                    int index = data2.Data.IndexOf("noquantity -");
                    if(index >= 0)
                    {
                        var message = data2.Data.Remove(0, 13);
                        ViewBag.Message = "No stock for product ( " + message + ")";
                    }
                }
                salemodel.invoicedetailslist = JsonConvert.DeserializeObject<List<Invoicedetails>>(salemodel.invoicesdetailsliststr);
                salemodel.productlist = sl;
                return View(salemodel);
            }
            ViewBag.Success = false;
            ViewBag.Message = "Failed to update record";
            return View(new Models.Invoice() { productlist = sl});
        }
    }
}
