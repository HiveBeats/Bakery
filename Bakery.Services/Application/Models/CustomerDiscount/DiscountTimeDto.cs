namespace Bakery.Services.Application.Models.CustomerDiscount
{
    public class DiscountTimeDto
    {
        public long TimeId { get; set; }
        public int DayWeek { get; set; }
        public decimal? StartTime { get; set; }
        public decimal? EndTime { get; set; }
    }
}