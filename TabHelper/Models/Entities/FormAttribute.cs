using System;
using System.Collections.Generic;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class FormAttribute : EntityBase
    {
        #region [ properties ]

        public string Name { get; private set; }
        public ComponentType ComponentType { get; private set; }
        public bool IsNumeric { get; set; }

        public string Title { get; private set; }
        public string Value { get; private set; }

        public string Info { get; private set; }
        public string Detail { get; private set; }

        public ICollection<Form> Forms { get; private set; } = new List<Form>();

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
        public FormAttribute(string name, ComponentType componentType, string title, string value, string info, string detail, bool isnumeric)
        {
            Validate(name, title);
            SetProperties(name, componentType, title, value, info, detail, isnumeric);
        }

        protected FormAttribute()
        {

        }

        #endregion

        #region [ methods ]

        private void Validate(string name, string title)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Defina um nome para seu atributo");
            DomainValidation.When(string.IsNullOrEmpty(title), "Titulo de exibição é obrigatorio");
        }

        private void SetProperties(string name, ComponentType componentType, string title, string value, string info, string detail, bool isnumeric)
        {
            Name = name;
            ComponentType = componentType;
            Title = title;
            Value = value;
            Info = info;
            Detail = detail;
            IsNumeric = isnumeric;
        }

        #endregion
    }
}
