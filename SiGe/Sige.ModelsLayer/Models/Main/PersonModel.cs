using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class PersonModel : BaseModel
    {
        public int PersonId { get; set; }
        public int IdentityDocumentTypeId { get; set; }
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
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
    }
}
