using System;
using System.Collections.Generic;
using TabHelper.Models.Entities;

namespace TabHelper.Models.ViewModel
{
    public class UserViewModel
    {
        public List<UserModel> Users { get; set; } = new List<UserModel>();
    }

    public class UserModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlock { get; set; }
        public UserAccess UserAccess { get; set; }

        public static explicit operator UserModel(User v)
        {
            return v == null ? null : new UserModel
            {
                Id = v.Id,
                Name = v.Name,
                Email = v.Email,
                IsBlock = v.IsBlock,
                Password = v.Password,
                IsDeleted = v.IsDeleted,
                UserAccess = v.UserAccess,
                CreatedAt = v.CreatedAt?.ToString("dd/MM/yyyy HH:mm"),
                DepartmentId = v.Department is null ? 0 : v.Department.Id,
            };
        }
    }

    public class UserLightModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }

        public static explicit operator UserLightModel(User v)
        {
            return v == null ? null : new UserLightModel
            {
                Id = v.Id,
                Name = v.Name,
                Display = v.IsBlock ? "desbloquear" : "bloquear",
            };
        }
    }
}
