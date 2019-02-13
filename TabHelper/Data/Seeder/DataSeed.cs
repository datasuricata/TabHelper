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
        public static async System.Threading.Tasks.Task InitializeAsync(IServiceProvider service)
        {
            try
            {
                using (var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>()))
                {
                    if (!context.Users.Any())
                    {
                        // # add default forms attributes
                        var att1 = new FormAttribute("TabHelper - Check", ComponentType.Check, "Title Check", null, "display info", "display some infos for component detail", false);
                        var att2 = new FormAttribute("TabHelper - Radio", ComponentType.Radio, "Title Radio", null, "display info", null, true);
                        var att3 = new FormAttribute("TabHelper - Text", ComponentType.Text, "Title Text", null, "display info", "display some infos for component detail", false);
                        var att4 = new FormAttribute("TabHelper - TextBox", ComponentType.TextBox, "Title TextBox", null, "display info", "display some infos for component detail", false);
                        var att5 = new FormAttribute("TabHelper - Custom", ComponentType.Custom, "Title Custom", "10", "display info", "display some infos for component detail", true);

                        context.FormAttributes.AddRange(att1, att2, att3, att4, att5);

                        // # add default form tabulations
                        var tab1 = new Tabulation("TabHelper - Default", "Default form tabulation");
                        var tab2 = new Tabulation("Pesquisa", "Formulário exemplo pesquisa");

                        context.Tabulations.AddRange(tab1);

                        // # add default forms
                        context.Forms.AddRange(
                        new Form(tab1, att1, 1, 0),
                        new Form(tab1, att2, 2, 0),
                        new Form(tab1, att3, 3, 0),
                        new Form(tab1, att4, 4, 0),
                        new Form(tab1, att5, 5, 0),
                        new Form(tab2, att1, 1, 3),
                        new Form(tab2, att2, 2, 1),
                        new Form(tab2, att3, 3, 2),
                        new Form(tab2, att4, 4, 0),
                        new Form(tab2, att5, 5, 0)
                        );

                        // # add default departments
                        var Dept1 = new Department("Root", "Root group system");
                        var Dept2 = new Department("Admin", "Admin group system");
                        var Dept3 = new Department("Pesquisa", "Grupo de vendas");

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

                        // # default tabulations in departments
                        context.DepartmentTabulations.AddRange(new DepartTab(Dept1, tab1), new DepartTab(Dept2, tab1), new DepartTab(Dept3, tab2));
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
        }
    }
}
