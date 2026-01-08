using Flexybook.Domain.Entities.Restaurant;
using Flexybook.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Flexybook.Infrastructure.Tests.Repositories
{
    public class RestaurantRepositoryTests : IDisposable
    {
        private readonly RestaurantContext _context;
        private readonly RestaurantRepository _sut;

        public RestaurantRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantContext(options);
            _sut = new RestaurantRepository(_context);
        }

        [Fact]
        public async Task GetAsync_WhenRestaurantExists_ReturnsRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Test Restaurant",
                Address = "123 Test St",
                City = "Test City",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com"
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetAsync(restaurant.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(restaurant.Id);
            result.Name.Should().Be("Test Restaurant");
        }

        [Fact]
        public async Task GetAsync_WhenRestaurantDoesNotExist_ReturnsNull()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _sut.GetAsync(nonExistentId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetFullAsync_ReturnsRestaurantWithRelatedData()
        {
            // Arrange
            var restaurantId = Guid.NewGuid();
            var restaurant = new Restaurant
            {
                Id = restaurantId,
                Name = "Test Restaurant",
                Address = "123 Test St",
                City = "Test City",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
                Images = new List<Image>(),
                OpeningHours = new List<OpeningHour>()
            };

            restaurant.Images.Add(new Image { Id = Guid.NewGuid(), Base64Image = "test-image-1", RestaurantId = restaurantId, Restaurant = restaurant });
            restaurant.OpeningHours.Add(new OpeningHour
                {
                    Id = Guid.NewGuid(),
                    Type = Flexybook.Domain.OpeningHourType.Restaurant,
                    DayOfWeek = DayOfWeek.Monday,
                    OpenTime = new TimeSpan(9, 0, 0),
                    CloseTime = new TimeSpan(17, 0, 0),
                    RestaurantId = restaurantId,
                    Restaurant = restaurant
            });

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // Act
            var result = await _sut.GetFullAsync(restaurantId);

            // Assert
            result.Should().NotBeNull();
            result!.Images.Should().HaveCount(1);
            result.OpeningHours.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllRestaurants()
        {
            // Arrange
            var restaurant1 = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Restaurant 1",
                Address = "Address 1",
                City = "City 1",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
            };

            var restaurant2 = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Restaurant 2",
                Address = "Address 2",
                City = "City 2",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
            };

            _context.Restaurants.AddRange(restaurant1, restaurant2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(r => r.Name == "Restaurant 1");
            result.Should().Contain(r => r.Name == "Restaurant 2");
        }

        [Fact]
        public async Task GetAllFullAsync_ReturnsAllRestaurantsWithRelatedData()
        {
            // Arrange
            var restaurant1Id = Guid.NewGuid();
            var restaurant1 = new Restaurant
            {
                Id = restaurant1Id,
                Name = "Restaurant 1",
                Address = "Address 1",
                City = "City 1",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
                Images = new List<Image>()
            };

            restaurant1.Images.Add(new Image { Id = Guid.NewGuid(), Base64Image = "image-1", RestaurantId = restaurant1Id, Restaurant = restaurant1 });

            _context.Restaurants.Add(restaurant1);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // Act
            var result = await _sut.GetAllFullAsync();

            // Assert
            result.Should().HaveCount(1);
            result.First().Images.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateAsync_WhenRestaurantExists_UpdatesSuccessfully()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Original Name",
                Address = "Original Address",
                City = "Original City",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            restaurant.Name = "Updated Name";
            restaurant.Address = "Updated Address";

            // Act
            var result = await _sut.UpdateAsync(restaurant);

            // Assert
            result.Should().BeTrue();
            
            var updatedRestaurant = await _context.Restaurants.FindAsync(restaurant.Id);
            updatedRestaurant!.Name.Should().Be("Updated Name");
            updatedRestaurant.Address.Should().Be("Updated Address");
        }

        [Fact]
        public async Task UpdateAsync_DetachesTrackedEntity()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Original Name",
                Address = "Original Address",
                City = "Original City",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            // Simulate tracked entity
            var trackedRestaurant = await _context.Restaurants.FindAsync(restaurant.Id);
            trackedRestaurant!.Name = "Modified Name";

            // Act - should detach the tracked entity
            restaurant.Name = "Updated Name";
            var result = await _sut.UpdateAsync(restaurant);

            // Assert
            result.Should().BeTrue();
            var finalRestaurant = await _context.Restaurants.AsNoTracking().FirstAsync(r => r.Id == restaurant.Id);
            finalRestaurant.Name.Should().Be("Updated Name");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
