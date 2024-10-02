using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Rapositories;
using System.Net;

namespace SadhinBangla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRapository imageRapository;

        public ImagesController(IImageRapository imageRapository)
        {
            this.imageRapository = imageRapository;
        }

        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await imageRapository.UploadAsunc(file);
            if (imageUrl == null)
            {
                return Problem("Something went wrong! ", null, (int)HttpStatusCode.InternalServerError); 
            }
            return new JsonResult(new { link = imageUrl });
        }
    } 
}
 