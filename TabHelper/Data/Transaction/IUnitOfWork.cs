using System.Threading.Tasks;

namespace TabHelper.Data.Transaction
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
