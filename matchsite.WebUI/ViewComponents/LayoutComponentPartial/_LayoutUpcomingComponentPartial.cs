using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.LayoutComponentPartial
{
    public class _LayoutUpcomingComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
