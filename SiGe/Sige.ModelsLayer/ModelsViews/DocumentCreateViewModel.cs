using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiGe
{
    public class DocumentCreateViewModel
    {
        public DocumentCreateViewModel()
        {
            Details = new List<DocumentCreateViewModelDetail>();
            DocumentTypes = new List<DocumentTypeModel>();
            Series = new List<SerieViewModel>();
            MethodPayments = new List<MethodPaymentModel>();
        }

        public int DocumentId { get; set; }
        public List<DocumentTypeModel> DocumentTypes { get; set; }
        public int DocumentTypeId { get; set; }
        public List<SerieViewModel> Series { get; set; }
        public int DocumentTypeBranchOfficeSerieId { get; set; }
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public CustomerModel Customer { get; set; }
        public List<MethodPaymentModel> MethodPayments { get; set; }
        public int MethodPaymentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        //public decimal Total()
        //{
        //    return Details.Sum(x => x.Price());
        //}
        public decimal Total { get; set; }
        public List<DocumentCreateViewModelDetail> Details { get; set; }

        public List<ProductModel> Products { get; set; }
        public List<CustomerModel> Customers { get; set; }

    }

    public  class DocumentCreateViewModelDetail
    {
        public string Code { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price()
        {
            return Quantity * UnitPrice;
        }

    }

}
