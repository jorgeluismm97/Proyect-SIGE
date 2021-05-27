using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class CompanyModel : BaseModel
    {
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(11)]
        [Display(Name = "Numero de RUC")]
        public string IdentityDocumentNumber { get; set; }
        [Required]
        [Display(Name = "Razon Social")]
        public string BusinessName { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string Address { get; set; }
    }
}
