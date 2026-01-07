using Flexybook.Domain.Responses.Restaurant;

namespace Flexybook.Domain.Entities.Restaurant
{
    public class OpeningHour : BaseEntity
    {
        public OpeningHourType Type { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public bool IsClosed { get; set; }
        public Guid RestaurantId { get; set; }
        public required Restaurant Restaurant { get; set; }

        public OpeningHourResponse ToResponse()
        {
            return new OpeningHourResponse
            {
                Id = this.Id,
                Type = this.Type,
                DayOfWeek = this.DayOfWeek,
                OpenTime = this.OpenTime,
                CloseTime = this.CloseTime,
                IsClosed = this.IsClosed,
                RestaurantId = this.RestaurantId,
                Restaurant = this.Restaurant.ToResponse()
            };
        }
    }
}
