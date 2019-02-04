using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabHelper.Models.Base
{
    public abstract class EntityBase
    {
        #region [ attributes ]

        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        #endregion

        #region [ ctor ]

        protected EntityBase()
        {

        }

        #endregion

        #region [ methods ]

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        }

        #endregion

    }
}
