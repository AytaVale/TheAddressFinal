using Microsoft.AspNetCore.Mvc;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;

namespace TheAddress.WebAdmin.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly IGenericService<DistrictDto, District> _service;
        private readonly ILogger<DistrictsController> _logger;

        public DistrictsController(IGenericService<DistrictDto, District> service, ILogger<DistrictsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var districts = await _service.GetListAsync();
            return View(districts);
        }

        public async Task<IActionResult> Update(int id)
        {
            var districts = await _service.GetByIdAsync(id);
            _logger.LogInformation($"Sistemdə {id} N-li Id məlumat çağrılmışdır");
            return View(districts);

        }

        [HttpPost]
        public IActionResult Update(DistrictDto itemDto)
        {
            var districts = _service.Update(itemDto);
            if (districts != null)
            {
                TempData["success"] = "Rayon uğurla dəyişdirildi.";
                _logger.LogInformation($"Sistemdə {itemDto.Id} N-li Id yenilənmişdir");
                return RedirectToAction("Index");
            }
            _logger.LogError($"Sistemdə olmayan {itemDto.Id} N-li Id çağrılmışdır");
            return View(districts);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var districts = await _service.GetByIdAsync(id);

            return View(districts);

        }
        [HttpPost]
        public IActionResult Delete(DistrictDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Rayon uğurla silindi.";
            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DistrictDto itemDto)
        {

            var districts = await _service.AddAsync(itemDto);
            if (districts != null)
            {
                TempData["success"] = "Rayon uğurla əlavə edildi.";
                _logger.LogError($"Sistemə {itemDto.Id} N-li Id yaradılmışdır");
                return RedirectToAction("Index");
            }
            return Ok(districts);
        }
    }
}