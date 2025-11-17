namespace MonthlyPremiumCalculatorAPI.Domain.Entities
{
    public class Premium
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public double MonthlyPremium { get; set; }
        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
    }
}
