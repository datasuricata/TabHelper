using System.Collections.Generic;
using System.Linq;
using TabHelper.Models.Entities;

namespace TabHelper.Models.ViewModel
{
    public class FormViewModel
    {
        public List<FormModel> Forms { get; set; } = new List<FormModel>();
    }

    public class FormAttModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int Repeat { get; set; }
        public string Name { get; set; }
        public ComponentType ComponentType { get; set; }


        public string Title { get; set; }
        public string Value { get; set; }

        public string Info { get; set; }
        public string Detail { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsNumeric { get; set; }
        public int FormId { get; set; }

        public static explicit operator FormAttModel(FormAttribute v)
        {
            return v is null ? null : new FormAttModel
            {
                ComponentType = v.ComponentType,
                Detail = v.Detail,
                Id = v.Id,
                Info = v.Info,
                IsDeleted = v.IsDeleted,
                Name = v.Name,
                Title = v.Title,
                Value = v.Value,
                IsNumeric = v.IsNumeric,
                Order = v.Order,
                Repeat = v.Repeat,
                FormId = v.FormId,
            };
        }
    }

    public class FormTabModel
    {
        public int TabulationId { get; set; }
        public TabModel Tabulation { get; set; }

        public int FormId { get; set; }
        public FormModel Form { get; set; }

        public static explicit operator FormTabModel(FormTab v)
        {
            return v is null ? null : new FormTabModel
            {
                Tabulation = (TabModel)v.Tabulation,
                TabulationId = v.TabulationId,
                Form = (FormModel)v.Form,
                FormId = v.FormId,
            };
        }
    }

    public class FormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<string> Tabulations { get; set; } = new List<string>();

        public static explicit operator FormModel(Form v)
        {
            return v == null ? null : new FormModel
            {
                Id = v.Id,
                Code = v.Code,
                Name = v.Name,
                Tabulations = v.FormTabs.Select(x => x.Tabulation.Name).ToList(),
            };
        }
    }
}
