using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Services;

namespace CoolBooks.Controllers
{
    public class AccountController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;

        public AccountController(IWebHostEnvironment hostEnvironment, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager, CoolBooksContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;            
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new CoolBooksUser { 
                    UserName = model.Email,
                    Email= model.Email,
                    DOB = model.DOB,
                    Name = model.Name    };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {            

            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }                    
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(CoolBooksUser updatedUser)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            return View(await userManager.GetUserAsync(User));
        }
        [HttpGet]
        public async Task<IActionResult> ProfileEdit(int id)
        {
            var user = await userManager.GetUserAsync(User);
            EditUserViewModel model = new EditUserViewModel
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    DOB = user.DOB,
                    ImagePath = user.ImagePath,
                    PhoneNumber = user.PhoneNumber

                };
            return View(model);        

        }
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(EditUserViewModel updatedUser)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            if (updatedUser.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, updatedUser.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    //StatusMessage = "Unexpected error when trying to set phone number.";
                    //return RedirectToPage();
                }
            }

            if (updatedUser.Name != user.Name)
            {
                user.Name = updatedUser.Name;
            }

            if (updatedUser.DOB != user.DOB)
            {
                user.DOB = updatedUser.DOB;
            }
            //save/update image
            if (updatedUser.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(updatedUser.ImageFile.FileName);
                string extension = Path.GetExtension(updatedUser.ImageFile.FileName);


                fileName = user.Id + extension; //name it after the booksId

                string path = Path.Combine(wwwRootPath + "/Images/Users/", fileName);


                //ta bort den gamla bilden
                if (System.IO.File.Exists(Path.Combine(wwwRootPath, user.ImagePath)))
                {
                    System.IO.File.Delete(Path.Combine(wwwRootPath, user.ImagePath));
                }

                //spara bilden
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await updatedUser.ImageFile.CopyToAsync(fileStream);
                }

                //update imagepath (kanske inte nödvändig.. men for now!)
                user.ImagePath = Path.Combine("/Images/Users/", fileName);
            }

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Profile));
        }               
        
            
        [HttpGet]
        public async Task<IActionResult> ProfileReviews(CoolBooksUser updatedUser)
        {
            var user = await userManager.GetUserAsync(User);

            string userid = user.Id;

            var reviews = _context.Review
                                  .Where(r => r.CreatedBy == userid)
                                  .ToList();

            

            return View(reviews);
        }
        [HttpGet]
        public async Task<IActionResult> ProfileMinaQuiz(CoolBooksUser updatedUser)
        {
            var user = await userManager.GetUserAsync(User);

            string userid = user.Id;

            var quiz = _context.Quiz
                                  .Where(r => r.CreatedBy == userid)
                                  .ToList();



            return View(quiz);
        }
        [HttpGet]
        public async Task<IActionResult> ProfileMinaQuizTaken(CoolBooksUser updatedUser)
        {
            var user = await userManager.GetUserAsync(User);

            string userid = user.Id;

            var quiz = _context.QuizTaken   
                               .Include(q => q.Quiz)
                               .Where(r => r.CreatedBy == userid)
                               .ToList();

            return View(quiz);
        }
    }
}
