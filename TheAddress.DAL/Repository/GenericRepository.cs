using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Repository.Interfaces;

namespace TheAddress.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDBContext _dbContext;
        protected readonly DbSet<TEntity> _entities;
        public GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity item)
        {
            await _entities.AddAsync(item);
            _dbContext.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            var dbItem = _entities.Find(id);
            _entities.Remove(dbItem);
            _dbContext.SaveChanges();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var dbItem = await _entities.FindAsync(id);
            return dbItem;
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            var dbItem = await _entities.ToListAsync();
            return dbItem;
        }

        public TEntity Update(TEntity item)
        {
            var dbEntity = _entities.Find(item.Id);
            item.InsertDate = dbEntity.InsertDate;
            _entities.Update(item);
            _dbContext.SaveChanges();
            return item;
        }
    }
}
