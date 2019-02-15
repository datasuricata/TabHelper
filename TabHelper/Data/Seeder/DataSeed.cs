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
                        // #  add forms
                        var form1 = new Form("Default", "0001");
                        var form2 = new Form("Pesquisa", "0001");

                        context.Forms.AddRange(form1, form2);

                        // # add default forms attributes
                        context.FormAttributes.AddRange(
                        new FormAttribute(form1, "TabHelper - Check", ComponentType.Check, "Title Check", null, "display info", "display some infos for component detail", false, 1, 0),
                        new FormAttribute(form1, "TabHelper - Radio", ComponentType.Radio, "Title Radio", null, "display info", null, true, 2, 0),
                        new FormAttribute(form1, "TabHelper - Text", ComponentType.Text, "Title Text", null, "display info", "display some infos for component detail", false, 3, 0),
                        new FormAttribute(form1, "TabHelper - TextBox", ComponentType.TextBox, "Title TextBox", null, "display info", "display some infos for component detail", false, 4, 0),
                        new FormAttribute(form1, "TabHelper - Custom", ComponentType.Custom, "Title Custom", "10", "display info", "display some infos for component detail", true, 5, 0),
                        new FormAttribute(form2, "TabHelper - Check", ComponentType.Check, "Title Check", null, "display info", "display some infos for component detail", false, 1, 0),
                        new FormAttribute(form2, "TabHelper - Text", ComponentType.Text, "Title Text", null, "display info", "display some infos for component detail", false, 2, 0),
                        new FormAttribute(form2, "TabHelper - Check", ComponentType.Check, "Title Check", null, "display info", "display some infos for component detail", false, 3, 0),
                        new FormAttribute(form2, "TabHelper - Text", ComponentType.Text, "Title Text", null, "display info", "display some infos for component detail", false, 4, 0),
                        new FormAttribute(form2, "TabHelper - Check", ComponentType.Check, "Title Check", null, "display info", "display some infos for component detail", false, 5, 0),
                        new FormAttribute(form2, "TabHelper - Text", ComponentType.Text, "Title Text", null, "display info", "display some infos for component detail", false, 6, 0));

                        // # add default form tabulations
                        var tab1 = new Tabulation("TabHelper - Default", "Default form tabulation");
                        var tab2 = new Tabulation("Pesquisa", "Formulário exemplo pesquisa");

                        context.Tabulations.AddRange(tab1);

                        // # add default forms
                        context.FormTabs.AddRange(new FormTab(tab1, form1),new FormTab(tab2, form2));

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
                        context.DepartTabs.AddRange(new DepartTab(Dept1, tab1), new DepartTab(Dept2, tab1), new DepartTab(Dept3, tab2));
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
