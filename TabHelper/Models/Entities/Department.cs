using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Department : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Tabulation> Tabs { get; set; } = new List<Tabulation>();
        public ICollection<DepartmentTabulation> DepartmentTabulations { get; set; } = new List<DepartmentTabulation>();

        public Department()
        {

        }

        public Department(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
