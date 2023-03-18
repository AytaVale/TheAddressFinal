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

        public async Task<IViewComponentResult> InvokeAsync(int counts = 4, bool buy = false, bool rent = false)
        {
            var product = new List<Property>();
            if(counts > 4)
            {
                 product = db.Properties.Include(p => p.PropertyDocuments).ToList();
            }
            else
            {
                 product = db.Properties.Include(p => p.PropertyDocuments).Take(counts).ToList();
            }


            if(buy == true)
            {
                product = product.Where(p => p.Buy == true).ToList();
            }

            if(rent == true)
            {
                product = product.Where(p => p.Rent == true).ToList();
            }

            return View(product);
        }
    }
}
