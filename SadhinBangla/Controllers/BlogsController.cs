using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Rapositories;

namespace SadhinBangla.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogLink =  await blogPostRepository.GetUrlHandleAsync(urlHandle);
            return View(blogLink);
        }
    }
}
