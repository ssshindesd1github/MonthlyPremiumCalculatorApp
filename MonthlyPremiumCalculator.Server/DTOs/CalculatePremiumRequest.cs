namespace MonthlyPremiumCalculatorAPI.DTOs
{
    public class CalculatePremiumRequest
    {
        public string Name { get; set; } = default!;
        public int AgeNextBirthday { get; set; }
        public string DobMonthYear { get; set; } = default!; // "MM/YYYY"
        public int OccupationId { get; set; }
        public double DeathCoverAmount { get; set; }
    }
}
