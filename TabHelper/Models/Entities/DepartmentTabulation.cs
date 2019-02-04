using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class DepartmentTabulation : EntityBase
    {
        public int DepartmentId { get; set; } 
        public Department Department { get; set; }

        public int TabulationId { get; set; }
        public Tabulation Tabulation { get; set; }
    }
}
