using System.ComponentModel.DataAnnotations;


namespace SiGe
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            UserId = 0;
        }
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Tipo de Documento de Identidad")]
        public int IdentityDocumentTypeId { get; set; }
        [Required]
        [MaxLength(8)]
        [Display(Name = "Numero de Documento de Identidad")]
        public string IdentityDocumentNumber { get; set; }
        [Required]
        [Display(Name = "Apellido Paterno")]
        public string FatherLastName { get; set; }
        [Required]
        [Display(Name = "Apellido Materno")]
        public string MotherLastName { get; set; }
        [Required]
        [Display(Name = "Nombres")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "El password no coincide")]
        public string ConfirmPassword { get; set; }
    }
}
