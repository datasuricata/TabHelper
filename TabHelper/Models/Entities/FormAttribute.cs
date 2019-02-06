using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class FormAttribute : EntityBase
    {
        public FormAttribute(int order, string name, ComponentType componentType, string title, string value, string info, string detail)
        {
            Order = order;
            Name = name;
            ComponentType = componentType;
            Title = title;
            Value = value;
            Info = info;
            Detail = detail;
        }

        protected FormAttribute()
        {

        }

        public int Order { get; private set; }
        public string Name { get; private set; }
        public ComponentType ComponentType { get; private set; }

        public string Title { get; private set; } 
        public string Value { get; private set; }

        public string Info { get; private set; }
        public string Detail { get; private set; } 

        public ICollection<Form> Forms { get; private set; } = new List<Form>();
    }
}
