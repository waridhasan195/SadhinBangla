using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Models;
using SadhinBangla.Models.ViewModels;
using SadhinBangla.Rapositories;
using System.Diagnostics;

namespace SadhinBangla.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRapository tagRapository;

        public HomeController(ILogger<HomeController> logger, 
            IBlogPostRepository blogPostRepository,
            ITagRapository tagRapository
            )
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRapository = tagRapository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            var tags = await tagRapository.GetAllAsync();
            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
