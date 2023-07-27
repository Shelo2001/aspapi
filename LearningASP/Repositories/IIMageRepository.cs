using LearningASP.Models.Domain;

namespace LearningASP.Repositories
{
    public interface IIMageRepository
    {
        Task<Image> Upload(Image image);
    }
}
