using Flexybook.ApplicationService.JwtFeatures;
using Flexybook.ApplicationService.Services;
using Flexybook.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Flexybook.AplicationService.Tests.Services
{
    public class ProfileServiceTests
    {
        private readonly Mock<UserManager<UserEntity>> _mockUserManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ProfileService _sut;

        public ProfileServiceTests()
        {
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
                new Mock<ILogger<UserManager<UserEntity>>>().Object
            );

            // Create a fake IConfiguration for JwtHandler
            var inMemorySettings = new Dictionary<string, string>
            {
                {"JwtSettings:securityKey", "TestSecretKey12345678901234567890"},
                {"JwtSettings:validIssuer", "TestIssuer"},
                {"JwtSettings:validAudience", "TestAudience"},
                {"JwtSettings:expiryInMinutes", "60"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            _jwtHandler = new JwtHandler(configuration, _mockUserManager.Object);

            _sut = new ProfileService(_mockUserManager.Object, _jwtHandler);
        }

        [Fact]
        public async Task LoginAsync_WhenUserExistsAndEmailConfirmed_ReturnsToken()
        {
            // Arrange
            var userEntity = new UserEntity
            {
                Id = "user-123",
                UserName = "Flexybook",
                Email = "flexybook@example.com",
                FirstName = "Flexy",
                LastName = "Book",
                EmailConfirmed = true
            };

            _mockUserManager
                .Setup(x => x.FindByNameAsync("Flexybook"))
                .ReturnsAsync(userEntity);

            _mockUserManager
                .Setup(x => x.IsEmailConfirmedAsync(userEntity))
                .ReturnsAsync(true);

            _mockUserManager
                .Setup(x => x.GetRolesAsync(userEntity))
                .ReturnsAsync(new List<string>());

            // Act
            var result = await _sut.LoginAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task LoginAsync_WhenUserDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockUserManager
                .Setup(x => x.FindByNameAsync("Flexybook"))
                .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _sut.LoginAsync();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_WhenEmailNotConfirmed_ReturnsNull()
        {
            // Arrange
            var userEntity = new UserEntity
            {
                Id = "user-123",
                UserName = "Flexybook",
                Email = "flexybook@example.com",
                FirstName = "Flexy",
                LastName = "Book",
                EmailConfirmed = false
            };

            _mockUserManager
                .Setup(x => x.FindByNameAsync("Flexybook"))
                .ReturnsAsync(userEntity);

            _mockUserManager
                .Setup(x => x.IsEmailConfirmedAsync(userEntity))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.LoginAsync();

            // Assert
            result.Should().BeNull();
        }
    }
}
