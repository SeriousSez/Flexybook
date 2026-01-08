using Flexybook.Domain.Entities;
using Flexybook.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Flexybook.Infrastructure.Tests.Repositories
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly RestaurantContext _context;
        private readonly UserRepository _sut;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantContext(options);
            _sut = new UserRepository(_context);
        }

        [Fact]
        public async Task GetAsync_WhenUserExists_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new UserEntity
            {
                Id = userId.ToString(),
                UserName = "testuser",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                FavouredRestaurants = new List<Guid> { Guid.NewGuid() }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // Act
            var result = await _sut.GetAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(userId.ToString());
            result.UserName.Should().Be("testuser");
            result.FavouredRestaurants.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAsync_WhenUserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _sut.GetAsync(nonExistentId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_WhenUserExists_UpdatesSuccessfully()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser",
                Email = "test@example.com",
                FirstName = "Original",
                LastName = "Name",
                FavouredRestaurants = new List<Guid>()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var restaurantId = Guid.NewGuid();
            user.FirstName = "Updated";
            user.FavouredRestaurants = new List<Guid> { restaurantId };

            // Act
            var result = await _sut.UpdateAsync(user);

            // Assert
            result.Should().BeTrue();

            var updatedUser = await _context.Users.FindAsync(user.Id);
            updatedUser!.FirstName.Should().Be("Updated");
            updatedUser.FavouredRestaurants.Should().Contain(restaurantId);
        }

        [Fact]
        public async Task UpdateAsync_WhenExceptionOccurs_ReturnsFalse()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = "invalid-id",
                UserName = null!, // This will cause validation error
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com"
            };

            // Act
            var result = await _sut.UpdateAsync(user);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_UpdatesFavouritesList()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var restaurant1 = Guid.NewGuid();
            var restaurant2 = Guid.NewGuid();

            var user = new UserEntity
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                FavouredRestaurants = new List<Guid> { restaurant1 }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // Act - Add second favourite
            user.FavouredRestaurants = new List<Guid> { restaurant1, restaurant2 };
            var result = await _sut.UpdateAsync(user);

            // Assert
            result.Should().BeTrue();

            var updatedUser = await _context.Users.FindAsync(userId);
            updatedUser!.FavouredRestaurants.Should().HaveCount(2);
            updatedUser.FavouredRestaurants.Should().Contain(restaurant1);
            updatedUser.FavouredRestaurants.Should().Contain(restaurant2);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
