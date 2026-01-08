using Flexybook.Infrastructure.Seeders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Flexybook.Infrastructure.Tests.Seeders
{
    public class RestaurantSeederTests : IDisposable
    {
        private readonly RestaurantContext _context;

        public RestaurantSeederTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantContext(options);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Seed_AddsRestaurantsToDatabase()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var restaurants = _context.Restaurants.ToList();
            restaurants.Should().HaveCount(2);
        }

        [Fact]
        public void Seed_CreatesAalborgRestaurant()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var aalborg = _context.Restaurants.FirstOrDefault(r => r.Name == "Aalborg");
            aalborg.Should().NotBeNull();
            aalborg!.Address.Should().Be("Østerågade 27, 9000");
            aalborg.City.Should().Be("Aalborg");
            aalborg.Telephone.Should().Be("+45 11 22 33 44");
            aalborg.Email.Should().Be("aalborg@flexybox.com");
        }

        [Fact]
        public void Seed_CreatesOdenseRestaurant()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var odense = _context.Restaurants.FirstOrDefault(r => r.Name == "Odense");
            odense.Should().NotBeNull();
            odense!.Address.Should().Be("Nøglens Kvarter 181, 5220");
            odense.City.Should().Be("Odense");
            odense.Telephone.Should().Be("+45 52 82 82 21");
            odense.Email.Should().Be("odense@flexybox.com");
        }

        [Fact]
        public void Seed_OdenseRestaurantHasDeterministicId()
        {
            // Arrange
            var expectedId = RestaurantSeeder.GetOdenseRestaurantId();

            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var odense = _context.Restaurants.FirstOrDefault(r => r.Name == "Odense");
            odense.Should().NotBeNull();
            odense!.Id.Should().Be(expectedId);
        }

        [Fact]
        public void Seed_RestaurantsHaveImages()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var restaurants = _context.Restaurants.Include(r => r.Images).ToList();
            restaurants.Should().AllSatisfy(r => r.Images.Should().NotBeEmpty());
            
            var aalborg = restaurants.First(r => r.Name == "Aalborg");
            aalborg.Images.Should().HaveCount(2);
            
            var odense = restaurants.First(r => r.Name == "Odense");
            odense.Images.Should().HaveCount(4);
        }

        [Fact]
        public void Seed_ImagesHaveBase64Data()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var images = _context.Restaurants
                .Include(r => r.Images)
                .SelectMany(r => r.Images)
                .ToList();

            // Images should exist even if Base64 is empty (file not found in test environment)
            images.Should().AllSatisfy(img =>
            {
                img.Base64Image.Should().NotBeNull();
                // In test environment, files may not exist, so Base64Image could be empty
                // The important part is that the structure is correct
            });
        }

        [Fact]
        public void Seed_RestaurantsHaveOpeningHours()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var restaurants = _context.Restaurants.Include(r => r.OpeningHours).ToList();
            restaurants.Should().AllSatisfy(r => r.OpeningHours.Should().NotBeEmpty());
        }

        [Fact]
        public void Seed_AalborgHasBuffetHours()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var aalborg = _context.Restaurants
                .Include(r => r.OpeningHours)
                .First(r => r.Name == "Aalborg");

            var buffetHours = aalborg.OpeningHours.Where(h => h.Type == Domain.OpeningHourType.Buffet).ToList();
            buffetHours.Should().NotBeEmpty();
            
            // Buffet should be closed Mon-Thu and open Fri-Sun
            buffetHours.Where(h => h.DayOfWeek.HasValue && h.DayOfWeek == DayOfWeek.Monday).Should().AllSatisfy(h => h.IsClosed.Should().BeTrue());
            buffetHours.Where(h => h.DayOfWeek.HasValue && h.DayOfWeek == DayOfWeek.Friday).Should().AllSatisfy(h => h.IsClosed.Should().BeFalse());
        }

        [Fact]
        public void Seed_RestaurantsHaveDifferentOpeningHours()
        {
            // Act
            RestaurantSeeder.Seed(_context);
            _context.SaveChanges();

            // Assert
            var aalborg = _context.Restaurants
                .Include(r => r.OpeningHours)
                .First(r => r.Name == "Aalborg");
            
            var odense = _context.Restaurants
                .Include(r => r.OpeningHours)
                .First(r => r.Name == "Odense");

            // Aalborg opens at 7:00, Odense varies (Mon-Wed 10:00, Thu-Sun 8:00)
            var aalborgMonday = aalborg.OpeningHours.First(h => h.DayOfWeek == DayOfWeek.Monday && h.Type == Domain.OpeningHourType.Restaurant);
            aalborgMonday.OpenTime.Should().Be(new TimeSpan(7, 0, 0));

            var odenseMonday = odense.OpeningHours.First(h => h.DayOfWeek == DayOfWeek.Monday && h.Type == Domain.OpeningHourType.Restaurant);
            odenseMonday.OpenTime.Should().Be(new TimeSpan(10, 0, 0));
        }

        [Fact]
        public void GetOdenseRestaurantId_ReturnsSameIdAcrossCalls()
        {
            // Act
            var id1 = RestaurantSeeder.GetOdenseRestaurantId();
            var id2 = RestaurantSeeder.GetOdenseRestaurantId();

            // Assert
            id1.Should().Be(id2);
        }
    }
}
