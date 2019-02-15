using TabHelper.Helpers;
using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class User : EntityBase
    {
        #region [ propeties ]

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public bool IsBlock { get; private set; }

        public UserAccess UserAccess { get; private set; }

        public int DepartmentId { get; private set; }
        public Department Department { get; private set; }

        #endregion

        #region [ ctor ]

        protected User()
        {

        }

        public User(string name, string email, string password, int departmentId, UserAccess userAccess)
        {
            Validate(name, email, password, departmentId, userAccess);
            SetProperties(name, email, password, departmentId, userAccess);
        }
        public User(string name, string email, string password, Department department, UserAccess userAccess)
        {
            Validate(name, email, password, department, userAccess);
            SetProperties(name, email, password, department, userAccess);
        }

        protected void Validate(string name, string email, string password, int departmentId, UserAccess userAccess)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(email), "E-mail é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(password), "Deve criar uma senha.");
            DomainValidation.When(departmentId == 0, "Selecione um departamento.");
        }
        private void SetProperties(string name, string email, string password, int departmentId, UserAccess userAccess)
        {
            Name = name;
            Email = email;
            Password = password.EncryptPassword();
            DepartmentId = departmentId;
            UserAccess = userAccess;
            IsBlock = false;
        }

        protected void Validate(string name, string email, string password, Department department, UserAccess userAccess)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(email), "E-mail é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(password), "Deve criar uma senha.");
            DomainValidation.When(department is null, "Selecione um departamento.");
        }
        private void SetProperties(string name, string email, string password, Department department, UserAccess userAccess)
        {
            Name = name;
            Email = email;
            Password = password.EncryptPassword();
            Department = department;
            UserAccess = userAccess;
            IsBlock = false;
        }

        #endregion

        #region [ methods ]

        public void Edit(User usr)
        {
            Validate(usr.Name, usr.Email, usr.Password, usr.DepartmentId, usr.UserAccess);

            Name = usr.Name;
            Email = usr.Email;
            UserAccess = usr.UserAccess;
            Password = usr.Password;
            Department = usr.Department;
        }

        public void Block()
        {
            IsBlock = !IsBlock;
        }

        #endregion
    }
}