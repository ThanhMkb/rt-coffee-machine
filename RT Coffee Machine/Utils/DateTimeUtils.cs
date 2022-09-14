namespace RT_Coffee_Machine.Utils
{
    public static class DateTimeUtils
    {
        public static bool IsMonthDay(this DateTime datetime, int month, int day)
        {
            return datetime.Month == month && datetime.Day == day;
        }
    }
}
