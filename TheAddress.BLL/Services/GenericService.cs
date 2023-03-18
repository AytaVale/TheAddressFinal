using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.BLL.Exceptions;
using TheAddress.BLL.Helper;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.DAL.Repository.Interfaces;

namespace TheAddress.BLL.Services
{
    public class GenericService
        <TDto, TEntity> : IGenericService<TDto, TEntity> where TDto : class where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        protected readonly IMapper _mapper;
        private readonly ILogger<GenericService<TDto, TEntity>> _logger;
        public GenericService(IGenericRepository<TEntity> genericRepository, IMapper mapper, ILogger<GenericService<TDto, TEntity>> logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TDto> AddAsync(TDto item)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(item);
                entity.SetValue<TEntity>("InsertDate", DateTime.Now);
                TEntity dbEntity = await _genericRepository.AddAsync(entity);
                return _mapper.Map<TDto>(dbEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw new CustomException("BLL də əlavə edillərkən xəta yarandı. Xahiş olunur adminsitrator ilə əlaqə saxlayın.");
            }


        }

        public void Delete(int id)
        {
            _genericRepository.Delete(id);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            TEntity entity = await _genericRepository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<List<TDto>> GetListAsync()
        {
            var list = await _genericRepository.GetListAsync();
            List<TDto> dtos = _mapper.Map<List<TDto>>(list);
            return dtos;
        }

        public TDto Update(TDto item)
        {
            TEntity entity = _mapper.Map<TEntity>(item);
            entity.SetValue<TEntity>("UpdateDate", DateTime.Now);
            TEntity dbEntity = _genericRepository.Update(entity);

            return _mapper.Map<TDto>(dbEntity);
        }
    }
}