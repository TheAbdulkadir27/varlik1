using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly VarlikZimmetEnginContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(VarlikZimmetEnginContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {

            await _dbSet.AddAsync(entity);
        }

        public virtual async void Update(T entity)
        {
            _context.Entry<T>(entity!).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public Varlık_ZimmetDepoYonetimi.UI.Models.VarlikZimmetEnginContext GetContext() => _context;
    }
}