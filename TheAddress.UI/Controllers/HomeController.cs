using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;
using TheAddress.UI.Models;

namespace TheAddress.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly AppDBContext db;
        public HomeController(ILogger<HomeController> logger, AppDBContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(int count = 6, Contact contact=null)
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
            ViewBag.Region = new SelectList(db.Districts.ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.PropertyCategories.ToList(), "Id", "Name");
            TempData["contact"] = contact;
            return View();
        }

        public IActionResult AddToCart(int? id, int? ids)
        {
            if (id == null || id == 0) 
            {
                id = ids;
            }
            var basket = new Basket();
            string cookieValueFromReq = Request.Cookies["propertycookie"];
            var value = Convert.ToInt32(cookieValueFromReq);

            var userId = value;


            var basketrow = db.Baskets.FirstOrDefault(p => p.PropertyId == id && p.UserId == userId);

            if (basketrow != null)
            {
                db.Baskets.Remove(basketrow);
                db.SaveChanges();
                    return RedirectToAction("index", "property", new {id=ids});

            }
            else
            {
                basket.PropertyId = (int)id;
                basket.UserId = userId;
                db.Baskets.Add(basket);
                db.SaveChanges();

                return RedirectToAction("index", "property", new { id = ids, color = "red" });
               
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}