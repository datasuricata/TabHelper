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

        public FormManager(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region [ forms ]

        public IEnumerable<Form> ListForms()
        {
            return db.Set<Form>().ToList();
        }

        #endregion

        #region [ tabulations ]

        public IEnumerable<Tabulation> ListTabs()
        {
            return tabRepo.List();
        }

        #endregion

        #region [ attributes ]

        public IEnumerable<FormAttribute> ListFormAtt()
        {
            return formAttRepo.List();
        }

        #endregion
    }
}
