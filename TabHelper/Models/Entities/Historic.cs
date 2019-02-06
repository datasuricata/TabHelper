using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Historic : EntityBase
    {
        #region [ properties ]

        public int TabulationId { get; private set; }
        public int UserId { get; private set; }
        public string FormJson { get; private set; }

        #endregion

        #region [ ctor ]

        protected Historic()
        {

        }

        public Historic(int tabulationId, int userId, string formJson)
        {
            TabulationId = tabulationId;
            UserId = userId;
            FormJson = formJson;
        }

        #endregion

        #region [ methods ]

        #endregion
    }
}
