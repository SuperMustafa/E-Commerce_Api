using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data;

namespace Persistance.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _DbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _DbContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (trackChanges)
            {
                return await _DbContext.Set<TEntity>().ToListAsync();
            }
            return await _DbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
        {
            return await SpecificationsEvalutor.GetQuery<TEntity>(_DbContext.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Specifications<TEntity> specifications)
        {
            return await SpecificationsEvalutor.GetQuery<TEntity>(_DbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

    

        public void DeleteAsync(TEntity entity)
        {
            _DbContext.Set<TEntity>().Remove(entity);
        }

   



        public void UpdateAsync(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
        }

        public async Task<int> GetCountAsync(Specifications<TEntity> specifications)
        
           => await ApplySpecificatons(specifications).CountAsync();
     
        
        private IQueryable<TEntity> ApplySpecificatons(Specifications<TEntity> specifications)
        {
            return SpecificationsEvalutor.GetQuery<TEntity>(_DbContext.Set<TEntity>(), specifications);
        }
    }
}
