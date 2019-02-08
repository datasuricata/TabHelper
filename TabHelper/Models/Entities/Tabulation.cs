using System.Collections.Generic;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Tabulation : EntityBase
    {
        #region [ properties ]

        public string Name { get; private set; }
        public string Observation { get; private set; }

        public ICollection<Form> Forms { get; private set; } = new List<Form>();
        public ICollection<DepartTab> DepartmentTabulations { get; private set; } = new List<DepartTab>();

        #endregion

        #region [ ctor ]

        public Tabulation(string name, string observation)
        {
            Validate(name);
            SetProperties(name, observation);
        }

        protected Tabulation()
        {

        }

        #endregion

        #region [ methods ]

        private void Validate(string name)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Defina um nome ao tabulação");
        }

        private void SetProperties(string name, string observation)
        {
            Name = name;
            Observation = observation;
        }

        #endregion

    }
}
    