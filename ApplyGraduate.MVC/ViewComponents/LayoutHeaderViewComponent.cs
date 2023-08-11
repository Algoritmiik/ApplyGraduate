using Microsoft.AspNetCore.Mvc;

namespace ApplyGraduate.MVC.ViewComponents
{
    public class LayoutHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}