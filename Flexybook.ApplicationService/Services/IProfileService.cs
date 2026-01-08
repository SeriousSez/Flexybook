using Flexybook.Domain.Entities;

namespace Flexybook.ApplicationService.Services
{
    public interface IProfileService
    {
        Task<string?> LoginAsync();
    }
}
