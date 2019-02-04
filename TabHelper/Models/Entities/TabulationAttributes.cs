using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class TabulationAttributes : EntityBase
    {
        protected TabulationAttributes()
        {

        }
        
        public string Name { get; set; } 
        public string Title { get; set; } 
        public string Detail { get; set; } 
        public string Info { get; set; }

        public int Order { get; set; }
        public ComponentType ComponentType { get; set; }
        public ICollection<Forms> Forms { get; set; } = new List<Forms>();
    }
}
