namespace SiGe
{
    public class ProductCommandText :IProductCommandText
    {
        public string AddProduct => "Usp_Pro_I_Pro_Product";
        public string UpdateProduct => "Usp_Pro_U_Pro_Product";
        public string GetProductById => "Usp_Pro_S_Pro_Product_Get_By_ProductId";
        public string GetAllProduct => "Usp_Pro_S_Pro_Product";
        public string GetProductByCompanyId => "Usp_Pro_S_Pro_Product_Get_By_CompanyId";
        public string GetProductByDescriptionCompanyId => "Usp_Pro_S_Pro_Product_Get_By_Description_CompanyId";
        public string GetProductByCode => "Usp_Pro_S_Pro_Product_Get_By_Code";
    }
}
