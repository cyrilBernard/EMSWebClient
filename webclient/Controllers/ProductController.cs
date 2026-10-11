using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly WebApi _wa;
        
        public ProductController(WebApi wa)
        {
            _wa = wa;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, int pageNumber)
        {
            PageFilter pf = new PageFilter();
            pf.PageNumber = pageNumber;
            pf.Value = currentFilter;
            pf.PageSize = 10;
            var res = await _wa.Response(pf, "api/ws/productlist", "POST");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<PagingResult<Product>>(res);

                List<Product> list = data2.Items.ToList();
                PagingResult<Product> r = new PagingResult<Product>();
                r.Items = list;
                r.Result = data2.Result;
                if(!string.IsNullOrEmpty(currentFilter))
                    ViewData["currentFilter"] = currentFilter;
                //var data3 = JsonConvert.SerializeObject(data2.items);
                //List<TrackingModel> data4 = JsonConvert.DeserializeObject<List<TrackingModel>>(data3);
                return View(r);
            }
            return View();
        }
        public async Task<IActionResult> ProductDetails(string itemid)
        {
            List<SelectListItem> sli = new List<SelectListItem>();
            var startcategories = await _wa.Response("", "api/settings/getcategoryselectlist", "");
            if (startcategories != null)
            {
                sli = JsonConvert.DeserializeObject<List<SelectListItem>>(startcategories);
            }
            var res = await _wa.Response(itemid, "api/ws/getproductdetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                Product data2 = JsonConvert.DeserializeObject<Product>(res);
                data2.productlistcategory = sli;
                var subcategories = await _wa.Response(data2.Productcategory, "api/settings/getcategorysubselectlist", "");
                if (subcategories != null)
                {
                    data2.productlistsubcategory = JsonConvert.DeserializeObject<List<SelectListItem>>(subcategories);
                }
                
                return View(data2);
            }
            
            return View(new Product() { productlistcategory = sli });
        }
        [HttpPost]
        public async Task<IActionResult> ProductDetails(Models.Product product)
        {
            var startcategories = await _wa.Response("", "api/settings/getcategoryselectlist", "");
            if (startcategories != null)
            {
                product.productlistcategory = JsonConvert.DeserializeObject<List<SelectListItem>>(startcategories);
            }
            var subcategories = await _wa.Response(product.Productcategory, "api/settings/getcategorysubselectlist", "");
            if (subcategories != null)
            {
                product.productlistsubcategory = JsonConvert.DeserializeObject<List<SelectListItem>>(subcategories);
            }
            
            var res = await _wa.Response(product, "api/ws/saveproduct",  "");
            if (!string.IsNullOrEmpty(res))
            {
                formmodels data2 = JsonConvert.DeserializeObject<formmodels>(res);
                
                if (data2 != null && data2.Status ==true)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "Successful update record";
                    return View(product);
                }
                ViewBag.Success = false;
                ViewBag.Message = data2.Data;
            }
            
            return View(product);
        }
        [Route("deleteproduct")]
        [HttpPost]
        public async Task<JsonResult> DeleteProduct([FromBody] RequestModel req)
        {
            var res = await _wa.Response(req.itemid, "api/ws/deleteproductdetails", "");
            if (res != null)
            {
                formmodels data2 = JsonConvert.DeserializeObject<formmodels>(res);
                return new JsonResult(new { success = data2.Status, data = data2.Data });
            }
            return new JsonResult(new
            {
                success = false,
                data = "No Data"
            });
        }
        [Route("getsubcategory")]
        [HttpPost]
        public async Task<JsonResult> GetSubCategory([FromBody] RequestModel req)
        {
            var subcategories = await _wa.Response(req.itemid, "api/settings/getcategorysubselectlist", "");
            if (subcategories != null)
            {
                var result = JsonConvert.DeserializeObject<List<SelectListItem>>(subcategories);
                return new JsonResult(new { success = true, data = result });
            }
            return new JsonResult(new
            {
                success = false,
                data = "No Data"
            });
        }
        [Route("getproductprice")]
        [HttpPost]
        public async Task<JsonResult> GetProductPrice([FromBody] RequestModel req)
        {
            var subcategories = await _wa.Response("", "api/ws/productpricelist", "");
            if (subcategories != null && !string.IsNullOrEmpty(req.itemid))
            {
                var result = JsonConvert.DeserializeObject<List<ProductPrice>>(subcategories);
                if(result.Count > 0)
                {
                    var result1 = result.Where(p => p.productid == req.itemid).FirstOrDefault();
                    return new JsonResult(new { success = true, data = result1.price });
                }
            }
            return new JsonResult(new
            {
                success = false,
                data = "No Data"
            });
        }
    }
}
