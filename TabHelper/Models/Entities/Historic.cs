using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Historic : EntityBase
    {
        public string TabulationId { get; set; }
        public string UserId { get; set; }
        public string FormJson { get; set; }

        protected Historic()
        {
            
        }
    }
}
