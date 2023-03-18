using Microsoft.AspNetCore.Mvc;
using TheAddress.BLL.Services.Interfaces;

namespace TheAddress.UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int id)
        {
            var data = await _service.GetPropertiesByCategoryIdAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _service.GetPropertiesDetailByIdAsync(id);
            return View(data);
        }
    }
}
