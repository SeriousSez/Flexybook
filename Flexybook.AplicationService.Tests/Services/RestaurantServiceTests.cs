using Flexybook.ApplicationService.Services;
using Flexybook.Domain.Responses.Restaurant;
using Flexybook.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using Xunit;
using Restaurant = Flexybook.Domain.Entities.Restaurant.Restaurant;

namespace Flexybook.AplicationService.Tests.Services
{
    public class RestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly RestaurantService _sut;

        public RestaurantServiceTests()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _sut = new RestaurantService(_mockRestaurantRepository.Object);
        }

        [Fact]
        public async Task GetAsync_WhenRestaurantExists_ReturnsRestaurantResponse()
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
                Images = new List<Flexybook.Domain.Entities.Restaurant.Image>(),
                OpeningHours = new List<Flexybook.Domain.Entities.Restaurant.OpeningHour>()
            };

            _mockRestaurantRepository
                .Setup(x => x.GetFullAsync(restaurantId))
                .ReturnsAsync(restaurant);

            // Act
            var result = await _sut.GetAsync(restaurantId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(restaurantId);
            result.Name.Should().Be("Test Restaurant");
            result.Address.Should().Be("123 Test St");
        }

        [Fact]
        public async Task GetAsync_WhenRestaurantDoesNotExist_ReturnsNull()
        {
            // Arrange
            var restaurantId = Guid.NewGuid();
            _mockRestaurantRepository
                .Setup(x => x.GetFullAsync(restaurantId))
                .ReturnsAsync((Restaurant?)null);

            // Act
            var result = await _sut.GetAsync(restaurantId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllRestaurants()
        {
            // Arrange
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = Guid.NewGuid(),
                    Name = "Restaurant 1",
                    Address = "Address 1",
                    City = "City 1",
                    Telephone = "+45 12345678",
                    Email = "test@restaurant.com",
                    Images = new List<Flexybook.Domain.Entities.Restaurant.Image>(),
                    OpeningHours = new List<Flexybook.Domain.Entities.Restaurant.OpeningHour>()
                },
                new Restaurant
                {
                    Id = Guid.NewGuid(),
                    Name = "Restaurant 2",
                    Address = "Address 2",
                    City = "City 2",
                    Telephone = "+45 12345678",
                    Email = "test@restaurant.com",
                    Images = new List<Flexybook.Domain.Entities.Restaurant.Image>(),
                    OpeningHours = new List<Flexybook.Domain.Entities.Restaurant.OpeningHour>()
                }
            };

            _mockRestaurantRepository
                .Setup(x => x.GetAllFullAsync())
                .ReturnsAsync(restaurants);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(r => r.Name == "Restaurant 1");
            result.Should().Contain(r => r.Name == "Restaurant 2");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoRestaurants_ReturnsEmptyCollection()
        {
            // Arrange
            _mockRestaurantRepository
                .Setup(x => x.GetAllFullAsync())
                .ReturnsAsync(new List<Restaurant>());

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateAsync_WhenUpdateSucceeds_ReturnsTrue()
        {
            // Arrange
            var restaurantResponse = new RestaurantResponse
            {
                Id = Guid.NewGuid(),
                Name = "Updated Restaurant",
                Address = "Updated Address",
                City = "Updated City",
                Telephone = "+45 12345678",
                Email = "updated@restaurant.com",
                Images = new List<ImageResponse>(),
                OpeningHours = new List<OpeningHourResponse>()
            };

            _mockRestaurantRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Restaurant>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateAsync(restaurantResponse);

            // Assert
            result.Should().BeTrue();
            _mockRestaurantRepository.Verify(
                x => x.UpdateAsync(It.Is<Restaurant>(r => r.Name == "Updated Restaurant")),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenUpdateFails_ReturnsFalse()
        {
            // Arrange
            var restaurantResponse = new RestaurantResponse
            {
                Id = Guid.NewGuid(),
                Name = "Updated Restaurant",
                Address = "Updated Address",
                City = "Updated City",
                Telephone = "+45 12345678",
                Email = "test@restaurant.com",
                Images = new List<ImageResponse>(),
                OpeningHours = new List<OpeningHourResponse>()
            };

            _mockRestaurantRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Restaurant>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.UpdateAsync(restaurantResponse);

            // Assert
            result.Should().BeFalse();
        }
    }
}
