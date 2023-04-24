using Microsoft.AspNetCore.Mvc;
using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;

namespace TheAddress.UI.Controllers
{
    public class ContactController : Controller
    {
        readonly AppDBContext db;
        public ContactController(AppDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index(Contact contacts)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2000);
            cookieOptions.Path = "/";

            Response.Cookies.Append("propertycookie", $"{1}", cookieOptions);

            string cookieValueFromReq = Request.Cookies["propertycookie"];
            var value = Convert.ToInt32(cookieValueFromReq);

            var countt = db.Baskets.Where(p => p.UserId == value).Count();
            ViewBag.Countt = countt;
            TempData["contact"] = contacts;
            return View();
        }

    }
}