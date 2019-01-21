using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Department : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Tabulation> Tabs { get; set; }

        public Department()
        {
            Users = new List<User>();
            Tabs = new List<Tabulation>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
