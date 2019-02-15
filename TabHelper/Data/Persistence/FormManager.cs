using TabHelper.Data.ORM;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models.Entities;

namespace TabHelper.Data.Persistence
{
    public class FormManager : IFormManager
    {
        #region [ properties ]

        private readonly AppDbContext db;
        private readonly IRepository<Tabulation> tabRepo;
        private readonly IRepository<FormAttribute> formAttRepo;

        public FormManager(AppDbContext db, IRepository<Tabulation> tabRepo, IRepository<FormAttribute> formAttRepo)
        {
            this.db = db;
            this.tabRepo = tabRepo;
            this.formAttRepo = formAttRepo;
        }

        #endregion
    }
}
