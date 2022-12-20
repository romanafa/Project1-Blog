using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oblig2_Blog.Data;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System.Security.Claims;

namespace Oblig2_Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public PostController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
        }
        // GET: PostController
        public ActionResult Index()
        {
            IEnumerable<Post> postList = _unitOfWork.Post.GetAll();
            return View(postList);
        }

        // GET: PostController/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Bad parameter");
            }

            var post = _unitOfWork.Post.GetPostViewModel(id);
            if (post == null || post.PostId != id)
            {
                return NotFound("Ikke funnet");
            }
            var postOwnerUserName = _userManager.Users.Where(u => u.Id == post.OwnerId).Select(u => u.UserName).FirstOrDefault();
            post.Username = postOwnerUserName;
            
            var tagsInPost = _db.Posts.Where(p => p.PostId == id).SelectMany(t => t.Tags).ToList();

            post.Tags = tagsInPost;

           var comments = _unitOfWork.Post.GetAllComments(id);
            foreach (var c in comments)
            {
                var commentOwnerUserName = (from u in _userManager.Users
                    where u.Id.Equals(c.OwnerId)
                    select u.UserName).SingleOrDefault();
                c.PostTitle = post.PostTitle;
                c.Username = commentOwnerUserName;
            }
            post.Comments = comments;

            if (comments == null)
            {
                return View(post);
            }
            
            return View(post);
        }

        // GET: PostController/Create
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        public ActionResult Create(int? blogId)
        {
            var model = new PostViewModel();
            if (blogId == null)
            {
                NotFound("BlogId is null");
            }
            //bind blogId from url
            model.BlogId = blogId;
            return View(model);
        }

        // POST: PostController/Create
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? blogId, [Bind("PostId, PostTitle, PostText")] PostViewModel post)
        {
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Post.Save(post, User, blogId);
                    TempData["success"] = "Post created successfully";
                    return RedirectToAction("Details", "Blog", new { id = blogId });
                }
                else throw new Exception();
            }

            catch
            {
                return View(post);
            }
        }

        // GET: PostController/Edit/5
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        public ActionResult Edit(int? id)
        {
            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var post = _unitOfWork.Post.GetFirstOrDefault(x => x.PostId == id);

            if (post== null ||  post.PostId != id)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: PostController/Edit/5
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int postId, int blogId, [Bind("PostId, PostTitle, PostText, BlogId")] Post post)
        {
            if (postId != post.PostId)
            {
                return NotFound();
            }

            ModelState.Clear();
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                if (ModelState.IsValid)
                {
                    post.User = currentUser;
                    post.Created = DateTime.Now;
                    _unitOfWork.Post.Update(post);
                    _unitOfWork.Save();
                    TempData["message"] = $"{post.PostTitle} has been updated";
                    return RedirectToAction("Details", "Blog", new { id = blogId });
                }
                else throw new Exception();
            }
            catch
            {
                return View(post);
            }
        }

        // GET: PostController/Delete/5
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _unitOfWork.Post.GetFirstOrDefault(x => x.PostId == id);

            if (post == null || post.PostId != id)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: PostController/Delete/5
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int blogId)
        {
            var post = _unitOfWork.Post.GetFirstOrDefault(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.Post.Remove(post);
                _unitOfWork.Save();
                TempData["success"] = "Post deleted successfully";
                return RedirectToAction("Details", "Blog", new { id = blogId });
            }
            catch
            {
                return View();
            }
        }
    }
}
