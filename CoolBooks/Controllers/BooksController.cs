#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Data;
using Microsoft.AspNetCore.Authorization;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CoolBooks.Models
{
    public class BooksController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BooksController(CoolBooksContext context, IWebHostEnvironment hostEnvironment)
        {
            this._context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchString) // DEN HÄR FUNGERAR :)
        {

            var books = _context.Book.Include(g => g.Genres)
                                     .Include(a => a.Authors)
                                     .Select(b => b);

            if (!string.IsNullOrEmpty(searchString)) // Söker på boktitel
            {
                books = books.Where(t => t.Title!.Contains(searchString) || // Sök på Titel
                
                                         t.Authors // Sök på Author
                                         .All(t => t.FirstName // du får inte köra where innuti where! därför .all                                                      
                                         .Contains(searchString)) ||

                                         t.Genres // Sök på Genre
                                         .All(t => t.Name
                                         .Contains(searchString))
                );
                                   
            }
            //if (!string.isnullorempty(searchstring)) // söker på boktitel
            //{
            //    books = books.where(t => t.authors
            //                 .all(t => t.firstname // du får inte köra where innuti where! därför .all                                                      
            //                 .contains(searchstring)));
            //}


            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            BookWithReviewsViewModel vm = new BookWithReviewsViewModel();

            if (id == null)
            {
                return NotFound();
            }

            vm.book = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);

            if (vm.book == null)
            {
                return NotFound();
            }

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            vm.reviews = await _context.Review.Where(r => r.BookId == id).ToListAsync();

            return View(vm);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel inputBook)
        {
            
            if (ModelState.IsValid)
            {
                Book bookToCeate = new Book();

                bookToCeate.Title = inputBook.Title;
                bookToCeate.Description = inputBook.Description;
                bookToCeate.ISBN = inputBook.ISBN;
                bookToCeate.Created = DateTime.Now;
                bookToCeate.IsDeleted = false;
                bookToCeate.ImagePath = "";

                foreach (var genre in inputBook.Genres)
                {
                    if (genre.IsSelected)
                    {
                        Genre bookGenre = new Genre { Id = genre.GenreId };
                        _context.Genre.Attach(bookGenre);
                        bookToCeate.Genres.Add(bookGenre);                        
                    }               
                 }

                foreach (var author in inputBook.Authors)
                {
                    if (author.IsSelected)
                    {
                        Author bookAuthor = new Author { Id = author.AuthorId };
                        _context.Author.Attach(bookAuthor);
                        bookToCeate.Authors.Add(bookAuthor);
                    }
                }


                //Insert record        
                _context.Add(bookToCeate);
                await _context.SaveChangesAsync();


                //save image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(inputBook.ImageFile.FileName);
                string extension = Path.GetExtension(inputBook.ImageFile.FileName);

                fileName = bookToCeate.Id + extension; //name it after the books Id

                string path = Path.Combine(wwwRootPath + "/Images/Books/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await inputBook.ImageFile.CopyToAsync(fileStream);
                }

                //update imagepath (kanske inte nödvändig.. men for now!)
                bookToCeate.ImagePath = Path.Combine("/Images/Books/", fileName);
                _context.Book.Attach(bookToCeate);
                _context.Entry(bookToCeate).State = EntityState.Modified;
                _context.SaveChanges();




                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("CreateBook","Administration");
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ISBN,isDeleted,Created")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
