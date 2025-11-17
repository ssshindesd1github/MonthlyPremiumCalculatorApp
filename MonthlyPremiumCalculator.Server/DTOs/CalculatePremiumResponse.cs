namespace MonthlyPremiumCalculatorAPI.DTOs
{
    public class CalculatePremiumResponse
    {
        public double MonthlyPremium { get; set; }
        public string Occupation { get; set; } = default!;
        public string Rating { get; set; } = default!;
        public double Factor { get; set; }
    }
}
