using System.Collections.Generic;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Department : EntityBase
    {
        #region [ properties ]

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<DepartTab> DepartmentTabulations { get; set; } = new List<DepartTab>();

        #endregion

        #region [ ctor ]

        protected Department()
        {

        }
        public Department(string name, string description)
        {
            Validate(name);
            SetProperties(name, description);
        }

        #endregion

        #region [ methods ]

        public void Edit(Department dept)
        {
            Name = dept.Name; 
            Description = dept.Description;
        }

        public void Validate(string name)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Defina um nome ao novo departamento");
        }
        public void SetProperties(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
