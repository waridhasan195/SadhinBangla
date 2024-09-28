using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Add(AddTagRequest addTagRequest)
        {

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            sadhinBanglaDbContext.Add(tag);
            sadhinBanglaDbContext.SaveChanges();
            //var name = addTagRequest.Name;
            //var displayname = addTagRequest.DisplayName;
            return RedirectToAction("TagList");
        }

        //https://localhost:7196/AdminTags/TagList
        [HttpGet]
        [ActionName("TagList")]
        public IActionResult TagList()
        {
            var TotalTags = sadhinBanglaDbContext.tags.ToList();
            return View(TotalTags);
        }


        [HttpGet]
        public IActionResult EditTag(Guid id)
        {
            //var tag = sadhinBanglaDbContext.tags.Find(id);
            var tag = sadhinBanglaDbContext.tags.FirstOrDefault(t => t.Id == id);
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
        public IActionResult EditTag(EditTagRequest editTagRequest)
        {

            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = sadhinBanglaDbContext.tags.Find(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                sadhinBanglaDbContext.SaveChanges();
                return RedirectToAction("EditTag", new { id = editTagRequest.Id }); 
            }
            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult DeleteTag(EditTagRequest editTagRequest)
        {
            var tag = sadhinBanglaDbContext.tags.FirstOrDefault(t => t.Id == editTagRequest.Id);
            if (tag != null)
            {
                sadhinBanglaDbContext.Remove(tag);
                sadhinBanglaDbContext.SaveChanges();
                return RedirectToAction("TagList");
            }

            return RedirectToAction("EditTag", new {id = editTagRequest.Id});
        }
    }
}
