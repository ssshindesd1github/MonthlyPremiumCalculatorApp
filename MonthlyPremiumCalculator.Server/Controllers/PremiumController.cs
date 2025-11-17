//namespace MonthlyPremiumCalculatorAPI.Controllers
// Controllers/PremiumController.cs
using Microsoft.AspNetCore.Mvc;
using MonthlyPremiumCalculatorAPI.Domain.Interfaces;
using MonthlyPremiumCalculatorAPI.DTOs;

namespace MonthlyPremiumCalculatorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PremiumController : ControllerBase
{
    private readonly IPremiumService _premiumService;
    private readonly IOccupationRepository _occupationRepo;

    public PremiumController(IPremiumService premiumService, IOccupationRepository occupationRepo)
    {
        _premiumService = premiumService;
        _occupationRepo = occupationRepo;
    }

    // GET: api/premium/occupations
    [HttpGet("occupations")]
    public async Task<IActionResult> GetOccupations()
    {
        var list = await _occupationRepo.GetAllAsync();
        return Ok(list.Select(o => new { o.Id, o.Name, o.Rating, o.Factor }));
    }

    // POST: api/premium/calculate
    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] CalculatePremiumRequest request)
    {
        var result = await _premiumService.CalculateAsync(request);
        return Ok(result);
    }
}

