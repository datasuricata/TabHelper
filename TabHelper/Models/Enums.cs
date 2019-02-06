using System.ComponentModel.DataAnnotations;

namespace TabHelper.Models
{
    public enum UserAccess
    {
        [Display(Description = "Operador")]
        Operador = 1,

        [Display(Description = "Supervisor")]
        Supervisor = 2,

        [Display(Description = "Gerente")]
        Gerente = 3,

        [Display(Description = "Administrador")]
        Administrador = 4,

        [Display(Description = "Super Administrador")]
        SuperAdministrador = 99,
    }

    public enum ComponentType
    {
        [Display(Description = "Texto")]
        Text = 0,
        
        [Display(Description = "Lógico")]
        Radio = 1,

        [Display(Description = "Check")]
        Check = 2,

        [Display(Description = "Caixa de Texto")]
        TextBox = 4,

        [Display(Description = "Customizados")]
        Custom = 5,        
    }
}