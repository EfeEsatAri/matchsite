using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.LayoutComponentPartial
{
    public class _LayoutFeatureComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
