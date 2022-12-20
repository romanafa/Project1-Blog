using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2_Blog.Configurations;
using Oblig2_Blog.Data;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;

namespace Oblig2_Blog.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public TagController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        // GET: TagController
        [Authorize(Roles = StaticDetail.RoleAdmin)]
        public ActionResult Index()
        {
            IEnumerable<Tag> tagList = _unitOfWork.Tag.GetAll();
            return View(tagList);
        }

        // GET: TagController/AllTags
        public ActionResult AllTags()
        {
            IEnumerable<Tag> tagList = _unitOfWork.Tag.GetAll();
            ViewBag.Tags = tagList;

            return View();
        }

        // GET: TagController/PostsByTags
        [HttpGet]
        public ActionResult PostsByTags(int? id)
        {
            var postsByTag = _db.Tags.Where(t => t.TagId == id).SelectMany(p => p.Posts).ToList();
            return PartialView("~/Views/Post/_ViewPostsByTag.cshtml", postsByTag);
        }

        // GET: TagController/Create
        [Authorize(Roles = StaticDetail.RoleAdmin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StaticDetail.RoleAdmin)]
        public async Task<IActionResult> Create([Bind("TagId, TagName")] TagViewModel tag)
        {
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Tag.Save(tag);
                    TempData["success"] = "Tag created successfully";
                    return RedirectToAction("Index", "Tag");
                }

                else throw new Exception();
            }
            catch
            {
                return View(tag);
            }
        }

        // GET: TagController/Delete/5
        [Authorize(Roles = StaticDetail.RoleAdmin)]
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tag = _unitOfWork.Tag.GetFirstOrDefault(x => x.TagId == id);
            
            if (tag == null || tag.TagId == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StaticDetail.RoleAdmin)]
        public ActionResult DeleteConfirmed(int id)
        {
            var tag = _unitOfWork.Tag.GetFirstOrDefault(x => x.TagId == id);
            if (tag == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.Tag.Remove(tag);
                _unitOfWork.Save();
                TempData["success"] = "Tag deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
