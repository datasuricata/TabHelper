using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class DepartTab
    {
        #region [ properties ]

        public int DepartmentId { get; private set; } 
        public Department Department { get; private  set; }

        public int TabulationId { get; private set; }
        public Tabulation Tabulation { get; private set; }

        #endregion

        #region [ ctor ]

        protected DepartTab()
        {

        }

        public DepartTab(Department department, Tabulation tabulation)
        {
            Department = department;
            Tabulation = tabulation;
        }

        public DepartTab(int departmentId, int tabulationId)
        {
            DepartmentId = departmentId;
            TabulationId = tabulationId;
        }

        #endregion

        #region [ methods ]

        private void Validate(Department department, Tabulation tabulation)
        {
            DomainValidation.When(department is null, "Selecione um departamento");
            DomainValidation.When(tabulation is null, "Selecione uma tabulação");
        }
        private void SetProperties(Department department, Tabulation tabulation)
        {
            Department = department;
            Tabulation = tabulation;
        }

        private void Validate(int departmentId, int tabulationId)
        {
            DomainValidation.When(departmentId == 0, "Selecione um departamento");
            DomainValidation.When(tabulationId == 0, "Selecione uma tabulação");
        }
        private void SetProperties(int departmentId, int tabulationId)
        {
            DepartmentId = departmentId;
            TabulationId = tabulationId;
        }

        #endregion
    }
}
