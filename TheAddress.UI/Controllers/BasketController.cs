using Microsoft.AspNetCore.Mvc;
using TheAddress.DAL.Data;

namespace TheAddress.UI.Controllers
{
    public class BasketController : Controller
    {
        readonly AppDBContext db;
        public BasketController(AppDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2000);
            cookieOptions.Path = "/";

            Response.Cookies.Append("propertycookie", $"{1}", cookieOptions);

            string cookieValueFromReq = Request.Cookies["propertycookie"];
            var value = Convert.ToInt32(cookieValueFromReq);

            var countt = db.Baskets.Where(p => p.UserId == value).ToList();
            ViewBag.Countt = countt.Count();

            return View(countt);
        }
    }
}
