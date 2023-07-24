using LearningASP.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {

            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadImageRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            
            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Only jpg, jpeg or png allowed");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB");
            }
        }

    }
}
