using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.BLL.Exceptions;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;
using TheAddress.DAL.Repository.Interfaces;

namespace TheAddress.BLL.Services
{
    public class PropertyService : GenericService<PropertyDto, Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IGenericRepository<PropertyCategory> _categoryRepository;
        private readonly IGenericRepository<PropertyDocument> _documentRepository;
        public PropertyService(IGenericRepository<Property> genericRepository,
            IMapper mapper, ILogger<GenericService<PropertyDto, Property>> logger,
            IGenericRepository<PropertyCategory> categoryRepository, IPropertyRepository productRepository, IGenericRepository<PropertyDocument> documentRepository)
            : base(genericRepository, mapper, logger)
        {
            _categoryRepository = categoryRepository;
            _propertyRepository = productRepository;
            _documentRepository = documentRepository;
        }

        public async Task<List<PropertyCategoryDto>> GetCategoriesAsync()
        {

            var categories = await _categoryRepository.GetListAsync();

            var categoryDtos = _mapper.Map<List<PropertyCategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task<PropertyDto> GetPropertiesDetailByIdAsync(int id)
        {
            var product = await _propertyRepository.GetByIdAsync(id);
            List<PropertyDocument> documents = await _documentRepository.GetListAsync();
            product.PropertyDocuments = documents.Where(d => d.PropertyId == id).ToList();
            var productDto = _mapper.Map<PropertyDto>(product);

            return productDto;
        }

        public async Task<List<PropertyDto>> GetPropertiesByCategoryIdAsync(int id)
        {
            var products = await _propertyRepository.GetByCategoryIdAsync(id);
            var productDtos = _mapper.Map<List<PropertyDto>>(products);
            return productDtos;
        }

        public PropertyDto UpdateProperty(PropertyDto item)
        {
            try
            {

                Property entity = _mapper.Map<Property>(item);
                Property dbEntity = _propertyRepository.UpdateProperty(entity);

                return _mapper.Map<PropertyDto>(dbEntity);
            }
            catch (Exception ex)
            {
                throw new CustomException("BLL də əlavə edillərkən xəta yarandı. Xahiş olunur adminsitrator ilə əlaqə saxla.");
            }
        }
    }
}
