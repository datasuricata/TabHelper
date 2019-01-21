using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Tabulation : EntityBase
    {
        public Tabulation()
        {
            Forms = new List<Forms>();   
        }

        public string Name { get; set; }
        public string Observation { get; set; }
        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Forms> Forms { get; set; }
    }
}
    