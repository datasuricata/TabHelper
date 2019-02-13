using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Form
    {
        #region [ properties ]

        public int TabulationId { get; private set; }
        public Tabulation Tabulation {get; private set; }

        public int FormAttributeId { get; private set; }
        public FormAttribute FormAttribute { get; private set; }

        public int Order { get; private set; }
        public int Repeat { get; private set; }

        #endregion

        #region [ ctor ]

        public Form()
        {

        }

        public Form(Tabulation tabulation, FormAttribute tabulationAttributes, int order, int repeat)
        {
            Validate(tabulation, tabulationAttributes, order);
            SetProperties(tabulation, tabulationAttributes, order, repeat);
        }

        public Form(int tabulationId, int tabulationAttributesId, int order, int repeat)
        {
            Validate(tabulationId, tabulationAttributesId, order);
            SetProperties(tabulationId, tabulationAttributesId, order, repeat);
        }

        #endregion

        #region [ methods ]

        private void Validate(int tabulationId, int tabulationAttributesId, int order)
        {
            DomainValidation.When(tabulationId == 0, "Selecione uma tabulação para criar o formulário");
            DomainValidation.When(tabulationAttributesId == 0, "Selecione o atributo que deseja adicionar no formulário");
            DomainValidation.When(order == 0, "Ordem do componente deve ser maior que 0");
        }
        private void Validate(Tabulation tabulation, FormAttribute tabulationAttributes, int order)
        {
            DomainValidation.When(tabulation is null, "Selecione uma tabulação para criar o formulário");
            DomainValidation.When(tabulationAttributes is null, "Selecione o atributo que deseja adicionar no formulário");
            DomainValidation.When(order == 0, "Ordem do componente deve ser maior que 0");
        }

        private void SetProperties(int tabulationId, int tabulationAttributesId, int order, int repeat)
        {
            TabulationId = tabulationId;
            FormAttributeId = tabulationAttributesId;
            Order = order;
            Repeat = repeat;
        }
        private void SetProperties(Tabulation tabulation, FormAttribute tabulationAttributes, int order, int repeat)
        {
            Tabulation = tabulation;
            FormAttribute = tabulationAttributes;
            Order = order;
            Repeat = repeat;
        }

        public void ChangeOrder(int order)
        {
            Order = order;
        }

        #endregion
    }
}