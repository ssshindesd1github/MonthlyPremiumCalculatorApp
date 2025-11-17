
using MonthlyPremiumCalculatorAPI.Domain.Interfaces;
using MonthlyPremiumCalculatorAPI.DTOs;

namespace MonthlyPremiumCalculatorAPI.Services
{
    public class PremiumService : IPremiumService
    {
        private readonly IOccupationRepository _occupations;
        public PremiumService(IOccupationRepository occupations) => _occupations = occupations;

        public async Task<CalculatePremiumResponse> CalculateAsync(CalculatePremiumRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required");
            if (request.AgeNextBirthday <= 0)
                throw new ArgumentException("Age Next Birthday must be positive");
            if (request.DeathCoverAmount <= 0)
                throw new ArgumentException("Death cover amount must be positive");

            var occ = await _occupations.GetByIdAsync(request.OccupationId)
                      ?? throw new ArgumentException("Invalid occupation");

            // Parse MM/YYYY to DateOnly (validate)
            var parts = request.DobMonthYear.Split('/');
            if (parts.Length != 2 || !int.TryParse(parts[0], out var mm) || !int.TryParse(parts[1], out var yyyy))
                throw new ArgumentException("Date of Birth must be in MM/YYYY format");
            var dob = new DateOnly(yyyy, mm, 1);

            // Formula: (Death Cover * Factor * Age) / 1000 * 12
            var monthly = (request.DeathCoverAmount * occ.Factor * request.AgeNextBirthday) / 1000d * 12d;

            return new CalculatePremiumResponse
            {
                MonthlyPremium = Math.Round(monthly, 2),
                Occupation = occ.Name,
                Rating = occ.Rating,
                Factor = occ.Factor
            };
        }
    }
}