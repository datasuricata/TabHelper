using System.Collections.Generic;
using TabHelper.Models.Entities;

namespace TabHelper.Models.ViewModel
{
    public class UserViewModel
    {
        public List<User> Users { get; set; } = new List<User>();
    }

    public class UserManagerModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserAccess UserAccess { get; set; }
    }

    public class UserAccessModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
    }
}
