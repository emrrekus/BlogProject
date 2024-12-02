namespace BlogProject.Container
{
    public static class DateHelper
    {
        public static string GetTimeAgo(DateTime createdDate)
        {
            var timeSpan = DateTime.Now - createdDate;

            if (timeSpan.TotalSeconds < 60)
                return $"{timeSpan.Seconds} saniye önce";

            if (timeSpan.TotalMinutes < 60)
                return $"{timeSpan.Minutes} dakika önce";

            if (timeSpan.TotalHours < 24)
                return $"{timeSpan.Hours} saat önce";

            if (timeSpan.TotalDays < 30)
                return $"{timeSpan.Days} gün önce";

            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} ay önce";

            return $"{(int)(timeSpan.TotalDays / 365)} yıl önce";
        }
    }
}
