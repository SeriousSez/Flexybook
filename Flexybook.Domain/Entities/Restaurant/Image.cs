using Flexybook.Domain.Responses.Restaurant;

namespace Flexybook.Domain.Entities.Restaurant
{
    public class Image : BaseEntity
    {
        public string? Base64Image { get; set; }
        public Guid RestaurantId { get; set; }
        public required Restaurant Restaurant { get; set; }

        public ImageResponse ToResponse()
        {
            return new ImageResponse
            {
                Id = this.Id,
                Created = this.Created,
                Base64Data = this.Base64Image,
                RestaurantId = this.RestaurantId,
            };
        }
    }
}
