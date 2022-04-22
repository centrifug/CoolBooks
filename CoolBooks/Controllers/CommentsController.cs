using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoolBooks.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {

        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;
        private readonly test2 commentlikedislike;


        public CommentsController(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager, test2 likedislike)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.commentlikedislike = likedislike;
        }

        public IActionResult Index()
        {
            return View();

        }

        
        public IActionResult CreateReviewComment(string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReviewComment(CreateCommentViewModel inputComment, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(inputComment);
            }

            Comment commentToSave = new Comment
            {
                Text = inputComment.Text,
                reviewId = id,
                Created = DateTime.Now,
                CreatedBy = userManager.GetUserId(User),
                IsDeleted = false
            };

            _context.Add(commentToSave);
            await _context.SaveChangesAsync();

            int? bookId = _context.Review.Where(r => r.Id == id).Select(r => r.BookId).FirstOrDefault();
            //return Redirect(returnUrl);

            return RedirectToAction("Details", "Books", new {id = bookId});
        }


        public IActionResult Create(string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCommentViewModel inputComment, int id, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(inputComment);
            }

            Comment commentToSave = new Comment
            {
                Text = inputComment.Text,
                commentId = id,
                Created = DateTime.Now,
                CreatedBy = userManager.GetUserId(User),
                IsDeleted = false
            };

            _context.Add(commentToSave);
            await _context.SaveChangesAsync();

            
            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);     
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            //return RedirectToAction("Details", "Books", new { id = bookId });

        }
        public ActionResult Like(int id, bool status)
        {
            //var Db = likedislike(_context, userManager, signInManager); 
            //var Db = _context;
            var user = userManager.GetUserId(User);
            if (user == null)
            {
                return Content("Logga in för att använda like/dislike!");
            }
            var result = commentlikedislike.Like(id, status, user);
            return Content(result);
        }


    }
}
