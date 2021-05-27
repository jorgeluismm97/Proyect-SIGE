using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        //Basic

        public async Task<int> AddAsync(SessionModel sessionModel)
        {
            return await _sessionRepository.AddAsync(sessionModel);
        }

        public async Task<int> UpdateAsync(SessionModel sessionModel)
        {
            return await _sessionRepository.UpdateAsync(sessionModel);
        }

        public async Task<SessionModel> GetByIdAsync(int sessionId)
        {
            return await _sessionRepository.GetByIdAsync(sessionId);
        }

        public async Task<List<SessionModel>> GetAllAsync()
        {
            return await _sessionRepository.GetAllAsync();
        }
    }
}
