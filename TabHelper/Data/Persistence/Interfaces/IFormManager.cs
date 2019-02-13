using System.Collections.Generic;
using System.Linq;
using TabHelper.Models.Entities;

namespace TabHelper.Data.Persistence.Interfaces
{
    public interface IFormManager
    {
        IEnumerable<FormAttribute> ListFormAtt();
        IEnumerable<FormAttribute> ListFormAttF();
        IQueryable<FormAttribute> QueryFormAtt();

        IEnumerable<Form> ListForms();
        IEnumerable<Form> ListFormsF();
        IQueryable<Form> QueryForms();

        IEnumerable<Tabulation> ListTabs();
        IEnumerable<Tabulation> ListTabsF();
        IQueryable<Tabulation> QueryTabs();
    }
}
