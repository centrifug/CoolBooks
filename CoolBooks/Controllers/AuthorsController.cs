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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CoolBooks.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;

        public AuthorsController(CoolBooksContext context, IWebHostEnvironment hostEnvironment, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.FirstNameAscDescSortParam = sortOrder == "FirstName ASC" ? "FirstName DESC" : "FirstName ASC";
            ViewBag.LastNameAscDescSortParam = sortOrder == "LastName ASC" ? "LastName DESC" : "LastName ASC";
            ViewBag.BirthDateAscDescSortParam = sortOrder == "BirthDate ASC" ? "BirthDate DESC" : "BirthDate ASC";
            ViewBag.CreatedAscDescSortParam = sortOrder == "Created ASC" ? "Created DESC" : "Created ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";

            var authors = _context.Author
                                     //.Include(g => g.Genres)
                                     //.Include(a => a.Authors)
                                     //.Where(b => b.IsDeleted != true)
                                     .Select(b => b);
            switch (sortOrder)
            {
                case "FirstName DESC":
                    authors = authors.OrderByDescending(b => b.FirstName);
                    break;
                case "FirstName ASC":
                    authors = authors.OrderBy(b => b.FirstName);
                    break;
                case "LastName DESC":
                    authors = authors.OrderByDescending(b => b.LastName);
                    break;
                case "LastName ASC":
                    authors = authors.OrderBy(b => b.LastName);
                    break;
                case "BirthDate DESC":
                    authors = authors.OrderByDescending(b => b.BirthDate);
                    break;
                case "BirthDate ASC":
                    authors = authors.OrderBy(b => b.BirthDate);
                    break;
                case "Created DESC":
                    authors = authors.OrderByDescending(b => b.Created);
                    break;
                case "Created ASC":
                    authors = authors.OrderBy(b => b.Created);
                    break;
                case "Rating DESC":
                    authors = authors.OrderByDescending(b => b.Rating);
                    break;
                case "Rating ASC":
                    authors = authors.OrderBy(b => b.Rating);
                    break;
                default:
                    authors = authors.OrderBy(b => b.Id);
                    break;
            }
            return View(authors.ToList());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id, string sortOrder)
        {
            AuthorDetailsViewModel vm = new AuthorDetailsViewModel();
            vm.Author = await _context.Author
                                      .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.TitleAscDescSortParam = sortOrder == "Title ASC" ? "Title DESC" : "Title ASC";
            ViewBag.DescriptionAscDescSortParam = sortOrder == "Description ASC" ? "Description DESC" : "Description ASC";
            ViewBag.GenreAscDescSortParam = sortOrder == "Genre ASC" ? "Genre DESC" : "Genre ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";
            ViewBag.AuthorAscDescSortParam = sortOrder == "Authors ASC" ? "Authors DESC" : "Authors ASC";

            var books = _context.Book.Include(g => g.Genres)
                                     .Include(a => a.Authors)
                                     .Where(b => b.IsDeleted != true)
                                     .Where(b => b.Authors.Any(g => g.Id == id))
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

        // GET: Authors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorViewModel authorInput)
        {
            if (ModelState.IsValid)
            {
                Author authorToCreate = new Author();
                var user = await userManager.GetUserAsync(User);

                authorToCreate.FirstName = authorInput.FirstName;
                authorToCreate.LastName = authorInput.LastName;
                authorToCreate.BirthDate = authorInput.BirthDate;
                authorToCreate.CreatedBy = user.Id;
                authorToCreate.Created = DateTime.Now;
                authorToCreate.ImagePath = "";

                _context.Add(authorToCreate);
                await _context.SaveChangesAsync();


                //save image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(authorInput.ImageFile.FileName);
                string extension = Path.GetExtension(authorInput.ImageFile.FileName);

                fileName = authorToCreate.Id + extension; //name it after the books Id

                string path = Path.Combine(wwwRootPath + "/Images/Authors/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await authorInput.ImageFile.CopyToAsync(fileStream);
                }

                //update imagepath (kanske inte nödvändig.. men for now!)
                authorToCreate.ImagePath = Path.Combine("/Images/Authors/", fileName);
                _context.Author.Attach(authorToCreate);
                _context.Entry(authorToCreate).State = EntityState.Modified;
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            return View(authorInput);
        }

        // GET: Authors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EditAuthorViewModel vm = new EditAuthorViewModel();
            var author = await _context.Author.FindAsync(id);

            vm.FirstName = author.FirstName;
            vm.LastName = author.LastName;
            vm.BirthDate = author.BirthDate;                
            
            if (author == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAuthorViewModel authorInput)
        {
            if (id != authorInput.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Author authorToUpdate = new Author();
                var user = await userManager.GetUserAsync(User);
                                
                authorToUpdate = await _context.Author.FirstOrDefaultAsync(a => a.Id == authorInput.Id);

                authorToUpdate.FirstName = authorInput.FirstName;
                authorToUpdate.LastName = authorInput.LastName;
                authorToUpdate.BirthDate = authorInput.BirthDate;
                authorToUpdate.LastUpdated = DateTime.Now;
                authorToUpdate.UpdatedBy = user.Id;

                try
                {
                    _context.Update(authorToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(authorToUpdate.Id))
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
            return View(authorInput);
        }

        // GET: Authors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
