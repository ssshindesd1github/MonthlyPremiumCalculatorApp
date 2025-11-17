using MonthlyPremiumCalculatorAPI.DTOs;

namespace MonthlyPremiumCalculatorAPI.Domain.Interfaces
{
    public interface IPremiumService
    {
        Task<CalculatePremiumResponse> CalculateAsync(CalculatePremiumRequest request);
    }
}
