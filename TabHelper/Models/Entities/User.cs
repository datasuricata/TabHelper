using System.Reflection.PortableExecutable;
using TabHelper.Helpers;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class User : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Department Department { get; private set; }// = new Department();
        public UserAccess UserAccess { get; private set; }

        protected User()
        {
            
        }

        public User(string name, string email, string password, Department department, UserAccess userAccess)
        {
            ValidateProperties(name, email, password, department, userAccess);
            SetProperties(name, email, password, department, userAccess);
        }

        private void ValidateProperties(string name, string email, string password, Department department, UserAccess userAccess)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(email), "E-mail é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(password), "Deve criar uma senha.");
            DomainValidation.When(department == null, "Selecione um departamento.");
            DomainValidation.When(userAccess.GetType() == typeof(UserAccess), "Tipo de acesso inválido.");
        }

        private void SetProperties(string name, string email, string password, Department department, UserAccess userAccess)
        {
            Name = name;
            Email = email;
            Password = password.EncryptPassword();
            Department = department;
            UserAccess = userAccess;
        }
    }
}