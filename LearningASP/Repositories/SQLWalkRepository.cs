using LearningASP.Data;
using LearningASP.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LearningASP.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly LearningASPDbContext dbContext;

        public SQLWalkRepository(LearningASPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walkers.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string sortBy=null, bool isAscending=true, int pageNumber = 1, int pageSize = 1000)
        {
            var walkers = dbContext.Walkers.Include("Difficulty").Include("Region").AsQueryable();
            
            // filtering
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkers = walkers.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkers = isAscending ? walkers.OrderBy(x => x.Name) : walkers.OrderByDescending(x => x.Name);
                } else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walkers = isAscending ? walkers.OrderBy(x=>x.LengthInKm) : walkers.OrderByDescending(x=>x.LengthInKm);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walkers.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var existingWalk = await dbContext.Walkers.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id==id);
            if (existingWalk == null)
            {
                return null;
            }
            return existingWalk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalker = await dbContext.Walkers.FirstOrDefaultAsync(x=>x.Id==id);

            if(existingWalker == null)
            {
                return null;
            }
            
            existingWalker.Name = walk.Name;
            existingWalker.Description = walk.Description;
            existingWalker.LengthInKm = walk.LengthInKm;
            existingWalker.WalkImageUrl = walk.WalkImageUrl;
            existingWalker.DifficultyId = walk.DifficultyId;
            existingWalker.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return existingWalker;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walkers.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        
            if(existingWalk == null)
            {
                return null;
            }

            dbContext.Walkers.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
