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
        public async Task<IActionResult> Index()
        {
            var coolBooksContext = _context.Quiz.Include(q => q.CoolBooksUser);
            return View(await coolBooksContext.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuizViewModel quizInput)
        {
            if (ModelState.IsValid)
            {
                Quiz quizToCreate = new Quiz();

                quizToCreate.Name = quizInput.Name;
                quizToCreate.Created = DateTime.Now;
                quizToCreate.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
        public async Task<IActionResult> AddQuestion(AddQuestionViewModel vmInput, int answer)
        {
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

                        if(i == answer)
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
    }
}
