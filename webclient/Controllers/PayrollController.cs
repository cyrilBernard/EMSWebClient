using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.Controllers
{
    [Authorize]
    public class PayrollController : Controller
    {
        private readonly WebApi _wa;
        public PayrollController(WebApi wa)
        {
            _wa = wa;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, int pageNumber)
        {
            PageFilter pf = new PageFilter();
            pf.PageNumber = pageNumber;
            pf.Value = currentFilter;
            pf.PageSize = 10;
            var res = await _wa.Response(pf, "api/payroll/getpayrolllist", "");
            if (!string.IsNullOrEmpty(res))
            {
                var data2 = JsonConvert.DeserializeObject<PagingResult<Payroll>>(res);

                List<Payroll> list = data2.Items.ToList();
                PagingResult<Payroll> r = new PagingResult<Payroll>();
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
            List<SelectListItem> sli = new List<SelectListItem>();
            var stafflist = await _wa.Response("", "api/staff/getstaffselectlist", "");
            if (stafflist != null)
            {
                sli = JsonConvert.DeserializeObject<List<SelectListItem>>(stafflist);
            }
            var res = await _wa.Response(itemid, "api/payroll/getpayrolldetails", "");
            if (!string.IsNullOrEmpty(res))
            {
                Models.Payroll data2 = JsonConvert.DeserializeObject<Models.Payroll>(res);
                data2.StaffList = sli;
                return View(data2);
            }

            return View(new Models.Payroll { StaffList = sli});
        }
        [HttpPost]
        public async Task<IActionResult> Details(Models.Payroll sm)
        {
            var res = await _wa.Response(sm, "api/payroll/setpayrolldetails", "");
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
        [Route("deletepayroll")]
        [HttpPost]
        public async Task<JsonResult> DeletePayroll([FromBody] RequestModel req)
        {
            var res = await _wa.Response(req.itemid, "api/payroll/deletepayrolldetails", "");
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
