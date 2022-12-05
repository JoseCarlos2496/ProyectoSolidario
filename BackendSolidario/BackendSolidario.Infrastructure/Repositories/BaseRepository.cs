using Microsoft.EntityFrameworkCore;
using BackendSolidario.Infrastructure.Data;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Infrastructure.Exceptions;

namespace BackendSolidario.Infrastructure.Repositories {
    public class BaseRepository<T, I> : IBaseRepository<T, I> where T : BaseEntity<I> {

        private readonly BcoSolidarioDbContext _db;
        private readonly DbSet<T> _entities;
        public BaseRepository(BcoSolidarioDbContext db) {
            _db = db;
            _entities = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            IEnumerable<T>? lst = await _entities.OrderBy(x => x.Id).ToListAsync();

            return lst;
        }

        public async Task<T> GetByIdAsync(I id) {
            T? entity = await _entities.FindAsync(id);

            if (entity == null) throw new NotFoundException(Constants.NOTFOUND);

            return entity;
        }

        public async Task<T> AddAsync(T entity) {
            _entities.Add(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(T entity) {
            _entities.Update(entity);
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(I id) {
            T entity = await GetByIdAsync(id);

            _entities.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}