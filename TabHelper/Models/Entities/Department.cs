using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Department : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<DepartFile> DepartmentTabulations { get; set; } = new List<DepartFile>();

        protected Department()
        {

        }

        public Department(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Department Edit(Department dept, string name, string desc)
        {
            dept.Name = name; 
            dept.Description = desc;

            return dept;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
