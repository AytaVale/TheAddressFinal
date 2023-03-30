using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.Data;
using TheAddress.DAL.Dtos;

namespace TheAddress.WebAdmin.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyService _service;
        private readonly AppDBContext db;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<PropertiesController> _logger;

        [Obsolete]
        public PropertiesController(AppDBContext db, IPropertyService service, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, ILogger<PropertiesController> logger)
        {
            _service = service;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            //var properties = await _service.GetListAsync();
            return View(await db.Properties.Include(x => x.PropertyCategory).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyCategory = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Region = new SelectList(db.Districts.ToList(), "Id", "Name");

            PropertyDto model = new()
            {
                PropertyCategoryDtos = await _service.GetCategoriesAsync()
            };
            return View(model);
        }
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Create(PropertyDto itemDto, List<IFormFile> files)
        {
            ViewBag.PropertyCategory = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Region = new SelectList(db.Districts.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {


                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string folderPath = @"Documents\PropertyImages";
                string fullPath = Path.Combine(wwwRootPath, folderPath);
                itemDto.PropertyDocuments = new List<PropertyDocumentDto>();
                foreach (var file in files)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(file.FileName);
                    string realPath = Path.Combine(fullPath, fileName + extension);

                    using (var fileStream = new FileStream(realPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    PropertyDocumentDto propertyDocument = new()
                    {
                        DocumentUrl = @"Documents/PropertyImages/" + fileName + extension,
                    };
                    itemDto.PropertyDocuments.Add(propertyDocument);

                }
                if (itemDto.PropertyDocuments.Count > 0)
                {
                    itemDto.ProfileDocPath = itemDto.PropertyDocuments[0].DocumentUrl;
                }

                await _service.AddAsync(itemDto);


                TempData["success"] = "Əmlak uğurla əlavə edildi";
                _logger.LogInformation($"Sistemə {itemDto.Id} N-li Id yaradılmışdır");

                return RedirectToAction("Index");
            }


            return View(itemDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.PropertyCategory = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Region = new SelectList(db.Districts.ToList(), "Id", "Name");
            if (id == null)
            {
                _logger.LogError("Sıfır gəlidiyi üçün xəta baş verdi");
                return NotFound();

            }

            var property = await _service.GetByIdAsync(id.Value);
            //var propertyImage = db.PropertyDocuments.Where(p=>p.PropertyId == id).ToList();

            if (property == null)
            {
                _logger.LogError($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                return NotFound();
            }

            PropertyDto model = new()
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Price = property.Price,
                Address = property.Address,
                RoomCount = property.RoomCount,
                Floor = property.Floor,
                Area = property.Area,
                PropertyCategoryId = property.PropertyCategoryId,
                PropertyCategoryDtos = await _service.GetCategoriesAsync(),
                DistrictId = property.DistrictId,
                DistrictDtos = property.DistrictDtos,
                Buy = property.Buy,
                Rent = property.Rent,
                PropertyDocuments = property.PropertyDocuments,
                ProfileDocPath = property.ProfileDocPath
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, PropertyDto itemDto, List<IFormFile> files)
        {
            ViewBag.PropertyCategory = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Region = new SelectList(db.Districts.ToList(), "Id", "Name");
            if (id != itemDto.Id)
            {
                _logger.LogInformation($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string folderPath = @"Documents\PropertyImages";
                string fullPath = Path.Combine(wwwRootPath, folderPath);

                itemDto.PropertyDocuments = itemDto.PropertyDocuments ?? new List<PropertyDocumentDto>();

                foreach (var file in files)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(file.FileName);
                    string realPath = Path.Combine(fullPath, fileName + extension);

                    using (var fileStream = new FileStream(realPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    PropertyDocumentDto productDocument = new()
                    {
                        DocumentUrl = @"Documents/PropertyImages/" + fileName + extension,
                    };

                    itemDto.PropertyDocuments.Add(productDocument);
                }

                if (itemDto.PropertyDocuments.Count > 0)
                {
                    itemDto.ProfileDocPath = itemDto.PropertyDocuments[0].DocumentUrl;
                }

                _service.Update(itemDto);

                TempData["success"] = "Əmlak uğurla dəyişdirildi";
                _logger.LogInformation($"Sistemdə {id} N-li Id  yenilənmişdir");
                return RedirectToAction("Index");
            }

            return View(itemDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var property = await _service.GetByIdAsync(id);

            return View(property);

        }
        [HttpPost]
        public IActionResult Delete(PropertyDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Əmlak uğurla silindi.";
            _logger.LogInformation($"Sistemdə {itemDto.Id} N-li Id silinmişdir");
            return RedirectToAction("Index");

        }
    }
}