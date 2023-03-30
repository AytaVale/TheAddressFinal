using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;

namespace TheAddress.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly AppDBContext db;
        public CartViewComponent(AppDBContext db)
        {
           this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int counts = 6,int id=0, bool buy = false, bool rent = false, int category = 0, bool notHome = false, int room = 0, int region = 0, int min = 0, int max = 0)
        {
            var product = new List<Property>();
            if(counts > 6)
            {
                 product = db.Properties.Include(p => p.PropertyDocuments).ToList();
            }
            else
            {
                 product = db.Properties.Include(p => p.PropertyDocuments).Take(counts).ToList();
            }
            if (id>0)
            {
                product=product.Where(p=>p.Id==id).ToList();
            }

            if(buy == true)
            {
                product = product.Where(p => p.Buy == true).ToList();
            }

            if(rent == true)
            {
                product = product.Where(p => p.Rent == true).ToList();
            }

            if(category > 0) {
                product = product.Where(p => p.PropertyCategoryId == category).ToList();
            }
            if (room > 0)
            {
                product = product.Where(p => p.RoomCount == room.ToString()).ToList();
            }
            if (region > 0)
            {
                product = product.Where(p => p.DistrictId == region).ToList();
            }

            if(product.Count == 0)
            {
                ViewBag.Product = "Axtardığınız parametrlər üzrə əmlak tapılmadı.";
            }
            return View(product);
        }
    }
}
