using LearningASP.Data;
using LearningASP.Models.Domain;

namespace LearningASP.Repositories
{
    public class LocalImageRepository : IIMageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LearningASPDbContext learningASPDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,LearningASPDbContext learningASPDbContext )
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.learningASPDbContext = learningASPDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
              $"{image.FileName}{image.FileExtension}" );

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        
            image.FilePath = urlFilePath;

            await learningASPDbContext.Images.AddAsync(image);
            await learningASPDbContext.SaveChangesAsync();

            return image;
        }
    }
}
