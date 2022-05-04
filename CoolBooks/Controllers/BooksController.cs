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
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using PagedList.Mvc;
using PagedList;
using CoolBooks.Services;

namespace CoolBooks.Models
{
    public class BooksController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;



        public BooksController(CoolBooksContext context, IWebHostEnvironment hostEnvironment, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
        {
            this._context = context;
            this._hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber) // DEN HÄR FUNGERAR :)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.searchString = searchString;

            ViewBag.TitleAscDescSortParam = sortOrder == "Title ASC" ? "Title DESC" : "Title ASC";
            ViewBag.DescriptionAscDescSortParam = sortOrder == "Description ASC" ? "Description DESC" : "Description ASC";
            ViewBag.GenreAscDescSortParam = sortOrder == "Genre ASC" ? "Genre DESC" : "Genre ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";
            ViewBag.AuthorAscDescSortParam = sortOrder == "Authors ASC" ? "Authors DESC" : "Authors ASC";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var books = _context.Book.Include(g => g.Genres.Where(g => g.IsDeleted != true))
                                     .Include(a => a.Authors.Where(a => a.IsDeleted != true))   
                                     .Where(b => b.IsDeleted != true)
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
            int pageSize = 10;
            //return View(await books.ToListAsync());
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Books/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string sortOrder, int? pageNumber)
        {
            
            BookWithReviewsViewModel vm = new BookWithReviewsViewModel();

            ViewBag.BookAscDescSortParam = sortOrder == "Book ASC" ? "Book DESC" : "Book ASC";
            ViewBag.CreatedByAscDescSortParam = sortOrder == "CreatedBy ASC" ? "CreatedBy DESC" : "CreatedBy ASC";
            ViewBag.TitleAscDescSortParam = sortOrder == "Title ASC" ? "Title DESC" : "Title ASC";
            ViewBag.TextAscDescSortParam = sortOrder == "Text ASC" ? "Text DESC" : "Text ASC";
            ViewBag.RatingAscDescSortParam = sortOrder == "Rating ASC" ? "Rating DESC" : "Rating ASC";
            ViewBag.CreatedAscDescSortParam = sortOrder == "Created ASC" ? "Created DESC" : "Created ASC";

            if (id == null)
            {
                return NotFound();
            }

            vm.book = await _context.Book
                .Include(b => b.Genres)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (vm.book == null)
            {
                return NotFound();
            }

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var userId = userManager.GetUserId(User);


            var reviews = _context.Review
                .Include(r => r.CoolBooksUser)
                .Include(r => r.reportedReviews.Where(rc => rc.UserId == userId))
                .Include(r => r.ReviewLikes.Where(rc => rc.UserId == userId))
                .Include(r => r.Comments)
                    .ThenInclude(c => c.CoolBooksUser)                
                .Include(r => r.Comments)
                    .ThenInclude(c => c.comments)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.ReportedComments.Where(rc => rc.UserId == userId))
                .Where(r => r.BookId == id && r.IsDeleted == false)
                
                .Select(b => b);


            //Alla lager av kommentarerer inkluderas inte om vi inte gör dom ToList() i detta steg först;
            //behöver dom "trackas"? Dom är ju redan inkluderarade i qureyn ovan?
            var comments = _context.Comment
                .Include(c => c.comments.Where(c => c.reviewIdNested == id))
                .Include(c => c.CommentLikes)
                .Include(c => c.ReportedComments)
                .Include(c => c.CoolBooksUser)
                .Where(c => c.reviewIdNested == id)
                .ToList();



            switch (sortOrder)
            {
                case "Book DESC":
                    reviews = reviews.OrderByDescending(b => b.Book);
                    break;
                case "Book ASC":
                    reviews = reviews.OrderBy(b => b.Book);
                    break;
                case "CreatedBy DESC":
                    reviews = reviews.OrderByDescending(b => b.CreatedBy);
                    break;
                case "CreatedBy ASC":
                    reviews = reviews.OrderBy(b => b.CreatedBy);
                    break;
                case "Title DESC":
                    reviews = reviews.OrderByDescending(b => b.Title);
                    break;
                case "Title ASC":
                    reviews = reviews.OrderBy(b => b.Title);
                    break;
                case "Text DESC":
                    reviews = reviews.OrderByDescending(b => b.Text);
                    break;
                case "Text ASC":
                    reviews = reviews.OrderBy(b => b.Text);
                    break;
                case "Rating DESC":
                    reviews = reviews.OrderByDescending(b => b.Rating);
                    break;
                case "Rating ASC":
                    reviews = reviews.OrderBy(b => b.Rating);
                    break;
                case "Created DESC":
                    reviews = reviews.OrderByDescending(b => b.Created);
                    break;
                case "Created ASC":
                    reviews = reviews.OrderBy(b => b.Created);
                    break;
                default:
                    reviews = reviews.OrderBy(b => b.Id);
                    break;
            }
            int pageSize = 2;
            vm.reviews = await PaginatedList<Review>.CreateAsync(reviews, pageNumber ?? 1, pageSize);
            return View(vm);
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            CreateBookViewModel vm = new CreateBookViewModel();

