using CoolBooks.Data;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity;

namespace CoolBooks.Services
{
    public class Test : ITest
    {
 
        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;

        public Test(CoolBooksContext ctx, UserManager<CoolBooksUser> userManager)
            {
                _context = ctx;
            this.userManager = userManager;
            
            }

            public Book GetBook(int id)
            {
                var book = from u in _context.Book where u.Id == id select u;
                if (book.Count() == 1)
                {
                    return book.First();
                }
                return null;
            }
        




    }
    public interface ITest
    {
        Book GetBook(int id);
    }
}
