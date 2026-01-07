namespace Flexybook.Domain.Responses.Restaurant
{
    public class OpeningHourResponse : BaseResponse
    {
        public OpeningHourType Type { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public bool IsClosed { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
