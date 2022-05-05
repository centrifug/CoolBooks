using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoolBooks.Controllers
{


    public class AdministrationController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<CoolBooksUser> userManager;

        public AdministrationController(CoolBooksContext context, RoleManager<IdentityRole> roleManager, UserManager<CoolBooksUser> userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateBook()
        {
            CreateBookViewModel vm = new CreateBookViewModel();

            var authors = _context.Author.ToList();

            var genres = _context.Genre.ToList();

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
                BookGenreViewModel bookGenreViewModel = new BookGenreViewModel();
                bookGenreViewModel.GenreId = genre.Id;
                bookGenreViewModel.GenreName = genre.Name;
                bookGenreViewModel.IsSelected = false;

                vm.Genres.Add(bookGenreViewModel);
            }



            return View("/Views/Books/Create.cshtml", vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListRoles()
        {

            var roles = roleManager.Roles.ToList();

            return View(roles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("ListRoles");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var vm = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user.UserName);
                }

            }

            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserInRole(string Id)
        {
            ViewBag.roleId = Id;

            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleVewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVewModel.IsSelected = true;
                }
                else
                {
                    userRoleVewModel.IsSelected = false;
                }

                model.Add(userRoleVewModel);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string Id)
        {

            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = Id });
                    }
                }
            }

            return RedirectToAction("EditRole", new { Id = Id });
        }

        public IActionResult Statistik()
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Idag 23:59:59
            string idag = endDateTime.ToString("dddd");
            int[] dagarAntalKommentar = new int[7];
            string[] dagarKommentar = new string[7];
            int[] dagarAntalReview = new int[7];
            string[] dagarReview = new string[7];
            for (int i = 0; i < 7; i++)
            {
                var kommentarIdag = _context.Comment
                                  .Where(c => c.Created >= startDateTime &&
                                              c.Created <= endDateTime)
                                  .Select(c => c.Text)
                                  .ToArray();

                var reviewIdag = _context.Review
                                  .Where(c => c.Created >= startDateTime &&
                                              c.Created <= endDateTime)
                                  .Select(c => c.Text)
                                  .ToArray();

                dagarKommentar[i] = idag;
                dagarAntalKommentar[i] = kommentarIdag.Length;
                dagarReview[i] = idag;
                dagarAntalReview[i] = reviewIdag.Length;
                startDateTime = startDateTime.AddDays(-1);
                endDateTime = endDateTime.AddDays(-1);
                idag = endDateTime.ToString("dddd");
            }

            DateTime startDateTimeWeek = DateTime.Today;
            DateTime endDateTimeWeek = DateTime.Today.AddDays(-7);
            DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dateTimeFormatInfo.Calendar;
            string week = calendar.GetWeekOfYear(startDateTimeWeek, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek).ToString();
            int[] veckaAntalKommentar = new int[10];
            string[] veckaKommentar = new string[10];
            int[] veckaAntalReview = new int[10];
            string[] veckaReview = new string[10];
            for (int i = 0; i < 10; i++)
            {
                var kommentarVecka = _context.Comment
                                  .Where(c => c.Created <= startDateTimeWeek &&
                                              c.Created >= endDateTimeWeek)
                                  .Select(c => c.Text)
                                  .ToArray();

                var reviewVecka = _context.Review
                                  .Where(c => c.Created <= startDateTimeWeek &&
                                              c.Created >= endDateTimeWeek)
                                  .Select(c => c.Text)
                                  .ToArray();

                veckaKommentar[i] = week;
                veckaAntalKommentar[i] = kommentarVecka.Length;
                veckaReview[i] = week;
                veckaAntalReview[i] = reviewVecka.Length;
                startDateTimeWeek = startDateTimeWeek.AddDays(-7);
                endDateTimeWeek = endDateTimeWeek.AddDays(-7);
                week = calendar.GetWeekOfYear(startDateTimeWeek, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek).ToString();
            }

            var firstDayOfMonth = new DateTime(startDateTime.Year, startDateTime.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            string månad = firstDayOfMonth.ToString("MMMM");
            int[] månadAntalKommentar = new int[12];
            string[] månadKommentar = new string[12];
            int[] månadAntalReview = new int[12];
            string[] månadReview = new string[12];
            for (int i = 0; i < 12; i++)
            {
                var kommentarMånad = _context.Comment
                                  .Where(c => c.Created >= firstDayOfMonth &&
                                              c.Created <= lastDayOfMonth)
                                  .Select(c => c.Text)
                                  .ToArray();

                var reviewMånad = _context.Review
                                  .Where(c => c.Created >= firstDayOfMonth &&
                                              c.Created <= lastDayOfMonth)
                                  .Select(c => c.Text)
                                  .ToArray();

                månadKommentar[i] = månad;
                månadAntalKommentar[i] = kommentarMånad.Length;
                månadReview[i] = månad;
                månadAntalReview[i] = reviewMånad.Length;
                firstDayOfMonth = firstDayOfMonth.AddMonths(-1);
                lastDayOfMonth = lastDayOfMonth.AddMonths(-1);
                månad = firstDayOfMonth.ToString("MMMM");
            }
         
            ViewBag.yaxelK = dagarAntalKommentar;
            ViewBag.xaxelK = dagarKommentar;
            ViewBag.yaxelR = dagarAntalReview;
            ViewBag.xaxelR = dagarReview;

            ViewBag.yaxelK_veckor = veckaAntalKommentar;
            ViewBag.xaxelK_veckor = veckaKommentar;
            ViewBag.yaxelR_veckor = veckaAntalReview;
            ViewBag.xaxelR_veckor = veckaReview;

            ViewBag.yaxelK_månad = månadAntalKommentar;
            ViewBag.xaxelK_månad = månadKommentar;
            ViewBag.yaxelR_månad = månadAntalReview;
            ViewBag.xaxelR_månad = månadReview;


            return View();
        }

        
        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public IActionResult ReportedComments()
        {

            var reportedComments = _context.ReportedComments
                .GroupBy(rc => rc.CommentId)
                .Select(x => new ReportedCommentViewModel
                {
                    reviewId = _context.Comment.Where(c => c.Id == x.Key).Select(c => c.reviewId).First(),
                    reviewIdNested = _context.Comment.Where(c => c.Id == x.Key).Select(c => c.reviewIdNested).First(),
                    commentId = _context.Comment.Where(c => c.Id == x.Key).Select(c => c.commentId).First(),
                    ReportedCommentId = x.Key,
                    CommentText = _context.Comment.Where(c => c.Id == x.Key).Select(c => c.Text).First(),
                    IsDeleted = _context.Comment.Where(c => c.Id == x.Key).Select(c => c.IsDeleted).First(),
                    Total = x.Count()

                })
                .OrderByDescending(x => x.Total);

            var comments = _context.Comment
                                   .Include(c => c.comment)
                                   .Select(c => c.reviewId)
                                   .Take(1);


            return View(reportedComments.ToList());
        }



        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public IActionResult ReportedReviews()
        {

            var reportedReviews = _context.ReportedReviews
                .GroupBy(rr => rr.ReviewId)
                .Select(x => new ReportedReviewViewModel
                {
                    ReviewId = x.Key,
                    ReviewName = _context.Review.Where(r => r.Id == x.Key).Select(r => r.Title).First(),
                    IsDeleted = _context.Review.Where(r => r.Id == x.Key).Select(r => r.IsDeleted).First(),
                    Total = x.Count()

                })
                .OrderByDescending(x => x.Total);


            return View(reportedReviews.ToList());
        }


        [HttpPost]
        [Authorize(Roles ="Moderator, Admin")]
        public async Task<IActionResult> PurgeReportedReviews(int id)
        {   
            var reportedReview = _context.ReportedReviews.Where(a => a.ReviewId == id);

            _context.ReportedReviews.RemoveRange(reportedReview);
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("ReportedReviews", "Administration");
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> PurgeReportedComments(int id)
        {
            var reportedReview = _context.ReportedComments.Where(a => a.CommentId == id);

            _context.ReportedComments.RemoveRange(reportedReview);
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("ReportedComments", "Administration");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RevokeDeletedBook()
        {
            var books = await _context.Book
                .Include(b => b.CoolBooksUser)
                .Where(b => b.IsDeleted == true)
                .ToListAsync();


            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> RevokeDeletedReview()
        {
            var review = await _context.Review
                .Include(b => b.CoolBooksUser)
                .Where(b => b.IsDeleted == true)
                .ToListAsync();


            return View(review);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> RevokeDeletedComment()
        {
            var comments = await _context.Comment
                .Include(b => b.CoolBooksUser)
                .Where(b => b.IsDeleted == true)
                .ToListAsync();


            return View(comments);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RevokeDeletedAuthor()
        {
            var authors = await _context.Author
                .Include(b => b.CoolBooksUser)
                .Where(b => b.IsDeleted == true)
                .ToListAsync();

            return View(authors);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RevokeDeletedBookGenre()
        {
            var genres = await _context.Genre
                .Include(b => b.CoolBooksUser)
                .Where(b => b.IsDeleted == true)
                .ToListAsync();

            return View(genres);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> RevokeDeletedQuiz()
        {
            var quizzes = await _context.Quiz
                .Include(b => b.CoolBooksUser)
                .Where(b => b.IsDeleted == true)
                .ToListAsync();

            return View(quizzes);
        }
    }
}
