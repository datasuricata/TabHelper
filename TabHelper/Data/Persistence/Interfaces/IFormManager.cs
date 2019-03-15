using System.Collections.Generic;
using TabHelper.Models.Entities;

namespace TabHelper.Data.Persistence.Interfaces
{
    public interface IFormManager
    {
        FormTab Register(int formId, int tabId);
        IEnumerable<FormTab> ListFormTabs();
    }
}
