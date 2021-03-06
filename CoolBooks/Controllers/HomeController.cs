using CoolBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Data;
using CoolBooks.ViewModels;

namespace CoolBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CoolBooksContext _context;

        public HomeController(ILogger<HomeController> logger, CoolBooksContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel vm = new HomeIndexViewModel();

            var random = new Random();
            int randomnr = random.Next(0, _context.Book.Count());
      
            vm.RandomBook = _context.Book.Include(b => b.Genres)
                                         .Include(b => b.Authors)
                                         .OrderBy(x => Guid.NewGuid())
                                         .First();

            vm.Books = _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .OrderByDescending(x => x.Created)
                .Take(3)
                .ToList();

            vm.AllBooks = _context.Book
                .OrderByDescending(x => x.Created)
                .Take(20)
                .ToList();

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}