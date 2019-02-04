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
        public Department Department { get; private set; }
        public UserAccess UserAccess { get; private set; }

        public User()
        {

        }

        public User(string name, string email, string password, Department department, UserAccess userAccess)
        {
            Validate(name, email, password, department, userAccess);
            SetProperties(name, email, password, department, userAccess);
        }

        protected void Validate(string name, string email, string password, Department department, UserAccess userAccess)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(email), "E-mail é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(password), "Deve criar uma senha.");
            DomainValidation.When(department is null, "Selecione um departamento.");
        }

        protected void SetProperties(string name, string email, string password, Department department, UserAccess userAccess)
        {
            Name = name;
            Email = email;
            Password = password.EncryptPassword();
            Department = department;
            UserAccess = userAccess;
        }

        public void Edit(User form)
        {
            Validate(form.Name, form.Email, form.Password, form.Department, form.UserAccess);

            Name = form.Name;
            Email = form.Email;
            UserAccess = form.UserAccess;
            Password = form.Password;
            Department = form.Department;
        }
    }
}