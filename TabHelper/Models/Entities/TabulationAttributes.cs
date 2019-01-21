using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class TabulationAttributes : EntityBase
    {
        public TabulationAttributes()
        {
            Forms = new List<Forms>();
        }
        
        public string Name { get; set; } 
        public string Title { get; set; } 
        public string Detail { get; set; } 
        public string Info { get; set; }

        public int Order { get; set; }
        public ComponentType ComponentType { get; set; }
        public virtual ICollection<Forms> Forms { get; set; }
    }
}
