using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;

namespace TheAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IGenericService<PropertyDto, Property> _service;
        private readonly ILogger<PropertiesController> _logger;

        public PropertiesController(IGenericService<PropertyDto, Property> service, ILogger<PropertiesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyDto>>> GetList()
        {
            var response = await _service.GetListAsync();
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PropertyDto>> GetByIdAsync(int id)

        {
            if (id == 0)
            {
                _logger.LogError("Sıfır gəlidiyi üçün xəta baş verdi");
                return BadRequest();
            }

            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                _logger.LogError($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                return NotFound();
            }
            _logger.LogInformation($"Sistemdə {id} N-li Id məlumat çağrılmışdır");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyDto>> Create(PropertyDto itemDto)
        {

            var response = await _service.AddAsync(itemDto);
            _logger.LogInformation($"Sistemə {itemDto.Id} N-li Id yaradılmışdır");

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PropertyDto> Update(int id, [FromBody] PropertyDto obj)
        {
            if (id == 0 || id != obj.Id)
            {
                _logger.LogError("Sıfır gəlidiyi üçün xəta baş verdi");
                return BadRequest();
            }

            var response = _service.GetByIdAsync(id).Result;
            if (response == null)
            {
                _logger.LogError($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                return NotFound();
            }

            response = _service.Update(obj);
            _logger.LogInformation($"Sistemdə {id} N-li Id yenilənmişdir");
            return Ok(response); ;
        }

        [HttpDelete("{id:int}")]

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Sıfır gəlidiyi üçün xəta baş verdi");
                return BadRequest();
            }

            var response = _service.GetByIdAsync(id).Result;
            if (response == null)
            {
                _logger.LogError($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                return NotFound();
            }

            _service.Delete(id);
            _logger.LogInformation($"Sistemdə {id} N-li Id silinmişdir");
            return NoContent();
        }
    }
}