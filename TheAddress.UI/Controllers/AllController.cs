using Microsoft.AspNetCore.Mvc;

namespace TheAddress.UI.Controllers
{
    public class AllController : Controller
    {
        public IActionResult Index(int count = 5, bool buy = false, bool rent = false)
        {
            ViewBag.Count = count;
            ViewBag.Buy = buy;
            ViewBag.Rent = rent;
            return View();
        }
    }
}
