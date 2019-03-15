using System;
using System.Collections.Generic;
using TabHelper.Models.Entities;
using TabHelper.Models.ViewModel;

namespace TabHelper.Models
{
    public class TabViewModel
    {
        public List<TabModel> Tabulations { get; set; }
        public List<FormAttModel> FormsAtts { get; set; }
    }

    public class TabModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Observation { get; set; }

        public bool IsCheck { get; set; }

        public List<FormTabModel> Forms { get; set; } = new List<FormTabModel>();

        public static explicit operator TabModel(Tabulation v)
        {
            return v == null ? null : new TabModel
            {
                Id = v.Id,
                Name = v.Name,
                Observation = v.Observation,
            };
        }
        //public ICollection<DepartTab> DepartmentTabulations { get; set; } = new List<DepartTab>();
    }
}
