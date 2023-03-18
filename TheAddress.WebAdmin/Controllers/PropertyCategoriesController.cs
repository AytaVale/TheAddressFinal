using Microsoft.AspNetCore.Mvc;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;

namespace TheAddress.WebAdmin.Controllers
{
    public class PropertyCategoriesController : Controller
    {
        private readonly IGenericService<PropertyCategoryDto, PropertyCategory> _service;
        private readonly ILogger<PropertyCategoriesController> _logger;

        public PropertyCategoriesController(IGenericService<PropertyCategoryDto, PropertyCategory> service, ILogger<PropertyCategoriesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _service.GetByIdAsync(id);
            _logger.LogInformation($"Sistemdə {id} N-li Id məlumat çağrılmışdır");
            return View(category);

        }

        [HttpPost]
        public IActionResult Update(PropertyCategoryDto itemDto)
        {
            var category = _service.Update(itemDto);
            if (category != null)
            {
                TempData["success"] = "Kateqoriya uğurla dəyişdirildi.";
                _logger.LogInformation($"Sistemdə {itemDto.Id} N-li Id yenilənmişdir");
                return RedirectToAction("Index");
            }
            _logger.LogError($"Sistemdə olmayan {itemDto.Id} N-li Id çağrılmışdır");
            return View(category);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);

            return View(category);

        }
        [HttpPost]
        public IActionResult Delete(PropertyCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Kateqoriya uğurla silindi.";
            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyCategoryDto itemDto)
        {

            var category = await _service.AddAsync(itemDto);
            if (category != null)
            {
                TempData["success"] = "Kateqoriya uğurla əlavə edildi.";
                _logger.LogError($"Sistemə {itemDto.Id} N-li Id yaradılmışdır");
                return RedirectToAction("Index");
            }
            return Ok(category);
        }
    }
}