using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;

namespace TheAddress.BLL.Services.Interfaces
{
    public interface IPropertyService : IGenericService<PropertyDto, Property>
    {
        public Task<List<PropertyCategoryDto>> GetCategoriesAsync();

        public Task<List<PropertyDto>> GetPropertiesByCategoryIdAsync(int id);
        public Task<PropertyDto> GetPropertiesDetailByIdAsync(int id);

    }
}
