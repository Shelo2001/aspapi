using System.ComponentModel.DataAnnotations;

namespace LearningASP.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage = "Code has to be min 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be max 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be max 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
