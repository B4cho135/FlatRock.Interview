using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Repositories
{
    public interface IRepository<TEntity, TEntityIdType> where TEntity : BaseEntity<TEntityIdType>
    {
        Task<TEntity?> GetByIdAsync(TEntityIdType Id);
        DbSet<TEntity> GetDbSet();
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntityIdType Id);
        Task<TEntity> CreateAsync(TEntity entity);
    }
}
