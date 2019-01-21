using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabHelper.Data.ORM;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models.Base;

namespace TabHelper.Data.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : EntityBase
    {
        protected readonly AppDbContext context;

        protected Repository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual TEntity GetById(int id)
        {
            var query = context.Set<TEntity>().Where(e => e.Id == id);
            return query.Any() ? query.First() : null;
        }

        public virtual IEnumerable<TEntity> All()
        {
            var query = context.Set<TEntity>();

            return query.Any() ? query.ToList() : new List<TEntity>();
        }

        public virtual void Save(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            context.Set<TEntity>().Add(entity);
        }

        public virtual IQueryable<TEntity> Get()
        {
            return context.Set<TEntity>().Where(e => !e.IsDeleted);
        }

        public virtual TEntity Get(int id)
        {
            return Get().SingleOrDefault(e => e.Id == id);
        }

        public virtual void Create(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = entity.CreatedAt;
            context.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);

            if (entity == null) return;
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = true;
        }

        public virtual Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
