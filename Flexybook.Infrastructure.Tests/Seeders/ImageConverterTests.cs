using Flexybook.Infrastructure.Seeders;
using FluentAssertions;
using Xunit;

namespace Flexybook.Infrastructure.Tests.Seeders
{
    public class ImageConverterTests
    {
        [Fact]
        public void ConvertToBase64_WhenFileDoesNotExist_ReturnsEmptyString()
        {
            // Arrange
            var nonExistentPath = "/images/nonexistent.jpg";

            // Act
            var result = ImageConverter.ConvertToBase64(nonExistentPath);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ConvertToBase64_WhenPathIsNull_ReturnsEmptyString()
        {
            // Arrange
            string nullPath = null!;

            // Act
            var result = ImageConverter.ConvertToBase64(nullPath);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ConvertToBase64_HandlesExceptionsGracefully()
        {
            // Arrange
            var invalidPath = "/invalid\0path/image.jpg"; // Invalid path characters

            // Act
            var result = ImageConverter.ConvertToBase64(invalidPath);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
