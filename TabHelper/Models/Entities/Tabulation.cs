using System.Collections.Generic;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Tabulation : EntityBase
    {
        public Tabulation(string name, string observation)
        {
            Name = name;
            Observation = observation;
        }

        protected Tabulation()
        {

        }

        public string Name { get; private set; }
        public string Observation { get; private set; }
        public ICollection<Form> Forms { get; private set; } = new List<Form>();
        public ICollection<DepartFile> DepartmentTabulations { get; private set; } = new List<DepartFile>();
    }
}
    