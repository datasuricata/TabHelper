using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Tabulation : EntityBase
    {
        protected Tabulation()
        {

        }

        public string Name { get; set; }
        public string Observation { get; set; }
        public ICollection<Forms> Forms { get; set; } = new List<Forms>();
        public ICollection<DepartmentTabulation> DepartmentTabulations { get; set; } = new List<DepartmentTabulation>();
    }
}
    