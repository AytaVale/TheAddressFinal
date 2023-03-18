using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;

namespace TheAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyCategoriesController : ControllerBase
    {
        private readonly IGenericService<PropertyCategoryDto, PropertyCategory> _service;


        public PropertyCategoriesController(IGenericService<PropertyCategoryDto, PropertyCategory> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyCategoryDto>>> GetList()
        {
            var response = await _service.GetListAsync();
            return response;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PropertyCategoryDto>> GetByIdAsync(int id)

        {
            if (id == 0)
            {
                return BadRequest();
            }

            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyCategoryDto>> Create(PropertyCategoryDto itemDto)
        {

            var response = await _service.AddAsync(itemDto);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PropertyCategoryDto> Update(int id, [FromBody] PropertyCategoryDto obj)
        {
            if (id == 0 || id != obj.Id)
            {
                return BadRequest();
            }

            var response = _service.GetByIdAsync(id).Result;
            if (response == null)
            {
                return NotFound();
            }
            response = _service.Update(obj);
            return Ok(response); ;
        }

        [HttpDelete("{id:int}")]

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var response = _service.GetByIdAsync(id).Result;
            if (response == null)
            {
                return NotFound();
            }

            _service.Delete(id);
            return NoContent();
        }
    }
}
