using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IPaymentRepository : IRepository<PaymentModel>
    {
        Task<List<PaymentModel>> GetByCompanyIdAsync(int companyId);
        Task<List<PaymentModel>> GetByCompanyIdDateAsync(int companyId, DateTime date);
    }
}
