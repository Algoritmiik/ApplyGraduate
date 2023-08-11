using Microsoft.AspNetCore.Mvc;

namespace ApplyGraduate.MVC.ViewComponents
{
    public class LayoutFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}