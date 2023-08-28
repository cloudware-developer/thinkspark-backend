namespace ThinkSpark.Shared.Extensions.Common
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Obtem o primeiro dia do mês para uma determinada data.
        /// </summary>
        /// <param name="value">Data</param>
        public static DateTime ToFirstDayOfMonth(this DateTime value)
        {
            var result = new DateTime(value.Year, value.Month, 1);
            return result;
        }
    }
}
