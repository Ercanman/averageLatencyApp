namespace AverageLatencyApplication.Models.Dtos
{
    public class DateDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public DateDto(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
