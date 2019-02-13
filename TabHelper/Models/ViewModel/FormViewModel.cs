using System.Collections.Generic;
using TabHelper.Models.Entities;

namespace TabHelper.Models.ViewModel
{
    public class FormViewModel
    {
        public List<FormAttModel> FormAttibutes { get; set; } = new List<FormAttModel>();
        public List<FormModel> Forms { get; set; } = new List<FormModel>();
    }

    public class FormAttModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public ComponentType ComponentType { get; set; }

        public string Title { get; set; }
        public string Value { get; set; }

        public string Info { get; set; }
        public string Detail { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsNumeric { get; set; }
        public int TabulationId { get; set; }

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
            };
        }
    }

    public class FormModel
    {
        public int TabulationId { get; set; }
        public TabModel Tabulation { get; set; }

        public int FormAttributeId { get; set; }
        public FormAttModel FormAttribute { get; set; }

        public int Order { get; set; }
        public int Repeat { get; set; }

        public static explicit operator FormModel(Form v)
        {
            return v is null ? null : new FormModel
            {
                FormAttribute = (FormAttModel)v.FormAttribute,
                FormAttributeId = v.FormAttributeId,
                Order = v.Order,
                Repeat = v.Repeat,
                Tabulation = (TabModel)v.Tabulation,
                TabulationId = v.TabulationId,
            };
        }
    }
}
