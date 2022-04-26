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
            var kommentarIdag = _context.Comment
                                  .Where(c => c.Created >= startDateTime && 
                                              c.Created <= endDateTime)
                                  .Select(c => c.Text)
                                  .ToArray();

            DateTime startDateTimeWeek = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 00:00:00
            DateTime endDateTimeWeek = DateTime.Today.AddDays(-7).AddTicks(-1); //1 vecka - 1sekund
            var kommentarPerVecka = _context.Comment
                                  .Where(c => c.Created <= startDateTimeWeek &&
                                              c.Created >= endDateTimeWeek)
                                  .Select(c => c.Text)
                                  .ToArray();

            ViewBag.kommentarPerDag = kommentarIdag;
            ViewBag.kommentarPerVecka = kommentarPerVecka;
            //Kommentar statistik

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
                    ReportedCommentId = x.Key,
                    CommentText = _context.Comment.Where(c => c.Id == x.Key).Select(c => c.Text).First(),
                    Total = x.Count()

                })
                .OrderByDescending(x => x.Total);


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
                    ReviewName = _context.Review.Where(c => c.Id == x.Key).Select(c => c.Title).First(),
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
    }
}
