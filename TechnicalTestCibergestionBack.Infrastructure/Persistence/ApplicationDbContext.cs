using Microsoft.EntityFrameworkCore;
using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<SurveyResponse> SurveyResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
