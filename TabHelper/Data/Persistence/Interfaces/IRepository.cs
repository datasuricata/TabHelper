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
        void Create(TEntity entity);
        void Update(TEntity entity);
        void SoftExclude(TEntity entity);
        void Exclude(TEntity entity);

        int Count();
        Task<int> SaveAsync();
    }
}
