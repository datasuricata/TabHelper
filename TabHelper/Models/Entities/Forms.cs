using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Forms : EntityBase
    {
        public int TabulationId { get; set; }
        public Tabulation Tabulation {get; set;}

        public int TabulationAttributesId { get; set; }
        public TabulationAttributes TabulationAttributes { get; set; }
    }
}