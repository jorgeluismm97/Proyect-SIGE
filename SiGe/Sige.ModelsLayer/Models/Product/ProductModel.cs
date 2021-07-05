using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class ProductModel : BaseModel
    {
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Código del Producto")]
        public string Code { get; set; }
        [Display(Name = "Descripción del Producto")]
        public string Description { get; set; }
        public string Summary { get; set; }
    }
}
