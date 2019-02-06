using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class DepartFile// : EntityBase
    {
        public int DepartmentId { get; private set; } 
        public Department Department { get; private  set; }

        public int TabulationId { get; private set; }
        public Tabulation Tabulation { get; private set; }

        protected DepartFile()
        {

        }

        public DepartFile(int deptId, int tabId)
        {
            DepartmentId = deptId;
            TabulationId = tabId;
        }
    }
}
