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
    public class LearningsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LearningsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Learnings
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {

            int totalCount = _context.Learnings.Count();
            int correctNumber = (pageNumber - 1) * pageSize;

            var data = await _context.Learnings
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .Include(l => l.PeopleLearning)
                .ThenInclude(l => l.Person)
                .ToListAsync();

            var viewModel = new PaginationViewModel<Learning>
            {
                Items = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        public async Task<ActionResult> PartialIndex(int pageNumber = 1, int pageSize = 5)
        {

            int totalCount = _context.Learnings.Count();
            int correctNumber = (pageNumber - 1) * pageSize;

            var data = await _context.Learnings
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .Include(l => l.PeopleLearning)
                .ThenInclude(l => l.Person)
                .ToListAsync();

            var viewModel = new PaginationViewModel<Learning>
            {
                Items = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return PartialView("_PartialIndexLearnings", viewModel);
        }

        // GET: Learnings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learning == null)
            {
                return NotFound();
            }

            return View(learning);
        }

        // GET: Learnings/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            return View();
        }

        // POST: Learnings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CircleId,ThemeId,OportunityLearning,LearningAction,MeasurementDate,MeasurementForm," +
            "Result,Comment,Status,TeacherId,StudentId")] Learning learning)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learning);
                await _context.SaveChangesAsync();

                PersonLearning Student = new PersonLearning();
                Student.Type = TypePerson.Mentorado;
                Student.LearningId = learning.Id;
                Student.PersonId = learning.StudentId;

                PersonLearning Teacher = new PersonLearning();
                Teacher.Type = TypePerson.Mentor;
                Teacher.LearningId = learning.Id;
                Teacher.PersonId = learning.TeacherId;

                _context.Add(Student);
                _context.Add(Teacher);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name", learning.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name", learning.TeacherId);
            return View(learning);
        }

        // GET: Learnings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings.FindAsync(id);
            if (learning == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name", learning.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name", learning.TeacherId);
            return View(learning);
        }

        // POST: Learnings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CircleId,ThemeId,OportunityLearning,LearningAction,MeasurementDate," +
            "MeasurementForm,Result,Comment,Status,TeacherId,StudentId")] Learning learning)
        {
            if (id != learning.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aprendizados = await _context.PeopleLearning
                        .Where(x => x.LearningId == learning.Id)
                        .ToListAsync();

                    foreach(var i in aprendizados)
                    {
                        if (i.Type == TypePerson.Mentor)
                        {
                            i.PersonId = learning.TeacherId;
                        }
                        else
                        {
                            i.PersonId = learning.StudentId;
                        }
                    }

                    _context.Update(learning);
                    _context.Update(aprendizados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningExists(learning.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);

            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name", learning.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name", learning.TeacherId);
            return View(learning);
        }

        // GET: Learnings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learning == null)
            {
                return NotFound();
            }

            return View(learning);
        }

        // POST: Learnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learning = await _context.Learnings.FindAsync(id);
            _context.Learnings.Remove(learning);
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
                    var learnings = await _context.Learnings.FindAsync(id);
                    _context.Learnings.Remove(learnings);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool LearningExists(int id)
        {
            return _context.Learnings.Any(e => e.Id == id);
        }
    }
}
