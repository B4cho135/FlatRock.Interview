using Application.Persistance.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenericService<TEntity, TModel, TEntityIdType> : IGenericService<TEntity, TModel, TEntityIdType> where TEntity : BaseEntity<TEntityIdType>
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TEntity, TEntityIdType> _entityRepository;

        public GenericService(IRepository<TEntity, TEntityIdType> entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public virtual async Task<TModel> CreateAsync(TEntity entity)
        {
            var createdEntity = await _entityRepository.CreateAsync(entity);
            var response = _mapper.Map<TModel>(createdEntity);
            return response;

        }

        public virtual async Task<bool> DeleteAsync(TEntityIdType id)
        {
            return await _entityRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Gets all entities from database.
        /// This usually is not good, but since there is no pagination or filtering needed,
        /// I will implement it this way for development speed purposes
        /// </summary>
        /// <returns></returns>
        public async Task<List<TModel>> GetAllAsync()
        {
            return _mapper.Map<List<TModel>>(await _entityRepository.GetDbSet().ToListAsync());
        }

        public virtual async Task<TModel> GetAsync(TEntityIdType id)
        {
            var entity = await _entityRepository.GetByIdAsync(id);

            var response = _mapper.Map<TModel>(entity);

            return response;
        }

        public virtual async Task<TModel> UpdateAsync(TEntity entity)
        {
            var updatedEntity = await _entityRepository.UpdateAsync(entity);
            var response = _mapper.Map<TModel>(updatedEntity);
            return response;
        }
    }
}
