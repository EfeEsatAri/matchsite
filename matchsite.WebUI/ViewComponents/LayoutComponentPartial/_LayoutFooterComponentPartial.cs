using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebUI.ViewComponents.LayoutComponentPartial
{
    public class _LayoutFooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
