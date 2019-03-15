using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TabHelper.Data.ORM;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models.Entities;

namespace TabHelper.Data.Persistence
{
    public class FormManager : IFormManager
    {
        #region [ properties ]

        private readonly AppDbContext db;

        #endregion

        public FormManager(AppDbContext db)
        {
            this.db = db;
        }

        public FormTab Register(int formId, int tabId)
        {
            var entity = new FormTab(tabId, formId);
            db.Set<FormTab>().Add(entity);
            return entity;
        }

        public IEnumerable<FormTab> ListFormTabs()
        {
            var query = db.Set<FormTab>();
            return query.Include(x => x.Form).Include(x => x.Tabulation).ToList();
        }
    }
}
