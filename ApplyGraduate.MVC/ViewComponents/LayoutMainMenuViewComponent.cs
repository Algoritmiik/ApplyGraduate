using Microsoft.AspNetCore.Mvc;

namespace ApplyGraduate.MVC.ViewComponents
{
    public class LayoutMainMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}