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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPost = await blogPostRepository.GetAllAsync();
            return View(blogPost);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var blogpost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRapository.GetAllAsync();

            if(blogpost != null)
            {
                //mapping the monain into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogpost.Id,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    ShortDescription = blogpost.ShortDescription,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    UrlHandle = blogpost.UrlHandle,
                    PublishDaate = blogpost.PublishDaate,
                    Author = blogpost.Author,
                    Visible = blogpost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem { Text = x.Name, Value=x.Id.ToString() }),
                    SelectedTags = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()
                }; 
                return View(model);
            }

            return View(null);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //Map view model to Domain Model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishDaate = editBlogPostRequest.PublishDaate,
                Author = editBlogPostRequest.Author,
                Visible = editBlogPostRequest.Visible,

            };

            //Map Tags into Domain Model
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out Guid tag))
                {
                    var foundTag = await tagRapository.GetAsync(tag);
                    if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            blogPostDomainModel.Tags = selectedTags;

            //Submit Information to repository to update
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);
            if(updatedBlog != null)
            {
                //Show Success Notification
                return RedirectToAction("Edit");
            }
            //redirect to GET

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            //Talk to repository to delete this blog post and tags
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlogPost != null)
            {
                //Success Response
                return RedirectToAction("List");
            }   
            //Show Error Notificastion
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }

    }
}
