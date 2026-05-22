using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.LayoutComponentPartial
{
    public class _LayoutHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
