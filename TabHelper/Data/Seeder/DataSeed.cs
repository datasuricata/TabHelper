using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TabHelper.Data.ORM;
using TabHelper.Helpers;
using TabHelper.Models;
using TabHelper.Models.Entities;

namespace TabHelper.Data.Seeder
{
    public static class DataSeed
    {
        public static void Initialize(IServiceProvider service)
        {
            try
            {
                using (var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>()))
                {
                    if (!context.Users.Any())
                    {
                        // # add default departments
                        var Dept1 = new Department("Root", "Root group system");
                        var Dept2 = new Department("Admin", "Admin group system");
                        var Dept3 = new Department("Sellers", "Sales group system");

                        // # add default users
                        context.Users.AddRange(
                        new User(
                            "Lucas Rocha de Moraes",
                            "#sm@datasuricata",
                            Utils.EncryptPassword("m0ra3s"),
                            Dept1,
                            UserAccess.SuperAdministrador),
                        new User(
                            "Robot Mr. Admin",
                            "sm@datasuricata",
                            Utils.EncryptPassword("123456"),
                            Dept2,
                            UserAccess.Administrador),
                        new User(
                            "Robot Mr. Manager",
                            "admin@datasuricata",
                            Utils.EncryptPassword("123456"),
                            Dept3,
                            UserAccess.Gerente),
                         new User(
                            "Robot Mr. Captain",
                            "super@datasuricata",
                            Utils.EncryptPassword("123456"),
                            Dept3,
                            UserAccess.Supervisor),
                          new User(
                            "Robot Mr. Operator",
                            "operator@datasuricata",
                            Utils.EncryptPassword("123456"),
                            Dept3,
                            UserAccess.Operador)
                            );
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
        }
    }
}
