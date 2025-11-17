namespace MonthlyPremiumCalculatorAPI.Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateOnly Dob { get; set; }
        public int AgeNextBirthday { get; set; }
        public int OccupationId { get; set; }
        public Occupation? Occupation { get; set; }
        public double DeathCoverAmount { get; set; }
    }
}
