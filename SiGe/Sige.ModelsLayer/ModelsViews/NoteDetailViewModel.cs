using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class NoteDetailViewModel
    {
        public int NoteId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Note { get; set; }
        public string NoteType { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public List<NoteProductModelDetail> Details { get; set; }

    }
    public class NoteProductModelDetail
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
    }
}
