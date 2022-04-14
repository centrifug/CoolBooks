﻿using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoolBooks.Controllers
{
    
    [Authorize(Roles = "Admin")]
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
        public IActionResult CreateRole()
        {    
            return View();
        }

        [HttpPost]
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
        public IActionResult ListRoles()
        {

            var roles = roleManager.Roles.ToList();

            return View(roles);
        }

        [HttpPost]
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

                if (await userManager.IsInRoleAsync(user,role.Name))
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
                    if (i < (model.Count -1))
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
            return View();
        }

    }

}
