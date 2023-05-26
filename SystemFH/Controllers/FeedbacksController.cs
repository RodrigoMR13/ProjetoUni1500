using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemFH.Data;
using SystemFH.Models;

namespace SystemFH.Controllers
{
    [Authorize]
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var data = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .Include(f => f.PersonFeedbacks)
                .ThenInclude(f => f.Person)
                .ToListAsync();

            return View(data);
        }

        public async Task<ActionResult> PartialIndex()
        {
            var data = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .Include(f => f.PersonFeedbacks)
                .ThenInclude(f => f.Person)
                .ToListAsync();

            return PartialView("_PartialIndexFeedbacks", data);
        }

        //public async Task<ActionResult> Search(int pageNumber = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int totalCount = _context.Feedbacks.Count();
        //    int correctNumber = (pageNumber - 1) * pageSize;

        //    var data = await _context.Feedbacks
        //        .Where(x => x.Date.ToString().Contains(searchValue.ToLower())
        //                    || x.Circle.Name.Contains(searchValue.ToLower())
        //                    || x.Theme.Name.Contains(searchValue.ToLower())
        //                    || x.StudentPerson.Name.Contains(searchValue.ToLower())
        //                    || x.TeacherPerson.Name.Contains(searchValue.ToLower())
        //                    || x.OportunityLearning.ToString().Contains(searchValue.ToLower())
        //                    || x.Note.ToString().Contains(searchValue.ToLower())
        //                    || x.Comment.ToString().Contains(searchValue.ToLower()))
        //        .Skip(correctNumber)
        //        .Take(pageSize)
        //        .Include(f => f.Circle)
        //        .Include(f => f.Theme)
        //        .Include(f => f.PersonFeedbacks)
        //        .ThenInclude(f => f.Person)
        //        .ToListAsync();

        //    var viewModel = new PaginationViewModel<Feedback>
        //    {
        //        Items = data,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalCount = totalCount
        //    };

        //    return PartialView("_PartialIndexFeedbacks", viewModel);
        //}

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            return View();
        }

        // POST: Feedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,CircleId,ThemeId,OportunityLearning,Note,Comment,StudentId,TeacherId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();

                PersonFeedback Student = new PersonFeedback();
                Student.Type = TypePerson.Mentorado;
                Student.FeedbackId = feedback.Id;
                Student.PersonId = feedback.StudentId;

                PersonFeedback Teacher = new PersonFeedback();
                Teacher.Type = TypePerson.Mentor;
                Teacher.FeedbackId = feedback.Id;
                Teacher.PersonId = feedback.TeacherId;

                _context.Add(Student);
                _context.Add(Teacher);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name", feedback.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name", feedback.TeacherId);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name", feedback.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name", feedback.TeacherId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CircleId,ThemeId,OportunityLearning,Note,Comment,StudentId,TeacherId")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var feedbacks = await _context.PeopleFeedback
                        .Where(x => x.FeedbackId == feedback.Id)
                        .ToListAsync();

                    foreach (var i in feedbacks)
                    {
                        if (i.Type == TypePerson.Mentor)
                        {
                            i.PersonId = feedback.TeacherId;
                        }
                        else
                        {
                            i.PersonId = feedback.StudentId;
                        }
                    }

                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name", feedback.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name", feedback.TeacherId);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("MultipleDelete")]
        public async Task<IActionResult> MultipleDeleteConfirmed(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var feedback = await _context.Feedbacks.FindAsync(id);
                    _context.Feedbacks.Remove(feedback);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
