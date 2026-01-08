using Flexybook.Domain.Responses.Restaurant;
using Flexybook___Restaurant_Opening_Hours.Helpers;
using FluentAssertions;
using Xunit;

namespace Flexybook.AplicationService.Tests.Helpers
{
    public class OpeningHoursDisplayHelperTests
    {
        [Fact]
        public void GetDisplayRows_WithIdenticalWeekdayHours_GroupsMonThroughThu()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 9, 17),
                CreateHour(DayOfWeek.Tuesday, 9, 17),
                CreateHour(DayOfWeek.Wednesday, 9, 17),
                CreateHour(DayOfWeek.Thursday, 9, 17),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result.Should().HaveCount(4); // Mon-Thu grouped, Fri, Sat, Sun
            result[0].Label.Should().Be("Monday - Thursday");
            result[0].Hours.Should().Be("09:00 - 17:00");
            result[1].Label.Should().Be("Friday");
            result[2].Label.Should().Be("Saturday");
            result[3].Label.Should().Be("Sunday");
        }

        [Fact]
        public void GetDisplayRows_WithDifferentWeekdayHours_DoesNotGroup()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 9, 17),
                CreateHour(DayOfWeek.Tuesday, 10, 17), // Different open time
                CreateHour(DayOfWeek.Wednesday, 9, 17),
                CreateHour(DayOfWeek.Thursday, 9, 17),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result.Should().HaveCount(7); // All days separate
            result[0].Label.Should().Be("Monday");
            result[1].Label.Should().Be("Tuesday");
            result[2].Label.Should().Be("Wednesday");
            result[3].Label.Should().Be("Thursday");
        }

        [Fact]
        public void GetDisplayRows_WithClosedDay_ShowsClosed()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 9, 17),
                CreateClosedHour(DayOfWeek.Tuesday),
                CreateHour(DayOfWeek.Wednesday, 9, 17),
                CreateHour(DayOfWeek.Thursday, 9, 17),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result.Should().HaveCount(7); // Cannot group because Tuesday is closed
            result[1].Label.Should().Be("Tuesday");
            result[1].Hours.Should().Be("Closed");
            result[1].IsClosed.Should().BeTrue();
        }

        [Fact]
        public void GetDisplayRows_WithHolidayHours_ShowsHolidays()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 9, 17),
                CreateHour(DayOfWeek.Tuesday, 9, 17),
                CreateHour(DayOfWeek.Wednesday, 9, 17),
                CreateHour(DayOfWeek.Thursday, 9, 17),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18),
                CreateHolidayHour(12, 20)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result.Last().Label.Should().Be("Holidays");
            result.Last().Hours.Should().Be("12:00 - 20:00");
        }

        [Fact]
        public void GetDisplayRows_WithMultipleHolidays_ShowsAllHolidays()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 9, 17),
                CreateHour(DayOfWeek.Tuesday, 9, 17),
                CreateHour(DayOfWeek.Wednesday, 9, 17),
                CreateHour(DayOfWeek.Thursday, 9, 17),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18),
                CreateHolidayHour(12, 20),
                CreateClosedHolidayHour()
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            var holidayRows = result.Where(r => r.Label == "Holidays").ToList();
            holidayRows.Should().HaveCount(2);
            holidayRows[0].Hours.Should().Be("12:00 - 20:00");
            holidayRows[1].Hours.Should().Be("Closed");
        }

        [Fact]
        public void GetDisplayRows_WithMissingWeekday_ShowsClosedForMissingDay()
        {
            // Arrange - Missing Tuesday
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 9, 17),
                CreateHour(DayOfWeek.Wednesday, 9, 17),
                CreateHour(DayOfWeek.Thursday, 9, 17),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result.Should().HaveCount(7); // Cannot group because Tuesday is missing
            var tuesday = result.FirstOrDefault(r => r.Label == "Tuesday");
            tuesday.Should().NotBeNull();
            tuesday!.Hours.Should().Be("Closed");
            tuesday.IsClosed.Should().BeTrue();
        }

        [Fact]
        public void GetDisplayRows_WithAllClosedWeekdays_GroupsThemAsClosed()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>
            {
                CreateClosedHour(DayOfWeek.Monday),
                CreateClosedHour(DayOfWeek.Tuesday),
                CreateClosedHour(DayOfWeek.Wednesday),
                CreateClosedHour(DayOfWeek.Thursday),
                CreateHour(DayOfWeek.Friday, 10, 18),
                CreateHour(DayOfWeek.Saturday, 10, 18),
                CreateHour(DayOfWeek.Sunday, 10, 18)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result[0].Label.Should().Be("Monday - Thursday");
            result[0].Hours.Should().Be("Closed");
            result[0].IsClosed.Should().BeTrue();
        }

        [Fact]
        public void GetDisplayRows_WithMidnightCrossover_FormatsCorrectly()
        {
            // Arrange - Late night hours (e.g., 14:00 to 02:00 next day)
            var openingHours = new List<OpeningHourResponse>
            {
                CreateHour(DayOfWeek.Monday, 14, 2),
                CreateHour(DayOfWeek.Tuesday, 14, 2),
                CreateHour(DayOfWeek.Wednesday, 14, 2),
                CreateHour(DayOfWeek.Thursday, 14, 2),
                CreateHour(DayOfWeek.Friday, 14, 2),
                CreateHour(DayOfWeek.Saturday, 14, 2),
                CreateHour(DayOfWeek.Sunday, 14, 2)
            };

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result[0].Label.Should().Be("Monday - Thursday");
            result[0].Hours.Should().Be("14:00 - 02:00");
        }

        [Fact]
        public void GetDisplayRows_WithEmptyList_ReturnsWeekdayAndWeekendRows()
        {
            // Arrange
            var openingHours = new List<OpeningHourResponse>();

            // Act
            var result = OpeningHoursDisplayHelper.GetDisplayRows(openingHours);

            // Assert
            result.Should().NotBeNull();
            // Empty list still creates rows for all weekdays and weekend (showing as closed)
            result.Should().HaveCount(7); // Mon-Thu grouped, Fri, Sat, Sun
            result.Should().AllSatisfy(r => r.IsClosed.Should().BeTrue());
        }

        // Helper methods to create test data
        private static OpeningHourResponse CreateHour(DayOfWeek day, int openHour, int closeHour)
        {
            return new OpeningHourResponse
            {
                DayOfWeek = day,
                OpenTime = new TimeSpan(openHour, 0, 0),
                CloseTime = new TimeSpan(closeHour, 0, 0),
                IsClosed = false
            };
        }

        private static OpeningHourResponse CreateClosedHour(DayOfWeek day)
        {
            return new OpeningHourResponse
            {
                DayOfWeek = day,
                IsClosed = true
            };
        }

        private static OpeningHourResponse CreateHolidayHour(int openHour, int closeHour)
        {
            return new OpeningHourResponse
            {
                DayOfWeek = null,
                OpenTime = new TimeSpan(openHour, 0, 0),
                CloseTime = new TimeSpan(closeHour, 0, 0),
                IsClosed = false
            };
        }

        private static OpeningHourResponse CreateClosedHolidayHour()
        {
            return new OpeningHourResponse
            {
                DayOfWeek = null,
                IsClosed = true
            };
        }
    }
}
