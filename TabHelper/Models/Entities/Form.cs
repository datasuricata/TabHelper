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

        public Form(string name, string code, IEnumerable<FormAttribute> attributes)
        {
            Validate(name, code, attributes);
            SetProperties(name, code, attributes);
        }
        private void Validate(string name, string code, IEnumerable<FormAttribute> attributes)
        {
            Validate(name, code);
            DomainValidation.When(attributes is null, "Crie pelo menos um atributo");
        }
        private void SetProperties(string name, string code, IEnumerable<FormAttribute> attributes)
        {
            SetProperties(name, code);
            Attributes = attributes as ICollection<FormAttribute>;
        }

        #endregion

        #region [ methods ]

        public void Edit(string name, string code)
        {
            Validate(name, code);

            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
