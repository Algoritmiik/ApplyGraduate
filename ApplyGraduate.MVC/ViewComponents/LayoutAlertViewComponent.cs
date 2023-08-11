using Microsoft.AspNetCore.Mvc;

namespace ApplyGraduate.MVC.ViewComponents
{
    public class LayoutAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}