using Flexybook.Domain.Responses.Restaurant;

namespace Flexybook___Restaurant_Opening_Hours.Helpers
{
    public static class OpeningHoursDisplayHelper
    {
        public class DisplayRow
        {
            public string Label { get; set; } = "";
            public string? Hours { get; set; }
            public bool IsClosed { get; set; }
        }

        public static List<DisplayRow> GetDisplayRows(List<OpeningHourResponse> openingHours)
        {
            var rows = new List<DisplayRow>();

            // Days of the week in order
            var days = new[]
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
                DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
            };

            // Group Monday-Thursday if all have the same hours and open/closed status
            var mon = openingHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Monday);
            var tue = openingHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Tuesday);
            var wed = openingHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Wednesday);
            var thu = openingHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Thursday);

            bool groupMonThu =
                mon != null && tue != null && wed != null && thu != null &&
                mon.OpenTime == tue.OpenTime && mon.OpenTime == wed.OpenTime && mon.OpenTime == thu.OpenTime &&
                mon.CloseTime == tue.CloseTime && mon.CloseTime == wed.CloseTime && mon.CloseTime == thu.CloseTime &&
                mon.IsClosed == tue.IsClosed && mon.IsClosed == wed.IsClosed && mon.IsClosed == thu.IsClosed;

            if (groupMonThu)
            {
                rows.Add(new DisplayRow
                {
                    Label = "Monday - Thursday",
                    Hours = mon.IsClosed ? "Closed" : $"{mon.OpenTime:hh\\:mm} - {mon.CloseTime:hh\\:mm}",
                    IsClosed = mon.IsClosed
                });
            }
            else
            {
                foreach (var day in new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday })
                {
                    var entry = openingHours.FirstOrDefault(x => x.DayOfWeek == day);
                    rows.Add(new DisplayRow
                    {
                        Label = day.ToString(),
                        Hours = entry == null || entry.IsClosed ? "Closed" : $"{entry.OpenTime:hh\\:mm} - {entry.CloseTime:hh\\:mm}",
                        IsClosed = entry == null || entry.IsClosed
                    });
                }
            }

            // Friday, Saturday, Sunday
            foreach (var day in new[] { DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday })
            {
                var entry = openingHours.FirstOrDefault(x => x.DayOfWeek == day);
                rows.Add(new DisplayRow
                {
                    Label = day.ToString(),
                    Hours = entry == null || entry.IsClosed ? "Closed" : $"{entry.OpenTime:hh\\:mm} - {entry.CloseTime:hh\\:mm}",
                    IsClosed = entry == null || entry.IsClosed
                });
            }

            // Holidays: show all entries with DayOfWeek == null (or a special flag if you add one)
            var holidays = openingHours.Where(x => x.DayOfWeek is null).ToList();
            foreach (var holiday in holidays)
            {
                rows.Add(new DisplayRow
                {
                    Label = "Holidays",
                    Hours = holiday.IsClosed ? "Closed" : $"{holiday.OpenTime:hh\\:mm} - {holiday.CloseTime:hh\\:mm}",
                    IsClosed = holiday.IsClosed
                });
            }

            return rows;
        }
    }
}
