using MonthlyPremiumCalculatorAPI.Domain.Entities;
using MonthlyPremiumCalculatorAPI.Domain.Interfaces;
using MonthlyPremiumCalculatorAPI.DTOs;
using MonthlyPremiumCalculatorAPI.Infrastructure.Data;
using MonthlyPremiumCalculatorAPI.Services;
using System.Globalization;

namespace MonthlyPremiumCalculator.Tests;

public class PremiumCalculatorServiceTests
{
    private readonly PremiumService _service;
    private readonly AppDbContext  _context;


    public PremiumCalculatorServiceTests()
    {
        _context = TestDbContextFactory.Create();
        _service = new PremiumService((IOccupationRepository)_service);
    }



    [Fact]
    public void CalculatePremium_WithValidDoctorInput_ReturnsCorrectPremium()
    {
        var input = new CalculatePremiumRequest
        {
            Name = "John",
            AgeNextBirthday = 30,
            DobMonthYear = "02-1990",
            OccupationId = 1,
            DeathCoverAmount = 100000
        };

        decimal result = Convert.ToDecimal(_service.CalculateAsync(input));

        decimal expected = (100000 * 1.5M * 30) / 1000 * 12;
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CalculatePremium_WithInvalidOccupation_ThrowsException()
    {

        var input = new CalculatePremiumRequest
        {
            Name = "Jane",
            AgeNextBirthday = 40,
            DobMonthYear = "01/1995",
            OccupationId = 2,
            DeathCoverAmount = 50000
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CalculateAsync(input));

    }

    [Fact]
    public async Task CalculatePremium_WithMissingRatingFactor_ThrowsException()
    {
        _context.Occupations.Add(new Occupation { Name = "Ghost", Rating = "Mystery" });
        _context.SaveChanges();

        var input = new CalculatePremiumRequest
        {
            Name = "Ghosty",
            AgeNextBirthday = 25,
            DobMonthYear = "01/1995",
            OccupationId = 2,
            DeathCoverAmount = 75000
        };

        await Assert.ThrowsAsync<Exception>(() => _service.CalculateAsync(input));
    }
}
