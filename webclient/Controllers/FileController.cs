using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using webclient.Models;
using webclient.Modules;
using Microsoft.AspNetCore.Hosting;

namespace webclient.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly WebApi _wa;
        public FileController(WebApi wa, IWebHostEnvironment environment)
        {
            _wa = wa;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> download(string fileid,string type)
        {
            Models.Download file = new Download();
            switch(type)
            {
                case "payslip":
                    file = await GetFile(fileid);
                   
                    break;
                case "invoice":
                    file = await GetInvoice(fileid);

                    break;
                default: 
                    file = new Download();
                    break;

            }
            return File(file.content, "application/pdf", file.filename);
        }
        private async Task<Models.Download> GetFile(string itemid)
        {

            try
            {
                var data = await _wa.Response(itemid, "api/file/getpayslip", "");
                if (data != null)
                {
                    var download = JsonConvert.DeserializeObject<Models.Download>(data);

                    return download;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
        private async Task<Models.Download> GetInvoice(string itemid)
        {

            try
            {
                var data = await _wa.Response(itemid, "api/file/getinvoice", "");
                if (data != null)
                {
                    var download = JsonConvert.DeserializeObject<Models.Download>(data);

                    return download;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public async Task<ActionResult> UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(List<IFormFile> files)
        {
            return View();
        }
    }
}
