using Flexybook.Domain;
using Flexybook.Domain.Entities.Restaurant;

namespace Flexybook.Infrastructure
{
    public static class DbSeeder
    {
        public static void Seed(RestaurantContext db)
        {
            // Aalborg
            var restaurantAalborgId = Guid.NewGuid();
            var restaurantAalborg = new Restaurant
            {
                Id = restaurantAalborgId,
                Name = "Aalborg",
                Address = "Østergade 27, 9000",
                City = "Aalborg",
                Telephone = "+45 11 22 33 44",
                Email = "aalborg@flexybox.com",
                IsFavourite = false
            };
            var imagesAalborg = new List<Image>
            {
                new Image { Id = Guid.NewGuid(), Url = "/images/restaurant-interior.jpg", RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new Image { Id = Guid.NewGuid(), Url = "/images/lee-campbell.jpg", RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg }
            };
            var openingHoursAalborg = new List<OpeningHour>
            {
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Tuesday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Wednesday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Thursday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(7,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },

                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Tuesday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Wednesday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Thursday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },

                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Monday, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Tuesday, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Wednesday,  IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour {Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Thursday, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg},
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(16,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(16,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(16,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Buffet, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },

                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Tuesday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Wednesday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Thursday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg }
            };
            restaurantAalborg.Images = imagesAalborg;
            restaurantAalborg.OpeningHours = openingHoursAalborg;

            // Odense
            var restaurantOdenseId = Guid.NewGuid();
            var restaurantOdense = new Restaurant
            {
                Id = restaurantOdenseId,
                Name = "Odense",
                Address = "Nøglens Kvarter 181, 5220",
                City = "Odense",
                Telephone = "+45 52 82 82 21",
                Email = "odense@flexybox.com",
                IsFavourite = true
            };
            var imagesOdense = new List<Image>
            {
                new Image { Id = Guid.NewGuid(), Url = "/images/food.jpeg", RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new Image { Id = Guid.NewGuid(), Url = "/images/food2.jpeg", RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new Image { Id = Guid.NewGuid(), Url = "/images/food3.jpeg", RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new Image { Id = Guid.NewGuid(), Url = "/images/fancy-interior.jpg", RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense }
            };
            var openingHoursOdense = new List<OpeningHour>
            {
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(10,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Tuesday, OpenTime = new TimeSpan(10,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Wednesday, OpenTime = new TimeSpan(10,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Thursday, OpenTime = new TimeSpan(8,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(8,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(8,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(8,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Restaurant, IsClosed = true, RestaurantId = restaurantOdenseId, Restaurant = restaurantOdense },

                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Tuesday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Wednesday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Thursday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(12,0,0), CloseTime = new TimeSpan(22,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.Takeaway, IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },

                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Tuesday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Wednesday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Thursday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(00,0,0), IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Friday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Saturday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, DayOfWeek = DayOfWeek.Sunday, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = false, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg },
                new OpeningHour { Id = Guid.NewGuid(), Type = OpeningHourType.SpecialEventForGroups, OpenTime = new TimeSpan(14,0,0), CloseTime = new TimeSpan(02,0,0), IsClosed = true, RestaurantId = restaurantAalborgId, Restaurant = restaurantAalborg }
            };
            restaurantOdense.Images = imagesOdense;
            restaurantOdense.OpeningHours = openingHoursOdense;

            db.Restaurants.Add(restaurantAalborg);
            db.Restaurants.Add(restaurantOdense);
            db.SaveChanges();
        }
    }
}
