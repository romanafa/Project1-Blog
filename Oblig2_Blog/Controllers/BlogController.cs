using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Oblig2_Blog.Data;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System.Security.Claims;

namespace Oblig2_Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: BlogController
        public ActionResult Index()
        {
            IEnumerable<Blog> blogList = _unitOfWork.Blog.GetAll();
            var blogs = new List<BlogViewModel>();

            foreach (var blog in blogList)
            {
                var blogOwnerUserName = (from u in _userManager.Users
                    where u.Id.Equals(blog.OwnerId)
                    select u.UserName).SingleOrDefault();

                var blogViewModel = new BlogViewModel
                {
                    BlogId = blog.BlogId,
                    BlogTitle = blog.BlogTitle,
                    CanPost = blog.CanPost,
                    Created = blog.Created,
                    Description = blog.Description,
                    OwnerId = blog.OwnerId,
                    Username = blogOwnerUserName
                };

                blogs.Add(blogViewModel);
            }

            return View(blogs);
        }

        [HttpGet]
        public ActionResult GeAllBlogsByUser()
        {
            IEnumerable<Blog> blogList;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            blogList = _unitOfWork.Blog.GetAll(u => u.OwnerId == claim.Value, includeProperties: "User");
            return View(blogList);
        }

        // GET: BlogController/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Bad parameter");
            }

            var blog = _unitOfWork.Blog.GetBlogViewModel(id);
            var ownerUserName = (from u in _userManager.Users
                where u.Id.Equals(blog.OwnerId)
                select u.UserName).SingleOrDefault();
            blog.Username = ownerUserName;
            
            var posts = _unitOfWork.Blog.GetAllPosts(blog.BlogId);
            blog.Posts = posts;

            if (posts == null)
            {
                return View(blog);
            }
            if (blog == null || blog.BlogId != id)
            {
                return NotFound("Ikke funnet");
            }

            return View(blog);
        }

        // GET: BlogController/Create
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [Authorize(Roles = StaticDetail.RoleAdmin + "," + StaticDetail.RoleBlogger)]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("BlogId, BlogTitle, CanPost, Description")] BlogViewModel blog)
        {
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                await _unitOfWork.Blog.Save(blog, User);
                TempData["success"] = "Blog created successfully";
                return RedirectToAction("Index");
            }
            var blogViewModel = _unitOfWork.Blog.GetBlogViewModel(null);
            return View(blog);
        }

    }
}
