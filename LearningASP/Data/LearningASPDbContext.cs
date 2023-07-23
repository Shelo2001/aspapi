using LearningASP.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LearningASP.Data
{
    public class LearningASPDbContext : DbContext
    {
        public LearningASPDbContext(DbContextOptions<LearningASPDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walkers { get; set; }

    }
}