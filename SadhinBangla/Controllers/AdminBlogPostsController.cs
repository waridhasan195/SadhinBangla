using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Models.ViewModels;
using SadhinBangla.Rapositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using SadhinBangla.Models.Domain;


namespace SadhinBangla.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRapository tagRapository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRapository tagRapository, IBlogPostRepository blogPostRepository)
        {
            this.tagRapository = tagRapository;
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Add()
        {
            var tags =  await tagRapository.GetAllAsync();

            var model = new AddBlogPostReqiest
            {
                //LINQ Operatin to gate tags list from database
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostReqiest addBlogPostReqiest)
        {
            //Map view model to domain model
            var blogpost = new BlogPost
            {
                Heading = addBlogPostReqiest.Heading,
                PageTitle = addBlogPostReqiest.PageTitle,
                Content = addBlogPostReqiest.Content,
                ShortDescription = addBlogPostReqiest.ShortDescription,
                FeaturedImageUrl = addBlogPostReqiest.FeaturedImageUrl,
                UrlHandle = addBlogPostReqiest.UrlHandle,
                PublishDaate = addBlogPostReqiest.PublishDaate,
                Author = addBlogPostReqiest.Author,
                Visible = addBlogPostReqiest.Visible,
            };

            //Map Tags from selected Tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostReqiest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRapository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogpost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogpost);
            return RedirectToAction("Add");
        }


        public async Task<IActionResult> List()
        {
            var blogPost = await blogPostRepository.GetAllAsync();
            return View(blogPost);
        }
    }
}
