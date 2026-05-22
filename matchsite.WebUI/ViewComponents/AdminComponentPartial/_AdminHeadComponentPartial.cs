using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.AdminComponentPartial
{
    public class _AdminHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
