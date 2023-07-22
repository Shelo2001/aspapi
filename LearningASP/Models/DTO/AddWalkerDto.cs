using LearningASP.Models.Domain;

namespace LearningASP.Models.DTO
{
    public class AddWalkerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

    }
}
