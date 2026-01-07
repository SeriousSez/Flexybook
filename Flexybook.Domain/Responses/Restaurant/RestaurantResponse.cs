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
    }
}
