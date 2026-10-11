using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly WebApi _wa;
        public StaffController(WebApi wa)
        {
            _wa = wa;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, int pageNumber)
        {
            PageFilter pf = new PageFilter();
            pf.PageNumber = pageNumber;
            pf.Value = currentFilter;
            pf.PageSize = 10;
            var res = await _wa.Response(pf, "api/staff/getstafflist", "");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<PagingResult<Staff>>(res);

                List<Staff> list = data2.Items.ToList();
                PagingResult<Staff> r = new PagingResult<Staff>();
                r.Items = list;
                r.Result = data2.Result;
                if (!string.IsNullOrEmpty(currentFilter))
                    ViewData["currentFilter"] = currentFilter;
                return View(r);
            }
            return View();
        }
        public async Task<IActionResult> Details(string itemid)
        {

            var res = await _wa.Response(itemid, "api/staff/getstaffdetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                Models.Staff data2 = JsonConvert.DeserializeObject<Models.Staff>(res);
                if(string.IsNullOrEmpty(data2.TempPassword))
                {
                    data2.TempPassword = Utils.RandomPassword();
                }
                return View(data2);
            }

            return View(new Models.Staff());
        }
        [HttpPost]
        public async Task<IActionResult> Details(Models.Staff sm)
        {
            var res = await _wa.Response(sm, "api/staff/setstaffdetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                formmodels data2 = JsonConvert.DeserializeObject<formmodels>(res);

                if (data2 != null && data2.Status == true)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "Successful update record";
                    return View(sm);
                }
                ViewBag.Success = false;
                ViewBag.Message = data2.Data;
            }

            return View(sm);
        }
        [Route("getdetails")]
        [HttpPost]
        public async Task<JsonResult> GetDetails([FromBody] RequestModel req)
        {
            var res = await _wa.Response(req.itemid, "api/staff/getstaffdetailsselected", "");
            if (res != null)
            {
                var result = JsonConvert.DeserializeObject<Models.StaffDetailsSelected>(res);
                return new JsonResult(new { success = true, data = result });
            }
            return new JsonResult(new
            {
                success = false,
                data = "No Data"
            });
        }
        [Route("deletestaff")]
        [HttpPost]
        public async Task<JsonResult> DeleteStaff([FromBody] RequestModel req)
        {
            var res = await _wa.Response(req.itemid, "api/staff/deletestaff", "");
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
    }
}
