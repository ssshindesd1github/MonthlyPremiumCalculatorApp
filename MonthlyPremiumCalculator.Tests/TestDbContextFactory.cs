using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculatorAPI.Domain.Entities;
using MonthlyPremiumCalculatorAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonthlyPremiumCalculator.Tests
{
    public static class TestDbContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Occupations.AddRange(
                new Occupation { Name = "Doctor", Rating = "Professional" },
                new Occupation { Name = "Farmer", Rating = "Heavy Manual" }
            );

            context.Occupations.AddRange(
                new Occupation { Rating = "Professional", Factor = 1.5 },
                new Occupation { Rating = "Heavy Manual", Factor = 31.75 }
            );

            context.SaveChanges();
            return context;
        }
    }
}
