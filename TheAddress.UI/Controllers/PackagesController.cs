using Microsoft.AspNetCore.Mvc;
using TheAddress.DAL.Data;

namespace TheAddress.UI.Controllers
{
    public class PackagesController : Controller
    {
        private readonly AppDBContext _service;

        public PackagesController(AppDBContext service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2000);
            cookieOptions.Path = "/";

            Response.Cookies.Append("propertycookie", $"{1}", cookieOptions);

            string cookieValueFromReq = Request.Cookies["propertycookie"];
            var value = Convert.ToInt32(cookieValueFromReq);

            var countt = _service.Baskets.Where(p => p.UserId == value).Count();
            ViewBag.Countt = countt;
            return View();
        }
    }
}
