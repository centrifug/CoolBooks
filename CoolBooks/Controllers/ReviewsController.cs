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
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.BookAscDescSortParam = sortOrder == "Book ASC" ? "Book DESC" : "Book ASC";
            ViewBag.TitleAscDescSortParam = sortOrder == "Title ASC" ? "Title DESC" : "Title ASC";
            ViewBag.TextAscDescSortParam = sortOrder == "Text ASC" ? "Text DESC" : "Text ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";
            ViewBag.CreatedAscDescSortParam = sortOrder == "Created ASC" ? "Created DESC" : "Created ASC";
            ViewBag.LikesAscDescSortParam = sortOrder == "Likes ASC" ? "Likes DESC" : "Likes ASC";
            ViewBag.DislikesAscDescSortParam = sortOrder == "Dislikes ASC" ? "Dislikes DESC" : "Dislikes ASC";
            //ViewBag.CreatedbyAscDescSortParam = sortOrder == "CreatedBy ASC" ? "CreatedBy DESC" : "CreatedBy ASC"; 
            // TODO CREATEDBY SORTERING??

            var coolBooksContext = _context.Review.Include(r => r.Book)
                                                  .Where(r => r.IsDeleted == false)
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
            
            return View(await coolBooksContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Book)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.CoolBooksUser)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            //gettolösning för att loada alla kommentarer... 
            var temp = await _context.Comment.Include(c => c.comments).ToListAsync();

            if (review == null)
            {
                return NotFound();
            }
            
            
            ViewBag.Like = likedislike.Getlikecounts((int)id);
            ViewBag.Dislike = likedislike.Getdislikecounts((int)id);
            ViewBag.AllUserlikedislike = likedislike.GetallUser((int)id);

            ViewBag.commentLike = commentlikedislike.Getlikecounts((int)id); // Vi får ju review id när vi försöker få kommentarens id.
            ViewBag.commentDislike = commentlikedislike.Getdislikecounts((int)id);
            ViewBag.commentAllUserlikedislike = commentlikedislike.GetallUser((int)id);

            

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
            if (userManager.GetUserId(User) != review.CreatedBy)
            {
                if (!User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home");
                }                
            }

            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Description", review.BookId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,Title,Text,Rating,IsDeleted,Created")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Description", review.BookId);
            return View(review);
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
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
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
            var result = likedislike.Like(id, status, user);
            return Content(result);
        }

    }
}
