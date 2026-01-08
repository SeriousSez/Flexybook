using Flexybook.Domain.Entities;

namespace Flexybook.Domain.Responses
{
    public class UserResponse
    {
        public string? UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Guid>? FavouredRestaurants { get; set; }

        public UserEntity ToEntity()
        {
            return new UserEntity
            {
                UserName = this.UserName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email!,
                Created = this.Created,
                FavouredRestaurants = this.FavouredRestaurants
            };
        }
    }
}
