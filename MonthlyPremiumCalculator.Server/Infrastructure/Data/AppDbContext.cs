namespace MonthlyPremiumCalculatorAPI.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using MonthlyPremiumCalculatorAPI.Domain.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Member> Members => Set<Member>();
        public DbSet<Occupation> Occupations => Set<Occupation>();
        public DbSet<Premium> Premiums => Set<Premium>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occupation>().HasData(
                new Occupation { Id = 1, Name = "Cleaner", Rating = "Light Manual", Factor = 11.50 },
                new Occupation { Id = 2, Name = "Doctor", Rating = "Professional", Factor = 1.50 },
                new Occupation { Id = 3, Name = "Author", Rating = "White Collar", Factor = 2.25 },
                new Occupation { Id = 4, Name = "Farmer", Rating = "Heavy Manual", Factor = 31.75 },
                new Occupation { Id = 5, Name = "Mechanic", Rating = "Heavy Manual", Factor = 31.75 },
                new Occupation { Id = 6, Name = "Florist", Rating = "Light Manual", Factor = 11.50 },
                new Occupation { Id = 7, Name = "Other", Rating = "Heavy Manual", Factor = 31.75 }
            );
        }
    }

}
