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

    }
}
