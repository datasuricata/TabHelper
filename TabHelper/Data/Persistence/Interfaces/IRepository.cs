using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabHelper.Data.Persistence.Interfaces
{
    public interface IRepository<TEntity> 
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetQueriable();
        IEnumerable<TEntity> List();

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity SoftExclude(TEntity entity);
        TEntity Exclude(TEntity entity);

        IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);

        int Count();
        Task<int> SaveAsync();
    }
}
