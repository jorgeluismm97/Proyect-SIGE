using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class KardexViewModel
    {

        public KardexViewModel()
        {
            Product = new ProductModel();
            Products = new List<ProductModel>();
        }
        public ProductModel Product { get; set; }
        public List<ProductModel> Products { get; set; }
    }

}
