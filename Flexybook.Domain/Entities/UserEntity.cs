using Flexybook.Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Flexybook.Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Guid>? FavouredRestaurants { get; set; }

        public UserResponse ToResponse()
        {
            return new UserResponse
            {
                UserName = this.UserName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email
            };
        }
    }
}
