using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IGenericService<TEntity, TModel, TEntityIdType>
    {
        Task<TModel> GetAsync(TEntityIdType id);
        Task<TModel> CreateAsync(TEntity request);
        Task<TModel> UpdateAsync(TEntity request);
        Task<bool> DeleteAsync(TEntityIdType id);
        Task<List<TModel>> GetAllAsync();
    }
}
