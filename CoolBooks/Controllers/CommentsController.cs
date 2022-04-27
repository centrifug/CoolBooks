using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        

        // GET: Comments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            //låt bara skaparen och admin editera
            if (userManager.GetUserId(User) == comment.CreatedBy || User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                EditCommentViewModel vm = new EditCommentViewModel();

                vm.Id = comment.Id;
                vm.Text = comment.Text;

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCommentViewModel inputComment, string? returnUrl)
        {
            if (id != inputComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var commentToUpdate = _context.Comment.FirstOrDefault(r => r.Id == inputComment.Id);

                commentToUpdate.Text = inputComment.Text;
                commentToUpdate.LastUpdated = DateTime.Now;
                commentToUpdate.UpdatedBy = userManager.GetUserId(User);

                try
                {
                    _context.Update(commentToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(commentToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }

            }

            return View(inputComment);
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
        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
        public ActionResult Report(int id)
        {

            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return NotFound();
            }

            var reportedComment = _context.ReportedComments
                .Where(rr => rr.CommentId == id && rr.UserId == userId)
                .FirstOrDefault();

            if (reportedComment == null)
            {
                reportedComment = new ReportedComment();
                reportedComment.UserId = userId;
                reportedComment.Created = DateTime.Now;
                reportedComment.CommentId = id;
                _context.Add(reportedComment);
                _context.SaveChanges();
                return Content("Rapporterad");
            }
            else
            {
                _context.ReportedComments.Remove(reportedComment);
                _context.SaveChanges();
                return Content("Rapportera");
            }


        }
    }
}
