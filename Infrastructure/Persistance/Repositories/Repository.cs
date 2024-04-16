using Application.Persistance.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class Repository<TEntity, TEntityIdType> : IRepository<TEntity, TEntityIdType> where TEntity : BaseEntity<TEntityIdType>
    {
        protected readonly FlatRockDbContext dbContext;
        public Repository(FlatRockDbContext context)
        {
            dbContext = context;
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;

            var createdEntity = await GetDbSet().AddAsync(entity);

            await dbContext.SaveChangesAsync(default);

            return createdEntity.Entity;
        }

        public async Task<bool> DeleteAsync(TEntityIdType Id)
        {
            try
            {
                var entity = await GetByIdAsync(Id);
                if (entity == null)
                {
                    return false;
                }
                entity.IsDeleted = true;

                await UpdateAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TEntity?> GetByIdAsync(TEntityIdType Id)
        {
            var entity = await GetDbSet().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id!.Equals(Id));

            return entity;
        }

        public DbSet<TEntity> GetDbSet()
        {
            return dbContext.Set<TEntity>();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;

            var updatedEntity = GetDbSet().Update(entity);
            await dbContext.SaveChangesAsync(default);
            return updatedEntity.Entity;
        }
    }
}
