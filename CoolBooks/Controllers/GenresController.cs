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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace CoolBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;
        public GenresController(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Genres
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.FirstNameAscDescSortParam = sortOrder == "Name ASC" ? "Name DESC" : "Name ASC";
            ViewBag.DescriptionAscDescSortParam = sortOrder == "Description ASC" ? "Description DESC" : "Description ASC";    
            ViewBag.CreatedAscDescSortParam = sortOrder == "Created ASC" ? "Created DESC" : "Created ASC";

            var genres = _context.Genre
                                     //.Include(g => g.Genres)
                                     //.Include(a => a.Authors)
                                     //.Where(b => b.IsDeleted != true)
                                     .Select(b => b);
            switch (sortOrder)
            {
                case "Name DESC":
                    genres = genres.OrderByDescending(b => b.Name);
                    break;
                case "Name ASC":
                    genres = genres.OrderBy(b => b.Name);
                    break;
                case "Description DESC":
                    genres = genres.OrderByDescending(b => b.Description);
                    break;
                case "Description ASC":
                    genres = genres.OrderBy(b => b.Description);
                    break;
                case "Created DESC":
                    genres = genres.OrderByDescending(b => b.Created);
                    break;
                case "Created ASC":
                    genres = genres.OrderBy(b => b.Created);
                    break;
                default:
                    genres = genres.OrderBy(b => b.Id);
                    break;
            }
            return View(genres.ToList());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id, string sortOrder)
        {
            GenreDetailsViewModel vm = new GenreDetailsViewModel();
            vm.Genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.TitleAscDescSortParam = sortOrder == "Title ASC" ? "Title DESC" : "Title ASC";
            ViewBag.DescriptionAscDescSortParam = sortOrder == "Description ASC" ? "Description DESC" : "Description ASC";
            ViewBag.GenreAscDescSortParam = sortOrder == "Genre ASC" ? "Genre DESC" : "Genre ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";
            ViewBag.AuthorAscDescSortParam = sortOrder == "Authors ASC" ? "Authors DESC" : "Authors ASC";

            var books = _context.Book.Include(g => g.Genres)
                                     .Include(a => a.Authors)
                                     .Where(b => b.IsDeleted != true)
                                     .Where(b => b.Genres.Any(g => g.Id == id))
                                     .Select(b => b);

            switch (sortOrder)
            {
                case "Title DESC":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Title ASC":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "Description DESC":
                    books = books.OrderByDescending(b => b.Description);
                    break;
                case "Description ASC":
                    books = books.OrderBy(b => b.Description);
                    break;
                case "Genre DESC":
                    books = books.OrderByDescending(b => b.Genres);
                    break;
                case "Genre ASC":
                    books = books.OrderBy(b => b.Genres);
                    break;
                case "Rating DESC":
                    books = books.OrderByDescending(b => b.Rating);
                    break;
                case "Rating ASC":
                    books = books.OrderBy(b => b.Rating);
                    break;
                case "Authors DESC":
                    books = books.Include(a => a.Authors
                                                .OrderByDescending(b => b.FirstName));
                    break;
                case "Authors ASC":
                    books = books.Include(a => a.Authors
                                                .OrderBy(b => b.FirstName));
                    break;
                default:
                    books = books.OrderBy(b => b.Id);
                    break;
            }

            vm.Books = books.ToList();
            return View(vm);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Created")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                genre.CreatedBy = userManager.GetUserId(User);
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Created")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }
    }
}
