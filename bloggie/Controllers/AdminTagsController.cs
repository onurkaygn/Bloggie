using bloggie.Data;
using bloggie.Models.Domain;
using bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext _bloggieDbContext;
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

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
                DisplayName = addTagRequest.DisplayName,
            };


            _bloggieDbContext.Tags.Add(tag);
            _bloggieDbContext.SaveChangesAsync();
            return View("Add");
        }


    }
}
