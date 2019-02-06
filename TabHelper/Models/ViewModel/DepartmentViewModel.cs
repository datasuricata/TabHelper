using System.Collections.Generic;
using TabHelper.Models.Entities;

namespace TabHelper.Models.ViewModel
{
    public class DepartmentViewModel
    {
        public ICollection<DepartmentModel> Departments;
    }

    public class DepartmentModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static explicit operator DepartmentModel(Department v)
        {
            return v == null ? null : new DepartmentModel
            {
                Id = v.Id,
                CreatedAt = v.CreatedAt.Value.ToString("dd/MM/yyyy HH:mm"),
                Description = v.Description,
                IsDeleted = v.IsDeleted,
                Name = v.Name,
            };
        }
        //public ICollection<User> Users { get; set; } = new List<User>();
    }
}
