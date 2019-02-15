using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class FormTab
    {
        #region [ properties ]

        public int TabulationId { get; private set; }
        public Tabulation Tabulation {get; private set; }

        public int FormId { get; private set; }
        public Form Form { get; private set; }

        #endregion

        #region [ ctor ]

        protected FormTab()
        {

        }

        public FormTab(Tabulation tabulation, Form form)
        {
            Validate(tabulation, form);
            SetProperties(tabulation, form);
        }
        public FormTab(int tabulationId, int formId)
        {
            Validate(tabulationId, formId);
            SetProperties(tabulationId, formId);
        }

        #endregion

        #region [ methods ]

        private void Validate(int tabulationId, int formId)
        {
            DomainValidation.When(tabulationId == 0, "Selecione uma tabula��o para criar o formul�rio");
            DomainValidation.When(formId == 0, "Selecione o atributo que deseja adicionar no formul�rio");
        }
        private void Validate(Tabulation tabulation, Form form)
        {
            DomainValidation.When(tabulation is null, "Selecione uma tabula��o para criar o formul�rio");
            DomainValidation.When(form is null, "Selecione o atributo que deseja adicionar no formul�rio");
        }

        private void SetProperties(int tabulationId, int formId)
        {
            TabulationId = tabulationId;
            FormId = formId;
        }
        private void SetProperties(Tabulation tabulation, Form Form)
        {
            Tabulation = tabulation;
            this.Form = Form;
        }

        #endregion
    }
}