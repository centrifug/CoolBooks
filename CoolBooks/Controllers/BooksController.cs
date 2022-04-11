﻿#nullable disable
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

namespace CoolBooks.Models
{

    
    public class BooksController : Controller
    {
        private readonly CoolBooksContext _context;

        public BooksController(CoolBooksContext context)
        {
            _context = context;
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

            vm.reviews = await _context.Review.Where(r => r.BookId == id).ToListAsync();

            return View(vm);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ISBN,isDeleted,Created")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
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
