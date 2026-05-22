using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.LayoutComponentPartial
{
    public class _LayoutLegaueHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
