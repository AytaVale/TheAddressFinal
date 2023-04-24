using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.Data;
using TheAddress.UI.Models;

namespace TheAddress.UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly AppDBContext _service;

        public PropertyController(AppDBContext service)
        {
            _service = service;
        }

        public IActionResult Index(int id, string? color)
        {
            ViewBag.color=color;
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2000);
            cookieOptions.Path = "/";

            Response.Cookies.Append("propertycookie", $"{1}", cookieOptions);

            string cookieValueFromReq = Request.Cookies["propertycookie"];
            var value = Convert.ToInt32(cookieValueFromReq);

            var countt = _service.Baskets.Where(p => p.UserId == value).Count();
            ViewBag.Countt = countt;
            var vm = new DetailViewModel();
            vm.Property = _service.Properties.Include(p => p.PropertyCategory).Include(p=>p.District).FirstOrDefault(p => p.Id == id);
            vm.PropertyDocument = _service.PropertyDocuments.Where(p => p.PropertyId == id).ToList();
            var basket=_service.Baskets.FirstOrDefault(p => p.UserId == value && p.PropertyId==vm.Property.Id);
            if (basket !=null)
            {
                ViewBag.color = "red";
            }
            if (color != null)
            {
                ViewBag.color = "red";
            }
            ViewBag.id=id;
            return View(vm);

        }

        [Route("/currency/{id}/{price}")]
        [Route("/property/currency/{id}/{price}")]
        public IActionResult Currency(string id, int price)
        {
            if (id == "AZN")
            {
                return Json(new
                {
                    error = false,
                    price = price
                });
            }
            else if (id == "USD")
            {
                var total = price * 0.59;
                return Json(new
                {
                    error = false,
                    price = total
                });
            }
            else if (id == "EUR")
            {
                var total = price * 0.54;
                return Json(new
                {
                    error = false,
                    price = total
                });
            }
            else if (id == "RUB")
            {
                var total = price * 47.86;
                return Json(new
                {
                    error = false,
                    price = total
                });
            }
            return View();
        }

    }
}
