namespace SiGe
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            NumberOfCompanies = 0;
            NumberOfCompaniesPayment = 0;
        }
        public int NumberOfCompanies { get; set; }
        public int NumberOfCompaniesPayment { get; set; }
        public int NumberOfCustomer { get; set; }
        public int NumberOfIn { get; set; }
        public int NumberOfOut { get; set; }
        public int NumberOfSales { get; set; }
    }
}
