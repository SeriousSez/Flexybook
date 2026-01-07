using Flexybook.Domain.Responses.Restaurant;

namespace Flexybook.Domain.Entities.Restaurant
{
    public class Image : BaseEntity
    {
        public required string Url { get; set; }
        public Guid RestaurantId { get; set; }
        public required Restaurant Restaurant { get; set; }

        public ImageResponse ToResponse()
        {
            return new ImageResponse
            {
                Id = this.Id,
                Url = this.Url,
                RestaurantId = this.RestaurantId,
                Restaurant = this.Restaurant.ToResponse()
            };
        }
    }
}
