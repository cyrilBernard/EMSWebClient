using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using webclient.Models;
using webclient.Modules;

namespace webclient.ViewComponents
{
    [Authorize]
    public class SideMenuViewComponent : ViewComponent
    {
        private readonly WebApi _webapi;
        public SideMenuViewComponent(WebApi webapi) 
        { 
            _webapi = webapi;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu =  await _webapi.Response("", "api/settings/getmenu", "");
            if(menu != null)
            {
                List<Menusetting> menusettings = JsonConvert.DeserializeObject<List<Menusetting>>(menu);
                return View(menusettings);
            }
            return View();
        }
    }
}
