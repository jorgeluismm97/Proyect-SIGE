using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class LoginViewModel
    {

        public int IdentityAction { get; set; }
        [StringLength(11)]
        [Display(Name = "Numero de RUC")]
        public string IdentityDocumentNumber { get; set; }
        [Required]
        [MaxLength(8)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(8)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Recordar Contraseña")]
        public bool RememberMe { get; set; }
    }
}
