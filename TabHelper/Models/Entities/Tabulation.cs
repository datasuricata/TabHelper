using System.Collections.Generic;
using System.Collections.ObjectModel;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Tabulation : EntityBase
    {
        #region [ properties ]

        public string Name { get; private set; }
        public string Observation { get; private set; }

        public ICollection<FormTab> FormTabs { get; private set; } = new Collection<FormTab>();
        public ICollection<DepartTab> DepartmentTabulations { get; private set; } = new Collection<DepartTab>();

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
    