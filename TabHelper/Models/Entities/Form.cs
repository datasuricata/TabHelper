using System.Collections.Generic;
using System.Collections.ObjectModel;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Form : EntityBase
    {
        #region [ properties ]

        public string Name { get; private set; }
        public string Code { get; private set; }

        public ICollection<FormTab> FormTabs { get; private set; } = new Collection<FormTab>();
        public ICollection<FormAttribute> Attributes { get; private set; } = new Collection<FormAttribute>();

        #endregion

        #region [ ctor ]

        protected Form()
        {

        }
        public Form(string name, string code)
        {
            Validate(name, code);
            SetProperties(name, code);
        }

        #endregion

        #region [ methods ]

        private void Validate(string name, string code)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatório");
            DomainValidation.When(string.IsNullOrEmpty(code), "Identificador é obrigatório");
        }
        private void SetProperties(string name, string code)
        {
            Name = name;
            Code = code;
        }

        #endregion
    }
}
