namespace SiGe
{
    public interface IProductCommandText
    {
        string AddProduct { get; }
        string UpdateProduct { get; }
        string GetProductById { get; }
        string GetAllProduct { get; }
        string GetProductByCompanyId { get; }
        string GetProductQuantityByCompanyId { get; }
        string GetProductByDescriptionCompanyId { get; }
        string GetProductByCode { get; }
    }
}
