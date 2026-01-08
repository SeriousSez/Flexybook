using Flexybook.ApplicationService.Services;
using Flexybook.Domain.Entities;
using Flexybook.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Flexybook.AplicationService.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<UserManager<UserEntity>> _mockUserManager;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            
            var store = new Mock<IUserStore<UserEntity>>();
            _mockUserManager = new Mock<UserManager<UserEntity>>(
                store.Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<UserEntity>>().Object,
                new IUserValidator<UserEntity>[0],
                new IPasswordValidator<UserEntity>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<UserEntity>>>().Object);

            _sut = new UserService(_mockUserRepository.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task GetUserAsync_WhenUserExists_ReturnsUserResponse()
        {
            // Arrange
            var userId = "user-123";
            var userEntity = new UserEntity
            {
                Id = userId,
                UserName = "testuser",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                FavouredRestaurants = new List<Guid>()
            };

            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(userEntity);

            // Act
            var result = await _sut.GetUserAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result!.UserName.Should().Be("testuser");
            result.Email.Should().Be("test@example.com");
        }

        [Fact]
        public async Task GetUserAsync_WhenUserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userId = "nonexistent-user";
            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _sut.GetUserAsync(userId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateUserFavouritesAsync_WhenAddingFavourite_AddsToList()
        {
            // Arrange
            var userId = "user-123";
            var restaurantId = Guid.NewGuid();
            var userEntity = new UserEntity
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                FavouredRestaurants = new List<Guid>()
            };

            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(userEntity);

            _mockUserRepository
                .Setup(x => x.UpdateAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateUserFavouritesAsync(userId, restaurantId, true);

            // Assert
            result.Should().BeTrue();
            userEntity.FavouredRestaurants.Should().Contain(restaurantId);
            _mockUserRepository.Verify(x => x.UpdateAsync(userEntity), Times.Once);
        }

        [Fact]
        public async Task UpdateUserFavouritesAsync_WhenRemovingFavourite_RemovesFromList()
        {
            // Arrange
            var userId = "user-123";
            var restaurantId = Guid.NewGuid();
            var userEntity = new UserEntity
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                FavouredRestaurants = new List<Guid> { restaurantId }
            };

            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(userEntity);

            _mockUserRepository
                .Setup(x => x.UpdateAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateUserFavouritesAsync(userId, restaurantId, false);

            // Assert
            result.Should().BeTrue();
            userEntity.FavouredRestaurants.Should().NotContain(restaurantId);
            _mockUserRepository.Verify(x => x.UpdateAsync(userEntity), Times.Once);
        }

        [Fact]
        public async Task UpdateUserFavouritesAsync_WhenAddingDuplicateFavourite_DoesNotAddDuplicate()
        {
            // Arrange
            var userId = "user-123";
            var restaurantId = Guid.NewGuid();
            var userEntity = new UserEntity
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                FavouredRestaurants = new List<Guid> { restaurantId }
            };

            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(userEntity);

            _mockUserRepository
                .Setup(x => x.UpdateAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateUserFavouritesAsync(userId, restaurantId, true);

            // Assert
            result.Should().BeTrue();
            userEntity.FavouredRestaurants.Should().ContainSingle();
            userEntity.FavouredRestaurants.Should().Contain(restaurantId);
        }

        [Fact]
        public async Task UpdateUserFavouritesAsync_WhenUserNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = "nonexistent-user";
            var restaurantId = Guid.NewGuid();

            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _sut.UpdateUserFavouritesAsync(userId, restaurantId, true);

            // Assert
            result.Should().BeFalse();
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<UserEntity>()), Times.Never);
        }

        [Fact]
        public async Task UpdateUserFavouritesAsync_WhenRepositoryUpdateFails_ReturnsFalse()
        {
            // Arrange
            var userId = "user-123";
            var restaurantId = Guid.NewGuid();
            var userEntity = new UserEntity
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                FavouredRestaurants = new List<Guid>()
            };

            _mockUserManager
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(userEntity);

            _mockUserRepository
                .Setup(x => x.UpdateAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.UpdateUserFavouritesAsync(userId, restaurantId, true);

            // Assert
            result.Should().BeFalse();
        }
    }
}
