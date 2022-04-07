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
            // vm.RandomBook = await _context.Book.Take(1).FirstOrDefaultAsync();

            var random = new Random();
            int randomnr = random.Next(1, _context.Book.Count());
            vm.RandomBook = await _context.Book.
                OrderBy(x => x.Id == randomnr)
                //.Take(1) 
                .FirstOrDefaultAsync();

            //Just nu funkar det bara på #1 och #2 även fast vi har 5 böcker, titta på detta asap!

            return View(vm);
            
            //return View(await _context.Book.Take(1)
            //    .FirstOrDefaultAsync());
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