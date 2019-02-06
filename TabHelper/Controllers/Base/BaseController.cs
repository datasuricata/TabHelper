using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TabHelper.Data.Transaction;

namespace TabHelper.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork uow;
        public BaseController(IUnitOfWork uow) => this.uow = uow;

        protected List<SelectListItem> GetDropDown<T>(List<T> listT, string propText, string propValue)
        {
            var list = new List<SelectListItem>();

            var text = string.Empty;
            var value = string.Empty;

            foreach (var x in listT)
            {
                foreach (var p in x.GetType().GetProperties())
                    if (p.Name == propText)
                        text = p.GetValue(x).ToString();

                foreach (var p in x.GetType().GetProperties())
                    if (p.Name == propValue)
                        value = p.GetValue(x).ToString();

                list.Add(new SelectListItem(text, value));
            }
            return list;
        }
    }
}