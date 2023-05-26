using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Models.Entities
{
    public class SubscriptionPlanLimit
    {
        [MaxLength(10)]
        public int id { get; set; }

        [StringLength(100)]
        public string PlanName { get; set; }

        [MaxLength(10)]
        public int MonthlyScheduledPostLimit { get; set; }
    }
}
