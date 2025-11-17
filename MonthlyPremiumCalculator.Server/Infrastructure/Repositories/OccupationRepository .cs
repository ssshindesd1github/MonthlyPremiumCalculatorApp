using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculatorAPI.Domain.Entities;
using MonthlyPremiumCalculatorAPI.Domain.Interfaces;
using MonthlyPremiumCalculatorAPI.Infrastructure.Data;

namespace MonthlyPremiumCalculatorAPI.Infrastructure.Repositories
{
   public class OccupationRepository : IOccupationRepository
    {
        private readonly AppDbContext _db;
        public OccupationRepository(AppDbContext db) => _db = db;

        public Task<Occupation?> GetByIdAsync(int id) =>
            _db.Occupations.FirstOrDefaultAsync(o => o.Id == id);

        public Task<List<Occupation>> GetAllAsync() => _db.Occupations.ToListAsync();
    }
}
