using System.Collections.Generic;
using TabHelper.Models.Entities;

namespace TabHelper.Models.ViewModel
{
    public class FormViewModel
    {
        public List<FormAttModel> FormAttibutes { get; set; } = new List<FormAttModel>();
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
        public string CreatedAt { get; set; }
        public int TabulationId { get; set; }

        public static explicit operator FormAttModel(FormAttribute v)
        {
            return v is null ? null : new FormAttModel
            {
                ComponentType = v.ComponentType,
                CreatedAt = v.CreatedAt?.ToString("dd/MM/yyyy HH:mm"),
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

    public class FormTabModel
    {
        public int TabId { get; set; }
        public int TabAttId { get; set; }
    }
}
