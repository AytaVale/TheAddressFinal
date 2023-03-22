using Microsoft.AspNetCore.Mvc;
using TheAddress.DAL.Data;
using TheAddress.UI.Models;

namespace TheAddress.UI.Controllers
{
    public class AllController : Controller
    {
        readonly AppDBContext db;
        public AllController(AppDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int count = 5, bool buy = false, bool rent = false, int categoryId = 0, FilterFormModel fm = null)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2000);
            cookieOptions.Path = "/";

            Response.Cookies.Append("propertycookie", $"{1}", cookieOptions);

            string cookieValueFromReq = Request.Cookies["propertycookie"];
            var value = Convert.ToInt32(cookieValueFromReq);

            var countt = db.Baskets.Where(p => p.UserId == value).Count();
            ViewBag.Countt = countt;

            ViewBag.Count = count;
            ViewBag.Buy = buy;
            ViewBag.Rent = rent;
            ViewBag.Category = categoryId;

            if(fm.CategoryId > 0)
            {
                ViewBag.Category = fm.CategoryId;
            }
            ViewBag.Room = fm.Roomcount;
            ViewBag.Region = fm.Region;
            return View();
        }
    }
}