            var authors = _context.Author.Where(a => a.IsDeleted != true).ToList();

            var genres = _context.Genre.Where(g => g.IsDeleted != true).ToList();

            foreach (Author author in authors)
            {
                BookAuthorViewModel bookAuthorViewModel = new BookAuthorViewModel();

                bookAuthorViewModel.AuthorId = author.Id;
                bookAuthorViewModel.AuthorFirstName = author.FirstName;
                bookAuthorViewModel.AuthorLastName = author.LastName;
                bookAuthorViewModel.IsSelected = false;

                vm.Authors.Add(bookAuthorViewModel);
            }

            foreach (Genre genre in genres)
            {
                BookGenreViewModel bookGenreViewModel = new BookGenreViewModel
                {
                    GenreId = genre.Id,
                    GenreName = genre.Name,
                    IsSelected = false
                };

                vm.Genres.Add(bookGenreViewModel);
            }

            return View(vm);
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel inputBook)
        {
            //Kolla så att minst en genre är vald
            var selectedGenres = inputBook.Genres.Where(g => g.IsSelected == true).Count();
            if (selectedGenres == 0)
            {
                ModelState.AddModelError("Genres", "Du måste välja minst en genre");
            }
            //Kolla så att minst en författare är vald
            var selectedAuthors = inputBook.Authors.Where(a => a.IsSelected == true).Count();
            if (selectedAuthors == 0)
            {
                ModelState.AddModelError("Authors", "Du måste välja minst en författare");
            }
            
            if (selectedAuthors == 0  || selectedGenres == 0)
            {
                return View(inputBook);
            }

            if (ModelState.IsValid)
            {
                Book bookToCreate = new Book();

                bookToCreate.Title = inputBook.Title;
                bookToCreate.Description = inputBook.Description;
                bookToCreate.ISBN = inputBook.ISBN;
                bookToCreate.Created = DateTime.Now;
                bookToCreate.IsDeleted = false;
                bookToCreate.ImagePath = "";
                bookToCreate.CreatedBy = userManager.GetUserId(User);

                foreach (var genre in inputBook.Genres)
                {
                    if (genre.IsSelected)
                    {
                        Genre bookGenre = new Genre { Id = genre.GenreId };
                        _context.Genre.Attach(bookGenre);
                        bookToCreate.Genres.Add(bookGenre);                        
                    }               
                 }

                foreach (var author in inputBook.Authors)
                {
                    if (author.IsSelected)
                    {
                        Author bookAuthor = new Author { Id = author.AuthorId };
                        _context.Author.Attach(bookAuthor);
                        bookToCreate.Authors.Add(bookAuthor);
                    }
                }
               
                //Insert record
                try
                {
                    _context.Add(bookToCreate);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {                    
                        if (ex.InnerException.Message.Contains("IX_Book_ISBN")) //uniqeconstraint error på isbn
                        {
                            ModelState.AddModelError("ISBN", "ISBN finns redan i databasen");
                            return View(inputBook);
                        }                       
                    
                    return View(inputBook);

                }                
     
                //save image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(inputBook.ImageFile.FileName);
                string extension = Path.GetExtension(inputBook.ImageFile.FileName);

                fileName = bookToCreate.Id + extension; //name it after the books Id

                string path = Path.Combine(wwwRootPath + "/Images/Books/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await inputBook.ImageFile.CopyToAsync(fileStream);
                }

                //update imagepath (kanske inte nödvändig.. men for now!)
                bookToCreate.ImagePath = Path.Combine("/Images/Books/", fileName);
                _context.Book.Attach(bookToCreate);
                _context.Entry(bookToCreate).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(inputBook);
            //return RedirectToAction("CreateBook","Administration");
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditBookViewModel editBookViewModel = new EditBookViewModel();

            var book = _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Where(b => b.Id == id).FirstOrDefault();

            var authors = _context.Author.ToList();            

            var genres = _context.Genre.ToList();

            editBookViewModel.BookId = book.Id;
            editBookViewModel.Title = book.Title;
            editBookViewModel.Description = book.Description;
            editBookViewModel.ISBN = book.ISBN;
            editBookViewModel.IsDeleted = book.IsDeleted;
            editBookViewModel.ImagePath = book.ImagePath;

            foreach (Author author in authors)
            {
                BookAuthorViewModel bookAuthorViewModel = new BookAuthorViewModel();

                bookAuthorViewModel.AuthorId = author.Id;
                bookAuthorViewModel.AuthorFirstName = author.FirstName;
                bookAuthorViewModel.AuthorLastName = author.LastName;
                bookAuthorViewModel.IsSelected = false;
                //kolla om boken tillhör en genre och bocka för den checkboxen

                foreach (var a in book.Authors)
                {
                    if (a.Id == author.Id)
                    {
                        bookAuthorViewModel.IsSelected = true;
                        break;
                    }
                }               

                editBookViewModel.Authors.Add(bookAuthorViewModel);
            }

            foreach (Genre genre in genres)
            {
                BookGenreViewModel bookGenreViewModel = new BookGenreViewModel();
                bookGenreViewModel.GenreId = genre.Id;
                bookGenreViewModel.GenreName = genre.Name;
                bookGenreViewModel.IsSelected = false;

                //kolla om boken tillhör en genre och bocka för den checkboxen
                foreach (var g in book.Genres)
                {
                    if (g.Id == genre.Id)
                    {
                        bookGenreViewModel.IsSelected = true;
                        break;
                    } 
                }

                editBookViewModel.Genres.Add(bookGenreViewModel);
            }

            //if (book == null)
            //{
            //    return NotFound();
            //}

            return View(editBookViewModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookViewModel bookInput)
        {
            //if (id != bookInput.BookId)
            //{
            //    return NotFound();
            //}
                     

            if (ModelState.IsValid)
            {

                Book bookToUpdate = await _context.Book
                        .Include(b => b.Authors)
                        .Include(b => b.Genres)
                        .Where(b => b.Id == id)
                        .FirstOrDefaultAsync();

                bookToUpdate.Title = bookInput.Title;
                bookToUpdate.Description = bookInput.Description;
                bookToUpdate.ISBN = bookInput.ISBN;
                bookToUpdate.Id = id;
                bookToUpdate.IsDeleted = bookInput.IsDeleted;

                //track authors (ghettolösning?)
                //var authors = _context.Author.ToList();

                foreach (var author in bookInput.Authors)
                {
                    if (author.IsSelected)
                    {
                        if (bookToUpdate.Authors.Where(a => a.Id == author.AuthorId).Count() == 0) 
                        {
                            Author a = new Author { Id = author.AuthorId };
                            _context.Author.Attach(a);
                            bookToUpdate.Authors.Add(a);
                        }
                    }
                    else //inte checkad
                    {
                        if (bookToUpdate.Authors.Where(a => a.Id == author.AuthorId).Count() > 0)
                        {
                            bookToUpdate.Authors.Remove(bookToUpdate.Authors.Single(a => a.Id == author.AuthorId));                            
                        }
                    }
                }

                //track genres (ghettolösning?)
                //var genres = _context.Genre.ToList();

                foreach (var genre in bookInput.Genres)
                {
                    if (genre.IsSelected)
                    {
                        if (bookToUpdate.Genres.Where(a => a.Id == genre.GenreId).Count() == 0)
                        {
                            Genre g = new Genre { Id = genre.GenreId };
                            _context.Genre.Attach(g);
                            bookToUpdate.Genres.Add(g);
                        }
                    }
                    else //inte checkad
                    {
                        if (bookToUpdate.Genres.Where(a => a.Id == genre.GenreId).Count() > 0)
                        {
                            bookToUpdate.Genres.Remove(bookToUpdate.Genres.Single(a => a.Id == genre.GenreId));
                        }
                    }
                }

                //save/update image
                if (bookInput.ImageFile != null)
                {                    
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(bookInput.ImageFile.FileName);
                    string extension = Path.GetExtension(bookInput.ImageFile.FileName);


                    fileName = bookInput.BookId + extension; //name it after the booksId

                    string path = Path.Combine(wwwRootPath + "/Images/Books/", fileName);


                    //ta bort den gamla bilden
                    if (System.IO.File.Exists(Path.Combine(wwwRootPath, bookToUpdate.ImagePath)))
                    {
                        System.IO.File.Delete(Path.Combine(wwwRootPath, bookToUpdate.ImagePath));
                    }

                    //spara bilden
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await bookInput.ImageFile.CopyToAsync(fileStream);
                    }

                    //update imagepath (kanske inte nödvändig.. men for now!)
                    bookToUpdate.ImagePath = Path.Combine("/Images/Books/", fileName);
                }


                try
                {
                    _context.Update(bookToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookToUpdate.Id))
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
            return View(bookInput);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            book.IsDeleted = true;
            _context.Book.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
