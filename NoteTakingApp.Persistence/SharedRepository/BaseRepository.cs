using Microsoft.EntityFrameworkCore;
using NoteTakingApp.Application.Abstractions.IRepository;
using NoteTakingApp.Domain.Entities;
using NoteTakingApp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Persistence.SharedRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly NoteTakingAppDbContext context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(NoteTakingAppDbContext context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<int> AddAsync(T model)
        {
            await _dbSet.AddAsync(model);

            return await SaveDatabaseChanges();
        }




        public async Task<int> DeleteAsync(Guid id)
        {
            _dbSet.Remove(new T() { Id = id });

            return await SaveDatabaseChanges();
        }




        public async Task<int> DeleteAsync(T model)
        {
            _dbSet.Remove(model);

            return await SaveDatabaseChanges();
        }




        public async Task<int> DeleteRangeAsync(List<Guid> ids)
        {
            List<T> entities = new List<T>();
            foreach (var id in ids)
            {
                entities.Add(new T() { Id = id });
            }

            _dbSet.RemoveRange(entities);

            return await SaveDatabaseChanges();
        }




        public async Task<int> DeleteRangeAsync(List<T> models)
        {
            _dbSet.RemoveRange(models);

            return await SaveDatabaseChanges();
        }




        public async Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => _dbSet.Where(expression));
        }




        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }




        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await Task.Run(() => _dbSet);
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task<int> UpdateAsync(T model)
        {
            _dbSet.Update(model);
            return await SaveDatabaseChanges();
        }




        #region Helper function
        private async Task<int> SaveDatabaseChanges() => await context.SaveChangesAsync();
        #endregion Helper function

    }
}
