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

            var coolBooksContext = _context.Quiz
                .Include(q => q.CoolBooksUser)
                .Where(q => q.Questions.Count > 0 && q.IsDeleted != true);
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

            var quiz = await _context.Quiz
                .Include(x => x.Questions)
                .ThenInclude(q => q.Options)
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                return NotFound();
            }

            if (userManager.GetUserId(User) == quiz.CreatedBy || User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                EditQuizViewModel vm = new EditQuizViewModel();

                vm.QuizId = quiz.Id;
                vm.QuizName = quiz.Name;
                vm.IsDeleted = quiz.IsDeleted;
                vm.Questions = quiz.Questions;

                return View(vm);
            }

            return RedirectToAction("Index", "Quiz");
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditQuizViewModel inputQuiz)
        {
            if (id != inputQuiz.QuizId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var quizToUpdate = _context.Quiz.Find(id);

                quizToUpdate.Name = inputQuiz.QuizName;
                quizToUpdate.IsDeleted = inputQuiz.IsDeleted;
                quizToUpdate.LastUpdated = DateTime.Now;
                quizToUpdate.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                try
                {
                    _context.Update(quizToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quizToUpdate.Id))
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
            
            return View(inputQuiz);
        }


        // GET: Quiz/EditQuestion/5
        public async Task<IActionResult> EditQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Question = await _context.Question
                .Include(q => q.Options)
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (Question == null)
            {
                return NotFound();
            }

            if (userManager.GetUserId(User) == Question.CreatedBy || User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                EditQuizQuestionViewModel vm = new EditQuizQuestionViewModel();

                vm.QuestionId = Question.Id;
                vm.QuestionText = Question.Text;

                foreach (var option in Question.Options)
                {
                    EditQuestionOptionViewModel vmOption = new EditQuestionOptionViewModel();
                    vmOption.Text = option.Text;
                    vmOption.Answer = option.Answer;
                    vm.Options.Add(vmOption);
                }                

                return View(vm);
            }

            return RedirectToAction("Index", "Quiz");
        }

        // POST: Quiz/EditQuestion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(int id, EditQuizQuestionViewModel inputQuestion, string? returnUrl)
        {
            if (inputQuestion.Answer == 0)
            {
                ModelState.AddModelError("Answer", "Rättsvar behövs!");
                return View(inputQuestion);
            }

            if (id != inputQuestion.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var questionToUpdate = _context.Question
                    .Include(q => q.Options)
                    .Where(q => q.Id == id)
                    .FirstOrDefault();

                questionToUpdate.Text = inputQuestion.QuestionText;
                questionToUpdate.LastUpdated = DateTime.Now;
                questionToUpdate.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                for (int i = 0; i < questionToUpdate.Options.Count; i++)
                {
                    questionToUpdate.Options[i].Text = inputQuestion.Options[i].Text;
                    questionToUpdate.Options[i].LastUpdated = DateTime.Now;
                    questionToUpdate.Options[i].UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    questionToUpdate.Options[i].Answer = false;

                    //plus ett för att ocheckad box = 0. Radiobutton values="i+1" i view
                    if ((i + 1) == inputQuestion.Answer)
                    {
                        questionToUpdate.Options[i].Answer = true;
                    }
                }

                try
                {
                    _context.Update(questionToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(questionToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }

            return View(inputQuestion);
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
            quiz.IsDeleted = true;
            _context.Quiz.Update(quiz);
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

        // POST: Quiz/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Correct(int id, Dictionary<int, int> answer)
        {

            //kolla så att du svarat på minst en fråga)
            if (answer.Count <= 0)
            {
                return RedirectToAction("Take", new {id = id});
            }

            //hämta quizet
            var quiz = _context.Quiz
                .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .Where(q => q.Id == id)
                .FirstOrDefault();

            int score = 0;

            if (quiz == null)
            {
                return NotFound();
            }

            CorrectedQuizViewModel vm = new CorrectedQuizViewModel();


            foreach (var question in quiz.Questions)
            {
                CorrectedQuestionViewModel q = new CorrectedQuestionViewModel();

                //hämta rätt svar
                var questionAnswer = question.Options.Where(o => o.Answer == true).First();

                //kolla om usernsvarat på frågan
                if (answer.ContainsKey(question.Id))
                {
                    //hämta vad som usern svarat
                    var userAnswer = answer.Where(a => a.Key == question.Id).Select(a => a.Value).First();

                    //kolla att om usern svarat rätt
                    if (questionAnswer.Id == userAnswer)
                    {
                        score++;
                        q.IsCorrect = true;
                    }
                    q.UserAnswer = question.Options.Where(o => o.Id == userAnswer).Select(o => o.Text).First();
                }
                else
                {
                    q.UserAnswer = "OBESVARAD";
                }
                

                //spara frågan till viewmodel          

                q.Name = question.Text;
                q.CorrectAnswer = questionAnswer.Text;
                

                vm.Quiz.Add(q);

            }

            vm.QuizName = quiz.Name;
            vm.Score = score;
            vm.NumberOfQuestions = quiz.Questions.Count;


            //spara att användaren har gjort quizet

            var userTakenQuiz = _context.QuizTaken
                .Where(qt => qt.QuizID == id && qt.CreatedBy == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault();

            if (userTakenQuiz == null)
            {
                QuizTaken quizTaken = new QuizTaken();

                quizTaken.QuizID = id;
                quizTaken.Score = score;
                quizTaken.MaxScore = quiz.Questions.Count;
                quizTaken.Created = DateTime.Now;
                quizTaken.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Add(quizTaken);
                _context.SaveChanges();

                vm.QuizAlreadyTaken = false;
                return View(vm);
            }
            else
            {
                vm.QuizAlreadyTaken = true;
                return View(vm);
            }


            
        }

    }
}
