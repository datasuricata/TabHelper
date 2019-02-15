using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class FormAttribute : EntityBase
    {
        #region [ properties ]

        public string Name { get; private set; }
        public ComponentType ComponentType { get; private set; }
        public bool IsNumeric { get; private set; }

        public int Order { get; private set; }
        public int Repeat { get; private set; }

        public string Title { get; private set; }
        public string Value { get; private set; }

        public string Info { get; private set; }
        public string Detail { get; private set; }

        public int FormId { get; private set; }
        public Form Form { get; private set; }

        #endregion

        #region [ ctor ]

        /// <summary>
        /// Return new attribute for tabulation form
        /// </summary>
        /// <param name="name">control name</param>
        /// <param name="componentType">type component</param>
        /// <param name="title">title for web display</param>
        /// <param name="value">value for custom component</param>
        /// <param name="isnumeric">define as numeric record</param>
        /// <param name="info">info for web span display</param>
        /// <param name="detail">detail for web display</param>
        public FormAttribute(int formId, string name, ComponentType componentType, string title, string value, string info, string detail, bool isnumeric, int order, int repeat)
        {
            Validate(formId, name, title);
            SetProperties(formId, name, componentType, title, value, info, detail, isnumeric, order, repeat);
        }
        public FormAttribute(Form form, string name, ComponentType componentType, string title, string value, string info, string detail, bool isnumeric, int order, int repeat)
        {
            Validate(form, name, title);
            SetProperties(form, name, componentType, title, value, info, detail, isnumeric, order, repeat);
        }

        protected FormAttribute()
        {

        }

        #endregion

        #region [ methods ]

        private void Validate(int formId, string name, string title)
        {
            DomainValidation.When(formId == 0, "É necessário vincular um formulário");
            DomainValidation.When(string.IsNullOrEmpty(name), "Defina um nome para seu atributo");
            DomainValidation.When(string.IsNullOrEmpty(title), "Titulo de exibição é obrigatorio");
        }
        private void SetProperties(int formId, string name, ComponentType componentType, string title, string value, string info, string detail, bool isnumeric, int order, int repeat)
        {
            Name = name;
            ComponentType = componentType;
            Title = title;
            Value = value;
            Info = info;
            Detail = detail;
            IsNumeric = isnumeric;
            Order = order;
            Repeat = repeat;
            FormId = formId;
        }

        private void Validate(Form form, string name, string title)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Defina um nome para seu atributo");
            DomainValidation.When(string.IsNullOrEmpty(title), "Titulo de exibição é obrigatorio");
            DomainValidation.When(form is null, "É necessário vincular um formulário");
        }
        private void SetProperties(Form form, string name, ComponentType componentType, string title, string value, string info, string detail, bool isnumeric, int order, int repeat)
        {
            Name = name;
            ComponentType = componentType;
            Title = title;
            Value = value;
            Info = info;
            Detail = detail;
            IsNumeric = isnumeric;
            Order = order;
            Repeat = repeat;
            Form = form;
        }

        public void ChangeOrder(int order)
        {
            Order = order;
        }

        #endregion
    }
}
