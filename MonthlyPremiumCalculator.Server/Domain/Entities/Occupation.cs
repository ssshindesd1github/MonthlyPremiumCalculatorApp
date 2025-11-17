namespace MonthlyPremiumCalculatorAPI.Domain.Entities
{
    public class Occupation
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Rating { get; set; } = default!;
        public double Factor { get; set; }
    }
}
