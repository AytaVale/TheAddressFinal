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
        public IActionResult Index(int count = 7, bool buy = false, bool rent = false, int categoryId = 0, int min = 0, int max=0, int region=0, int roomCount=0, AllViewModel fm = null)
        {
            var vm = new AllViewModel();
            vm.FilterForm = fm.FilterForm;
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
            ViewBag.Min = min;
            ViewBag.Max = max;
            ViewBag.Region = region;
            ViewBag.Room = roomCount;

            if (vm.FilterForm!=null)
            {
                ViewBag.Category = vm.FilterForm.CategoryId;
                ViewBag.Min = vm.FilterForm.Min;
                ViewBag.Max = vm.FilterForm.Max;
                ViewBag.Region = vm.FilterForm.Region;
                ViewBag.Room = vm.FilterForm.Roomcount;
            }

            vm.Categories = db.PropertyCategories.ToList();
            vm.Districts = db.Districts.ToList();
            return View(vm);
        }
    }
}
