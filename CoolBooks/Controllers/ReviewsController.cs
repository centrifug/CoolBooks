#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Claims;

namespace CoolBooks.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;
        private readonly test likedislike;
        private readonly test2 commentlikedislike;


        public ReviewsController(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager, test likedislike, test2 commentlikedislike)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.likedislike = likedislike;
            this.commentlikedislike = commentlikedislike;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.searchString = searchString;

            ViewBag.BookAscDescSortParam = sortOrder == "Book ASC" ? "Book DESC" : "Book ASC";
            ViewBag.TitleAscDescSortParam = sortOrder == "Title ASC" ? "Title DESC" : "Title ASC";
            ViewBag.TextAscDescSortParam = sortOrder == "Text ASC" ? "Text DESC" : "Text ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";
            ViewBag.CreatedAscDescSortParam = sortOrder == "Created ASC" ? "Created DESC" : "Created ASC";
            ViewBag.LikesAscDescSortParam = sortOrder == "Likes ASC" ? "Likes DESC" : "Likes ASC";
            ViewBag.DislikesAscDescSortParam = sortOrder == "Dislikes ASC" ? "Dislikes DESC" : "Dislikes ASC";
            //ViewBag.CreatedbyAscDescSortParam = sortOrder == "CreatedBy ASC" ? "CreatedBy DESC" : "CreatedBy ASC"; 
            // TODO CREATEDBY SORTERING??

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {

                searchString = currentFilter;
            }

            var coolBooksContext = _context.Review.Include(r => r.Book)
                                                  .Where(r => r.IsDeleted != true 
                                                  && r.Book.IsDeleted != true )
                                                  .Select(r => r);

            switch (sortOrder)
            {
                case "Book DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.Book.Title);
                    break;
                case "Book ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.Book.Title);
                    break;
                case "Title DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.Title);
                    break;
                case "Title ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.Title);
                    break;
                case "Text DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.Text);
                    break;
                case "Text ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.Text);
                    break;
                case "Rating DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.Rating);
                    break;
                case "Rating ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.Rating);
                    break;
                case "Likes DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.LikeCount);
                    break;
                case "Likes ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.LikeCount);
                    break;
                case "Dislikes DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.DisLikeCount);
                    break;
                case "Dislikes ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.DisLikeCount);
                    break;
                case "Created DESC":
                    coolBooksContext = coolBooksContext.OrderByDescending(b => b.Created);
                    break;
                case "Created ASC":
                    coolBooksContext = coolBooksContext.OrderBy(b => b.Created);
                    break;            
                default:
                    coolBooksContext = coolBooksContext.OrderBy(b => b.Id);
                    break;
            }
            int pageSize = 5;
            //return View(await authors.ToList());
            return View(await PaginatedList<Review>.CreateAsync(coolBooksContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var userId = userManager.GetUserId(User);

            var review = await _context.Review
                .Include(r => r.Book)
                .Include(r => r.reportedReviews.Where(r => r.UserId == userId ))
                .Include(r => r.Comments)
                    .ThenInclude(c => c.CoolBooksUser)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.CommentLikes)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.comments) 
                .Include(r => r.Comments)
                    .ThenInclude(c => c.ReportedComments.Where(rc => rc.UserId == userId))
                .FirstOrDefaultAsync(m => m.Id == id);

            //gettolösning för att loada alla kommentarer...
            //update! tack vare reviewIDNested så är den här queryn betydligt mycket mindre
            var temp = await _context.Comment
                .Include(c => c.comments.Where(c => c.reviewIdNested == id))
                .Include(c => c.CommentLikes)
                .Include(c => c.ReportedComments)
                .Include(c => c.CoolBooksUser)
                .Where(c => c.reviewIdNested == id)
                .ToListAsync();

            if (review == null)
            {
                return NotFound();
            }
            
            
            ViewBag.Like = likedislike.Getlikecounts((int)id);
            ViewBag.Dislike = likedislike.Getdislikecounts((int)id);
            ViewBag.AllUserlikedislike = likedislike.GetallUser((int)id);

            //ViewBag.commentLike = commentlikedislike.Getlikecounts((int)id); // Vi får ju review id när vi försöker få kommentarens id.
            //ViewBag.commentDislike = commentlikedislike.Getdislikecounts((int)id);
            //ViewBag.commentAllUserlikedislike = commentlikedislike.GetallUser((int)id);


            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Description");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,Rating")] CreateReviewViewModel review, string? returnUrl,int id)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            if (ModelState.IsValid)
            {

                Review reviewToSave = new Review
                {
                    BookId = id,
                    Title = review.Title,
                    Text = review.Text,
                    Rating = review.Rating,
                    CreatedBy = userManager.GetUserId(User)
                };

                _context.Add(reviewToSave);
                await _context.SaveChangesAsync();

                //update rating on book
                var book = _context.Book.Include(b => b.Authors).Where(b => b.Id == reviewToSave.BookId).FirstOrDefault();
                
                if (book != null)
                {
                    var rating = _context.Review.Where(r => r.BookId == reviewToSave.BookId).Average(r => r.Rating);

                    book.Rating = rating;
                }

                //update rating on author
                foreach (var author in book.Authors)
                {
                    //var books = _context.Book.Include(a => a.Authors.Where(a => a.Id == author.Id)).ToList();
                    var books = _context.Author.Include(a => a.Books).FirstOrDefault(a => a.Id == author.Id);

                    List<double> avg = new List<double>();

                    foreach (var b in books.Books)
                    {
                        try
                        {
                            var rating = _context.Review.Where(r => r.BookId == b.Id).Average(r => r.Rating);
                            avg.Add(rating);
                        }
                        catch (Exception)
                        {
                            //logga felet?                            
                        }                                           
                        
                    }

                    author.Rating = avg.Average();
                }

                await _context.SaveChangesAsync();

                return Redirect(returnUrl);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
                //return View("~/Views/Books/Details");
            }
            else
            {
                return RedirectToAction("Home", "Index");
            }

        }

        // GET: Reviews/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();                
            }

            //låt bara skaparen och admin editera
            if (userManager.GetUserId(User) == review.CreatedBy || User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
        
                EditReviewViewModel vm = new EditReviewViewModel();

                vm.Id = review.Id;
                vm.BookId = review.BookId;
                vm.Title = review.Title;
                vm.Text = review.Text;
                vm.Rating = review.Rating;
                vm.IsDeleted = review.IsDeleted;
                vm.Created  = review.Created;
            

                ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", review.BookId);

                return View(vm);

              
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditReviewViewModel inputReview, string? returnUrl)
        {
            if (id != inputReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var reviewToUpdate = _context.Review.FirstOrDefault(r => r.Id == inputReview.Id);

                reviewToUpdate.BookId = inputReview.BookId;
                reviewToUpdate.Title = inputReview.Title;
                reviewToUpdate.Text = inputReview.Text;
                reviewToUpdate.Rating = inputReview.Rating;
                reviewToUpdate.IsDeleted = inputReview.IsDeleted;
                reviewToUpdate.LastUpdated = DateTime.Now;
                reviewToUpdate.UpdatedBy = userManager.GetUserId(User);

                try
                {
                    _context.Update(reviewToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(reviewToUpdate.Id))
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
                    return RedirectToAction("Index", "Reviews");
                }
                else
                {
                    return Redirect(returnUrl);
                }
                
            }

            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", inputReview.BookId);
            return View(inputReview);
        }

        // GET: Reviews/Block/5
        [Authorize (Roles = "Moderator, Admin")]
        public async Task<IActionResult> Block(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }



        // POST: Reviews/Block/5
        [HttpPost, ActionName("Block")]
        [Authorize(Roles = "Moderator, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockConfirmed(int id)
        {

            var review = await _context.Review.FindAsync(id);

            review.IsDeleted = true;
            _context.Review.Update(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            review.IsDeleted = true;
            review.LastUpdated = DateTime.Now;
            review.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Review.Update(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
        public ActionResult Like(int id, bool status)
        {
            var user = userManager.GetUserId(User);
            if (user == null)
            {
                return Content("Logga in för att använda like/dislike!");
            }
            var result = likedislike.Like(id, status, user);
            return Content(result);
        }

        public ActionResult Report(int id) {
            
            var review = _context.Review.Find(id);

            if (review == null) 
            {
                return NotFound(); 
            }

            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return NotFound();
            }

            var reportedReview = _context.ReportedReviews
                .Where(rr => rr.ReviewId == id && rr.UserId == userId)
                .FirstOrDefault();

            if (reportedReview == null) 
            {
                reportedReview = new ReportedReview();
                reportedReview.UserId = userId;
                reportedReview.Created = DateTime.Now;
                reportedReview.ReviewId = id;
                _context.Add(reportedReview);
                _context.SaveChanges();

                var antal = _context.ReportedReviews
                            .Where(rr => rr.ReviewId == id)
                            .Count();
                if (antal == 5)
                {
                    // Om en review får 5 reports blir den automatiskt blockerad, men bara en gång.
                    reportedReview.Review.IsDeleted = true;
                    _context.SaveChanges();
                }

                return Content("Rapporterad");
            }
            else
            {                
                _context.ReportedReviews.Remove(reportedReview);
                _context.SaveChanges();
                return Content("Rapportera");
            }

           
        }
    }
}
