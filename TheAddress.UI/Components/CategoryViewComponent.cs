using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using TheAddress.DAL.Data;
using Microsoft.EntityFrameworkCore;
using TheAddress.UI.Models;

namespace TheAddress.UI.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        readonly AppDBContext db;
        public CategoryViewComponent(AppDBContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
        

            var ct = db.PropertyCategories.Include(p=>p.Properties).Where(p=> p.Properties.Count > 0).Take(6);
       
            return View(ct);
        }
    }
}
