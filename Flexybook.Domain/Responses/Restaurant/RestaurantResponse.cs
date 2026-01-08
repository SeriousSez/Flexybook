using RestaurantEntity = Flexybook.Domain.Entities.Restaurant.Restaurant;

namespace Flexybook.Domain.Responses.Restaurant
{
    public class RestaurantResponse : BaseResponse
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public List<ImageResponse>? Images { get; set; }
        public List<OpeningHourResponse> OpeningHours { get; set; } = new();

        public RestaurantEntity ToEntity()
        {

            var restaurant = new RestaurantEntity
            {
                Id = this.Id,
                Created = this.Created,
                Name = this.Name,
                Address = this.Address,
                City = this.City,
                Telephone = this.Telephone,
                Email = this.Email,
            };
            restaurant.Images = this.Images?.Select(i => i.ToEntity(restaurant)).ToList();
            restaurant.OpeningHours = this.OpeningHours.Select(i => i.ToEntity(restaurant)).ToList();

            return restaurant;
        }
    }
}
