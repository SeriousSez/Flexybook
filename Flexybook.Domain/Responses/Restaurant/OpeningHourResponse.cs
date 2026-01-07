using OpeningHourEntity = Flexybook.Domain.Entities.Restaurant.OpeningHour;
using RestaurantEntity = Flexybook.Domain.Entities.Restaurant.Restaurant;

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

        public OpeningHourEntity ToEntity(RestaurantEntity restaurant)
        {
            return new OpeningHourEntity
            {
                Id = this.Id,
                Created = this.Created,
                Type = this.Type,
                DayOfWeek = this.DayOfWeek,
                OpenTime = this.OpenTime,
                CloseTime = this.CloseTime,
                IsClosed = this.IsClosed,
                RestaurantId = this.RestaurantId,
                Restaurant = restaurant
            };
        }
    }
}
