using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class BranchOfficeModel : BaseModel
    {
        public int BranchOfficeId { get; set; }
        public int CompanyId { get; set; }
        public int PersonId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Cellular { get; set; }

    }
}
