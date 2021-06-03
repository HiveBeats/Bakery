using System;
using System.Collections.Generic;

namespace Bakery.Core.Entities
{
    public class DiscountTime
    {
        public DiscountTime()
        {
            TimeId = Guid.NewGuid().ToString();
        }
        public string DiscountId { get; set; }
        public string TimeId { get; set; }
        public int DayWeek { get; set; }
        public decimal? StartTime { get; set; }
        public decimal? EndTime { get; set; }

        public virtual CustomerDiscount Discount { get; set; }
    }
}
