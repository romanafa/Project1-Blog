using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Oblig2_Blog.Configurations;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System.Security.Principal;
using static Oblig2_Blog.Configurations.Helper;

namespace Oblig2_Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: CommentController/Create
        [Authorize]
        [NoDirectAccess]
        public ActionResult Create(int? postId)
        {
            var model = new CommentViewModel();
            var post = _unitOfWork.Post.GetFirstOrDefault(x => x.PostId == postId);
            //model.PostTitle = post.PostTitle;
            ViewBag.PostTitle = post.PostTitle;

            //var comment = new Comment();
            //comment.PostTitle = post.PostTitle;

            //bind postId from url
            model.PostId = postId;
            return View();
        }

        // POST: CommentController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? postId, [Bind("CommentId, CommentText")] CommentViewModel comment)
        {
            ModelState.Clear();

            if (postId == null)
            {
                return NotFound("Bad parameter");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Comment.Save(comment, User, postId);
                    TempData["success"] = "Comment created successfully";
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllComments", _unitOfWork.Comment.GetCommentPerPost(postId)) });
                }

                else throw new Exception();
            }
            catch
            {
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", comment) });
            }
        }

        // GET: CommentController/Edit/5
        [Authorize]
        [NoDirectAccess]
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var comment = _unitOfWork.Comment.GetFirstOrDefault(x => x.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int commentId, int postId, [Bind("CommentId, CommentText, PostId")] Comment comment)
        {
            ModelState.Clear();
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (commentId != comment.CommentId)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    comment.User = currentUser;
                    comment.Created = DateTime.Now;
                    _unitOfWork.Comment.Update(comment);
                    _unitOfWork.Save();
                    TempData["success"] = "Comment updated successfully";
                    return RedirectToAction("Details", "Post", new {id = postId});
                }
                else throw new Exception();
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var comment = _unitOfWork.Comment.GetFirstOrDefault(x => x.CommentId == id);

            if (comment == null || comment.CommentId != id)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: CommentController/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int postId)
        {
            var comment = _unitOfWork.Comment.GetFirstOrDefault(x => x.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.Comment.Remove(comment);
                _unitOfWork.Save();
                TempData["success"] = "Comment deleted successfully";
                return RedirectToAction("Details", "Post", new {id = postId});
            }
            catch
            {
                return View();
            }
        }
    }
}
