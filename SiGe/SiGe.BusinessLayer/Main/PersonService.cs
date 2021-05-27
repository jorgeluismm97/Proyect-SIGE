using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        //Basic

        public async Task<int> AddAsync(PersonModel personModel)
        {
            return await _personRepository.AddAsync(personModel);
        }

        public async Task<int> UpdateAsync(PersonModel personModel)
        {
            return await _personRepository.UpdateAsync(personModel);
        }

        public async Task<PersonModel> GetByIdAsync(int personId)
        {
            return await _personRepository.GetByIdAsync(personId);
        }

        public async Task<List<PersonModel>> GetAllAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        // Advanced
        public async Task<PersonModel> GetByIdentityDocumentTypeIdIdentityDocumentNumberAsync(int identityDocumentTypeId, string identityDocumentNumber)
        {
            return await _personRepository.GetByIdentityDocumentTypeIdIdentityDocumentNumberAsync(identityDocumentTypeId, identityDocumentNumber);
        }
        public async Task<List<PersonModel>> GetByCompanyIdAsync(int companyId)
        {
            return await _personRepository.GetByCompanyIdAsync(companyId);
        }

        public async Task<List<PersonModel>> GetWithOutUserByCompanyIdAsync(int companyId)
        {
            return await _personRepository.GetWithOutUserByCompanyIdAsync(companyId);
        }
    }
}
