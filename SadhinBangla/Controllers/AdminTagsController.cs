using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Models.Domain;
using SadhinBangla.Models.ViewModels;
using SadhinBangla.Rapositories;

namespace SadhinBangla.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRapository tagRapository;

        public AdminTagsController(ITagRapository tagRapository)
        {
            this.tagRapository = tagRapository;
        }


        //https://localhost:7196/AdminTags/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddRagRequest to Tag domain Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            var addTag = await tagRapository.AddAsync(tag);
            if (addTag != null)
            {
                //Add Notification
            }
            else
            {
                //Add Notification
            }
            //var name = addTagRequest.Name;
            //var displayname = addTagRequest.DisplayName;
            return RedirectToAction("TagList");
        }

        //https://localhost:7196/AdminTags/TagList
        [HttpGet]
        [ActionName("TagList")]
        public async Task<IActionResult> TagList()
        {
            var TotalTags = await tagRapository.GetAllAsync();
            return View(TotalTags);
        }


        [HttpGet]
        public async Task<IActionResult> EditTag(Guid id)
        {
            //var tag = sadhinBanglaDbContext.tags.Find(id);
            var tag = await tagRapository.GetAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            else
            {
                return View(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTag(EditTagRequest editTagRequest)
        {

            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updateTag = await tagRapository.UpdateAsync(tag);
            if (updateTag != null)
            {
                //Update Success Notification
            }
            else
            {
                //Update Error Notification
            }

            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTag(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRapository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //Delete Success Notification
                return RedirectToAction("TagList");
            }
            //Delete Error Notification
            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }
    }
}
