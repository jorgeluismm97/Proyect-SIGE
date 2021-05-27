using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class PersonCompanyService : IPersonCompanyService
    {
        private readonly IPersonCompanyRepository _personCompanyRepository;

        public PersonCompanyService(IPersonCompanyRepository personCompanyRepository)
        {
            _personCompanyRepository = personCompanyRepository;
        }

        //Basic

        public async Task<int> AddAsync(PersonCompanyModel personCompanyModel)
        {
            return await _personCompanyRepository.AddAsync(personCompanyModel);
        }

        public async Task<int> UpdateAsync(PersonCompanyModel personCompanyModel)
        {
            return await _personCompanyRepository.UpdateAsync(personCompanyModel);
        }

        public async Task<PersonCompanyModel> GetByIdAsync(int personCompanyId)
        {
            return await _personCompanyRepository.GetByIdAsync(personCompanyId);
        }

        public async Task<List<PersonCompanyModel>> GetAllAsync()
        {
            return await _personCompanyRepository.GetAllAsync();
        }

    }
}
