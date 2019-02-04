using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabHelper.Data.ORM;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models.Base;

namespace TabHelper.Data.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase 
    {
        private readonly AppDbContext db;

        public Repository(AppDbContext db)
        {
            this.db = db;
        }

        public virtual TEntity GetById(int id)
        {
            return db.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public virtual IQueryable<TEntity> GetQueriable()
        {
            return db.Set<TEntity>().AsQueryable();
        }

        public virtual IEnumerable<TEntity> List()
        {
            var query = db.Set<TEntity>();
            return query.Any() ? query.ToList() : new List<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            db.Set<TEntity>().Attach(entity);
            db.Entry(entity);
        }

        public virtual void SoftExclude(TEntity entity)
        {
            entity.IsDeleted = true;
            db.Set<TEntity>().Attach(entity);
            db.Entry(entity);
        }

        public virtual void Exclude(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public virtual int Count()
        {
            return db.Set<TEntity>().Count();
        }

        public virtual Task<int> SaveAsync()
        {
            return db.SaveChangesAsync();
        }
    }
}
