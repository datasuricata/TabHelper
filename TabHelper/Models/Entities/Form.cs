using TabHelper.Models.Base;

namespace TabHelper.Models.Entities
{
    public class Form// : EntityBase
    {
        public int TabulationId { get; private set; }
        public Tabulation Tabulation {get; private set; }

        public int TabulationAttributesId { get; private set; }
        public FormAttribute TabulationAttributes { get; private set; }

        public Form()
        {

        }

        protected Form(int tabId, int tabAttId)
        {
            TabulationId = tabId;
            TabulationAttributesId = tabAttId;
        }
    }
}