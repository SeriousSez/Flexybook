using Flexybook.Domain.Entities;
using Flexybook.Infrastructure.Seeders;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Flexybook.Infrastructure.Tests.Seeders
{
    public class UserSeederTests
    {
        private readonly Mock<UserManager<UserEntity>> _mockUserManager;

        public UserSeederTests()
        {
            var store = new Mock<IUserStore<UserEntity>>();
            _mockUserManager = new Mock<UserManager<UserEntity>>(
                store.Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<UserEntity>>().Object,
                Array.Empty<IUserValidator<UserEntity>>(),
                Array.Empty<IPasswordValidator<UserEntity>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<UserEntity>>>().Object);
        }

        [Fact]
        public async Task SeedAsync_WhenUserDoesNotExist_CreatesUser()
        {
            // Arrange
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((UserEntity?)null);
            
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await UserSeeder.SeedAsync(_mockUserManager.Object);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(
                It.Is<UserEntity>(u => 
                    u.UserName == "Flexybook" &&
                    u.Email == "flexybook@example.com" &&
                    u.FirstName == "Flexy" &&
                    u.LastName == "Book" &&
                    u.EmailConfirmed == true),
                "Flexybook1234"), 
                Times.Once);
        }

        [Fact]
        public async Task SeedAsync_WhenUserExists_DoesNotCreateUser()
        {
            // Arrange
            var existingUser = new UserEntity
            {
                UserName = "Flexybook",
                Email = "flexybook@example.com",
                FirstName = "Flexy",
                LastName = "Book"
            };

            _mockUserManager.Setup(x => x.FindByNameAsync("Flexybook"))
                .ReturnsAsync(existingUser);

            // Act
            await UserSeeder.SeedAsync(_mockUserManager.Object);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task SeedAsync_WithFavouriteRestaurantId_AddsFavouriteToUser()
        {
            // Arrange
            var favouriteId = Guid.NewGuid();
            
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((UserEntity?)null);
            
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await UserSeeder.SeedAsync(_mockUserManager.Object, favouriteId);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(
                It.Is<UserEntity>(u => 
                    u.FavouredRestaurants != null &&
                    u.FavouredRestaurants.Contains(favouriteId)),
                It.IsAny<string>()), 
                Times.Once);
        }

        [Fact]
        public async Task SeedAsync_WithoutFavouriteRestaurantId_DoesNotAddFavourite()
        {
            // Arrange
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((UserEntity?)null);
            
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await UserSeeder.SeedAsync(_mockUserManager.Object);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(
                It.Is<UserEntity>(u => u.FavouredRestaurants == null),
                It.IsAny<string>()), 
                Times.Once);
        }

        [Fact]
        public async Task SeedAsync_WhenCreateFails_ThrowsException()
        {
            // Arrange
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((UserEntity?)null);
            
            var errors = new[]
            {
                new IdentityError { Description = "Password too weak" },
                new IdentityError { Description = "Invalid email" }
            };
            
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));

            // Act
            Func<Task> act = async () => await UserSeeder.SeedAsync(_mockUserManager.Object);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Failed to create user: Password too weak, Invalid email");
        }

        [Fact]
        public async Task SeedAsync_CreatesUserWithEmailConfirmed()
        {
            // Arrange
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((UserEntity?)null);
            
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await UserSeeder.SeedAsync(_mockUserManager.Object);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(
                It.Is<UserEntity>(u => u.EmailConfirmed == true),
                It.IsAny<string>()), 
                Times.Once);
        }
    }
}
