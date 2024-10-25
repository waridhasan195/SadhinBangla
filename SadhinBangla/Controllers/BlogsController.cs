using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Models.ViewModels;
using SadhinBangla.Rapositories;

namespace SadhinBangla.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(IBlogPostRepository blogPostRepository, 
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked  = false;
            var blogPost =  await blogPostRepository.GetUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();


            if (blogPost != null)
            {
                if (signInManager.IsSignedIn(User))
                {
                    //Get like for this blog for this user
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlogForUser(blogPost.Id);

                    var userId = userManager.GetUserId(User);
                    if(userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }

                }

                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);
                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishDaate = blogPost.PublishDaate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked
                };
            }
            return View(blogDetailsViewModel);
        }
    }
}
