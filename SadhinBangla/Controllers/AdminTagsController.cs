using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Models.Domain;
using SadhinBangla.Models.ViewModels;

namespace SadhinBangla.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly SadhinBanglaDbContext sadhinBanglaDbContext;

        public AdminTagsController(SadhinBanglaDbContext sadhinBanglaDbContext)
        {
            this.sadhinBanglaDbContext = sadhinBanglaDbContext;
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

            await sadhinBanglaDbContext.AddAsync(tag);
            await sadhinBanglaDbContext.SaveChangesAsync();
            //var name = addTagRequest.Name;
            //var displayname = addTagRequest.DisplayName;
            return RedirectToAction("TagList");
        }

        //https://localhost:7196/AdminTags/TagList
        [HttpGet]
        [ActionName("TagList")]
        public async Task<IActionResult> TagList()
        {
            var TotalTags = await sadhinBanglaDbContext.tags.ToListAsync();
            return View(TotalTags);
        }


        [HttpGet]
        public async Task<IActionResult> EditTag(Guid id)
        {
            //var tag = sadhinBanglaDbContext.tags.Find(id);
            var tag = await sadhinBanglaDbContext.tags.FirstOrDefaultAsync(t => t.Id == id);
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

            var existingTag = await sadhinBanglaDbContext.tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await sadhinBanglaDbContext.SaveChangesAsync();
                return RedirectToAction("EditTag", new { id = editTagRequest.Id }); 
            }
            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTag(EditTagRequest editTagRequest)
        {
            var tag = await sadhinBanglaDbContext.tags.FirstOrDefaultAsync(t => t.Id == editTagRequest.Id);
            if (tag != null)
            {
                sadhinBanglaDbContext.Remove(tag);
                await sadhinBanglaDbContext.SaveChangesAsync();
                return RedirectToAction("TagList");
            }

            return RedirectToAction("EditTag", new {id = editTagRequest.Id});
        }
    }
}
