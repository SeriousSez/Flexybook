namespace Flexybook.Domain.Responses.Restaurant
{
    public class ImageResponse : BaseResponse
    {
        public required string Url { get; set; }
        public Guid RestaurantId { get; set; }
        public required RestaurantResponse Restaurant { get; set; }
    }
}
