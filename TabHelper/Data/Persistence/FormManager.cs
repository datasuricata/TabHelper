using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        #endregion

        #region [ ctor ]

        public FormManager(IRepository<Tabulation> tabRepo, IRepository<FormAttribute> formAttRepo, AppDbContext db)
        {
            this.db = db;
            this.tabRepo = tabRepo;
            this.formAttRepo = formAttRepo;
        }

        #endregion

        #region [ attributes ]

        public IEnumerable<FormAttribute> ListFormAtt()
        {
            return formAttRepo.List();
        }
        public IEnumerable<FormAttribute> ListFormAttF()
        {
            return formAttRepo.GetQueriable().Include(i => i.Forms).ThenInclude(i => i.Tabulation).ToList();
        }
        public IQueryable<FormAttribute> QueryFormAtt()
        {
            return formAttRepo.GetQueriable();
        }

        public FormAttribute Create(FormAttribute entity)
        {
            return formAttRepo.Create(entity);
        }

        #endregion

        #region [ forms ]

        public IEnumerable<Form> ListForms()
        {
            return db.Set<Form>().ToList();
        }
        public IEnumerable<Form> ListFormsF()
        {
            return db.Set<Form>().Include(i => i.FormAttribute).Include(i => i.Tabulation).ToList();
        }
        public IQueryable<Form> QueryForms()
        {
            return db.Set<Form>().AsQueryable();
        }

        #endregion

        #region [ tabulations ]

        public IEnumerable<Tabulation> ListTabs()
        {
            return tabRepo.List();
        }
        public IEnumerable<Tabulation> ListTabsF()
        {
            return tabRepo.GetQueriable().Include(i => i.Forms).ThenInclude(i => i.FormAttribute).ToList();
        }
        public IQueryable<Tabulation> QueryTabs()
        {
            return tabRepo.GetQueriable();
        }

        #endregion
    }
}
