using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.AdminComponentPartial
{
    public class _AdminScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
