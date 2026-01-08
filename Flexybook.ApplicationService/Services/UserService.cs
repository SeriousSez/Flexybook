using Flexybook.Domain.Responses;
using Flexybook.Infrastructure.Repositories;

namespace Flexybook.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return user?.ToResponse();
        }
    }
}
