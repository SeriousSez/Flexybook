using ImageEntity = Flexybook.Domain.Entities.Restaurant.Image;
using RestaurantEntity = Flexybook.Domain.Entities.Restaurant.Restaurant;

namespace Flexybook.Domain.Responses.Restaurant
{
    public class ImageResponse : BaseResponse
    {
        public string? Base64Data { get; set; }
        public Guid RestaurantId { get; set; }

        public ImageEntity ToEntity(RestaurantEntity restaurant)
        {
            return new ImageEntity
            {
                Id = this.Id,
                Created = this.Created,
                Base64Image = this.Base64Data,
                RestaurantId = this.RestaurantId,
                Restaurant = restaurant
            };
        }
    }
}
