using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class KardexDetailModel
    {
        public KardexDetailModel()
        {
            Product = new ProductModel();
            BalanceIn = 0;
            BalanceOut = 0;
            Details = new List<DetailViewModel>();
        }
        public ProductModel Product { get; set; }
        public int BalanceIn { get; set; }
        public List<DetailViewModel> Details { get; set; }
        public int BalanceOut { get; set; }
    }

    public class DetailViewModel
    {
        public int NoteId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
        public int Balance { get; set; }
    }

    public class ResulViewModel
    {
        public int ActionTYpe { get; set; }
        public DateTime IssueDate { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
