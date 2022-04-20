using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoolBooks.Controllers
{
    public class CommentsController : Controller
    {

        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;


        public CommentsController(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateReviewComment()
        {           
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReviewComment(CreateCommentViewModel inputComment, string? returnUrl, int id)
        {
            if (!ModelState.IsValid)
            {
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

            }

            return View(inputComment);
        }


        public IActionResult Create()
        {
            return View();
        }





    }
}
