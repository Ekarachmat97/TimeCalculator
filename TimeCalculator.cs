using System.Globalization;

namespace TimeCalculator;

public static class TimeCalculator
{
    public static int CalculateDurationInMinutes(string startTimeText, string stopTimeText)
    {
        if (string.IsNullOrWhiteSpace(startTimeText))
        {
            throw new ArgumentException("Please enter a start time.");
        }

        if (string.IsNullOrWhiteSpace(stopTimeText))
        {
            throw new ArgumentException("Please enter a stop time.");
        }

        if (!TryParseTime(startTimeText, out var startTime) || !TryParseTime(stopTimeText, out var stopTime))
        {
            throw new ArgumentException("Please enter a valid time in HH:mm format.");
        }

        var duration = stopTime - startTime;

        if (duration.TotalMinutes < 0)
        {
            duration += TimeSpan.FromDays(1);
        }

        return (int)Math.Round(duration.TotalMinutes, MidpointRounding.AwayFromZero);
    }

    public static string FormatMainResult(int totalMinutes)
    {
        if (totalMinutes == 0)
        {
            return "0 minutes";
        }

        return totalMinutes == 1
            ? "1 minute"
            : $"{totalMinutes} minutes";
    }

    public static string FormatDetailedDuration(int totalMinutes)
    {
        if (totalMinutes == 0)
        {
            return "0 minutes";
        }

        var hours = totalMinutes / 60;
        var minutes = totalMinutes % 60;

        var parts = new List<string>();

        if (hours > 0)
        {
            parts.Add($"{hours} hour{(hours == 1 ? string.Empty : "s")}");
        }

        if (minutes > 0 || parts.Count == 0)
        {
            parts.Add($"{minutes} minute{(minutes == 1 ? string.Empty : "s")}");
        }

        return string.Join(" ", parts);
    }

    public static string NormalizeInputTime(string value)
    {
        var digits = new string((value ?? string.Empty).Where(char.IsDigit).Take(4).ToArray());

        if (digits.Length == 0)
        {
            return string.Empty;
        }

        if (digits.Length == 1)
        {
            return digits;
        }

        if (digits.Length == 2)
        {
            return $"{digits}:";
        }

        if (digits.Length == 3)
        {
            return $"{digits.Substring(0, 2)}:{digits.Substring(2)}";
        }

        return $"{digits.Substring(0, 2)}:{digits.Substring(2, 2)}";
    }

    private static bool TryParseTime(string value, out TimeOnly time)
    {
        var normalizedValue = NormalizeInputTime(value);

        if (string.IsNullOrWhiteSpace(normalizedValue) || normalizedValue.EndsWith(':'))
        {
            time = default;
            return false;
        }

        return TimeOnly.TryParseExact(
            normalizedValue,
            "HH:mm",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out time);
    }
}
