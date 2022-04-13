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

namespace CoolBooks.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly CoolBooksContext _context;

        public AuthorsController(CoolBooksContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.FirstNameAscDescSortParam = sortOrder == "FirstName ASC" ? "FirstName DESC" : "FirstName ASC";
            ViewBag.LastNameAscDescSortParam = sortOrder == "LastName ASC" ? "LastName DESC" : "LastName ASC";
            ViewBag.BirthDateAscDescSortParam = sortOrder == "BirthDate ASC" ? "BirthDate DESC" : "BirthDate ASC";
            ViewBag.CreatedAscDescSortParam = sortOrder == "Created ASC" ? "Created DESC" : "Created ASC";

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
                default:
                    authors = authors.OrderBy(b => b.Id);
                    break;
            }
            return View(authors.ToList());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .Include(a => a.Books) //inkludera alla böcker av den här författaren
                .ThenInclude(b => b.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (author == null)
            {
                return NotFound();
            }


            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate,Created")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BirthDate,Created")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
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
