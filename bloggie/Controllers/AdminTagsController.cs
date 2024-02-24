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
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
           var tags = _bloggieDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            var tag = _bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag is not null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

          
            var existingTag = _bloggieDbContext.Tags.Find(tag.Id);
            if (existingTag is not null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                _bloggieDbContext.SaveChanges();

                return RedirectToAction("List");
            }

            else
            {
                return RedirectToAction("List", new {id = editTagRequest.Id});
            }
        }   
    }
}
