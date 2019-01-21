using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabHelper.Data.Persistence.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> All();
        IQueryable<TEntity> Get();
        TEntity Get(int id);
        void Create(TEntity record);
        void Update(TEntity record);
        void Delete(int id);
        void Save(TEntity entity);
        Task<int> SaveAsync();
    }
}
