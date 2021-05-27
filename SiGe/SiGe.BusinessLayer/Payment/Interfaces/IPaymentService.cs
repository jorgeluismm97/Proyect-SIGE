using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IPaymentService : IService<PaymentModel>
    {
        Task<List<PaymentModel>> GetByCompanyIdAsync(int companyId);
        Task<List<PaymentModel>> GetByCompanyIdDateAsync(int companyId, DateTime date);
    }
}
