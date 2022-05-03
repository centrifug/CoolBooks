#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Data;
using CoolBooks.Models.Quiz;
using CoolBooks.ViewModels;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CoolBooks.Controllers
{
    public class QuizController : Controller
    {
        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;
        public QuizController(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Quiz
        public async Task<IActionResult> Index(int? pageNumber)
        {       

            var coolBooksContext = _context.Quiz.Include(q => q.CoolBooksUser);
            int pageSize = 4;

            return View(await PaginatedList<Quiz>.CreateAsync(coolBooksContext.AsNoTracking(),pageNumber ?? 1,pageSize));
        }

        public async Task<IActionResult> Take(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.CoolBooksUser)
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }


        // GET: Quiz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.CoolBooksUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quiz/Create
        [Authorize]
        public IActionResult Create()
        {

            CreateQuizViewModel vm = new CreateQuizViewModel();

            var quizGenres = _context.QuizGenre.ToList();

            foreach (QuizGenre genre in quizGenres)
            {
                QuizGenreViewModel QuizGenre = new QuizGenreViewModel
                {
                    GenreId = genre.Id,
                    GenreName = genre.Name,
                    IsSelected = false
                };

                vm.Genres.Add(QuizGenre);
            }

            return View(vm);
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuizViewModel quizInput)
        {
            //Kolla så att minst en genre är vald
            var selectedGenres = quizInput.Genres.Where(g => g.IsSelected == true).Count();
            if ( selectedGenres == 0)
            {
                ModelState.AddModelError("Genres", "Du måste välja minst en genre");
                return View(quizInput);
            }

                if (ModelState.IsValid)
            {
                Quiz quizToCreate = new Quiz();

                quizToCreate.Name = quizInput.Name;
                quizToCreate.Created = DateTime.Now;
                quizToCreate.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (var genre in quizInput.Genres)
                {
                    if (genre.IsSelected)
                    {
                        QuizGenre quizGenre = new QuizGenre { Id = genre.GenreId };
                        _context.QuizGenre.Attach(quizGenre);
                        quizToCreate.QuizGenres.Add(quizGenre);
                    }
                }


                _context.Add(quizToCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddQuestion", new { QuizId = quizToCreate.Id});
            }
            
            return View(quizInput);
        }

        // GET: Quiz/AddQuestion
        public IActionResult AddQuestion(int QuizId)
        {
           AddQuestionViewModel vm = new AddQuestionViewModel();

            vm.QuizId = QuizId;
           
           return View(vm);
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(AddQuestionViewModel vmInput)
        {
            //Kolla så att ett rättsvar är checked
            if (vmInput.Answer == 0)
            {
                ModelState.AddModelError("Answer", "Rättsvar behövs!");
                return View(vmInput);
            }

            if (ModelState.IsValid)
            {
                Question questionToCreate = new Question();

                for (int i = 0; i < vmInput.Options.Count; i++)
                {
                    if (vmInput.Options[i].Text != null)
                    {
                        QOption o =  new QOption();

                        o.Text = vmInput.Options[i].Text;
                        o.Created = DateTime.Now;
                        o.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        //plus ett för att ocheckad box = 0. Radiobutton values="i+1" i view
                        if((i+1) == vmInput.Answer)
                        {
                            o.Answer = true;
                        }

                        questionToCreate.Options.Add(o);
                    }
                }

                questionToCreate.Text = vmInput.Text;
                questionToCreate.Created = DateTime.Now;
                questionToCreate.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                questionToCreate.QuizId = vmInput.QuizId;

                _context.Add(questionToCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddQuestion", new AddQuestionViewModel { QuizId = vmInput.QuizId });
            }

            return View(vmInput);
        }



        // GET: Quiz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", quiz.CreatedBy);
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,Created,CreatedBy,UpdatedBy,LastUpdated")] Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", quiz.CreatedBy);
            return View(quiz);
        }

        // GET: Quiz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.CoolBooksUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);
            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.Id == id);
        }


        // GET: Genres/Create
        [Authorize(Roles = "Admin, moderator")]
        public IActionResult CreateGenre()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre(CreateQuizGenreViewModel genreInput)
        {

            if (ModelState.IsValid)
            {
                QuizGenre genreToCreate = new QuizGenre();

                var user = await userManager.GetUserAsync(User);

                genreToCreate.Name = genreInput.Name;
                genreToCreate.Created = DateTime.Now;
                genreToCreate.CreatedBy = user.Id;

                _context.Add(genreToCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genreInput);
        }


    }
}
