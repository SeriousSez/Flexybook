using Flexybook.Domain.Responses.Restaurant;

namespace Flexybook.Helpers
{
    /// <summary>
    /// Helper class for formatting and grouping opening hours for display.
    /// </summary>
    public static class OpeningHoursDisplayHelper
    {
        private const string HoursFormat = "hh\\:mm";
        private const string ClosedLabel = "Closed";
        private const string MondayThroughThursdayLabel = "Monday - Thursday";
        private const string HolidaysLabel = "Holidays";

        /// <summary>
        /// Represents a single row of opening hours data for display.
        /// </summary>
        public class DisplayRow
        {
            public string Label { get; set; } = "";
            public string? Hours { get; set; }
            public bool IsClosed { get; set; }
        }

        /// <summary>
        /// Converts a list of opening hours into display rows, grouping Monday-Thursday when they share the same hours.
        /// </summary>
        /// <param name="openingHours">The list of opening hours to format.</param>
        /// <returns>A list of display rows ready for rendering.</returns>
        public static List<DisplayRow> GetDisplayRows(List<OpeningHourResponse> openingHours)
        {
            var rows = new List<DisplayRow>();

            AddWeekdayRows(rows, openingHours);
            AddWeekendRows(rows, openingHours);
            AddHolidayRows(rows, openingHours);

            return rows;
        }

        private static void AddWeekdayRows(List<DisplayRow> rows, List<OpeningHourResponse> openingHours)
        {
            var weekdays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };
            var weekdayHours = weekdays.Select(day => openingHours.FirstOrDefault(x => x.DayOfWeek == day)).ToList();

            if (CanGroupWeekdays(weekdayHours))
            {
                rows.Add(CreateGroupedWeekdayRow(weekdayHours[0]!));
            }
            else
            {
                rows.AddRange(weekdays.Select(day => CreateDayRow(day.ToString(), openingHours.FirstOrDefault(x => x.DayOfWeek == day))));
            }
        }

        private static void AddWeekendRows(List<DisplayRow> rows, List<OpeningHourResponse> openingHours)
        {
            var weekendDays = new[] { DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            
            foreach (var day in weekendDays)
            {
                var entry = openingHours.FirstOrDefault(x => x.DayOfWeek == day);
                rows.Add(CreateDayRow(day.ToString(), entry));
            }
        }

        private static void AddHolidayRows(List<DisplayRow> rows, List<OpeningHourResponse> openingHours)
        {
            var holidays = openingHours.Where(x => x.DayOfWeek is null).ToList();
            
            foreach (var holiday in holidays)
            {
                rows.Add(CreateDayRow(HolidaysLabel, holiday));
            }
        }

        private static bool CanGroupWeekdays(List<OpeningHourResponse?> weekdayHours)
        {
            if (weekdayHours.Any(h => h == null))
                return false;

            var first = weekdayHours[0]!;
            
            return weekdayHours.All(h => 
                h!.OpenTime == first.OpenTime &&
                h.CloseTime == first.CloseTime &&
                h.IsClosed == first.IsClosed);
        }

        private static DisplayRow CreateGroupedWeekdayRow(OpeningHourResponse hours)
        {
            return new DisplayRow
            {
                Label = MondayThroughThursdayLabel,
                Hours = FormatHours(hours),
                IsClosed = hours.IsClosed
            };
        }

        private static DisplayRow CreateDayRow(string dayLabel, OpeningHourResponse? hours)
        {
            return new DisplayRow
            {
                Label = dayLabel,
                Hours = FormatHours(hours),
                IsClosed = hours?.IsClosed ?? true
            };
        }

        private static string FormatHours(OpeningHourResponse? hours)
        {
            if (hours == null || hours.IsClosed)
                return ClosedLabel;

            return $"{hours.OpenTime.ToString(HoursFormat)} - {hours.CloseTime.ToString(HoursFormat)}";
        }
    }
}
