using Flexybook.Domain.Responses.Restaurant;

namespace Flexybook.Domain.Entities.Restaurant
{
    public class Restaurant : BaseEntity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public List<Image>? Images { get; set; }
        public List<OpeningHour> OpeningHours { get; set; } = new();

        public RestaurantResponse ToResponse()
        {
            return new RestaurantResponse
            {
                Id = this.Id,
                Created = this.Created,
                Name = this.Name,
                Address = this.Address,
                City = this.City,
                Telephone = this.Telephone,
                Email = this.Email,
                Images = this.Images?.Select(i => i.ToResponse()).ToList(),
                OpeningHours = this.OpeningHours.Select(oh => oh.ToResponse()).ToList()
            };
        }
    }
}
