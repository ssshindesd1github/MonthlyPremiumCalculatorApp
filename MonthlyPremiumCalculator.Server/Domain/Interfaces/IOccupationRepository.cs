using MonthlyPremiumCalculatorAPI.Domain.Entities;

namespace MonthlyPremiumCalculatorAPI.Domain.Interfaces
{
    public interface IOccupationRepository
    {
        Task<Occupation?> GetByIdAsync(int id);
        Task<List<Occupation>> GetAllAsync();
    }
}
