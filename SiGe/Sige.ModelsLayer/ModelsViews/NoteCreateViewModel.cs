using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class NoteCreateViewModel
    {
        public NoteCreateViewModel()
        {
            NoteTypes = new List<NoteTypeModel>();
            Details = new List<NoteCreateViewModelDetail>();
            Products = new List<ProductModel>();
        }

        public int NoteId { get; set; }
        public List<NoteTypeModel> NoteTypes { get; set; }
        public int NoteTypeId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public List<NoteCreateViewModelDetail> Details { get; set; }
        public List<ProductModel> Products { get; set; }

        public class NoteCreateViewModelDetail
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
}
