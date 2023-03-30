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
            TempData["contact"] = contacts;
            return View();
        }

    }
}