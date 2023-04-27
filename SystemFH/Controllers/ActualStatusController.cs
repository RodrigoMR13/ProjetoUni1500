using System.Collections.Generic;
using System;
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
    public class ActualStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActualStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActualStatus
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var data = await _context.ActualStatus
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .ToListAsync();

            int totalCount = data.Count();
            int correctNumber = (pageNumber - 1) * pageSize;
   
            data = await _context.ActualStatus
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .ToListAsync();

            var viewModel = new PaginationViewModel<ActualStatus> 
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
            var data = await _context.ActualStatus
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .ToListAsync();

            int totalCount = data.Count();
            int correctNumber = (pageNumber - 1) * pageSize;

            data = await _context.ActualStatus
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .ToListAsync();

            var viewModel = new PaginationViewModel<ActualStatus>
            {
                Items = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return PartialView("_PartialIndexActualStatus", viewModel);
        }

        // GET: ActualStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualStatus = await _context.ActualStatus
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualStatus == null)
            {
                return NotFound();
            }

            return View(actualStatus);
        }

        // GET: ActualStatus/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name");
            return View();
        }

        // POST: ActualStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CircleId,ProjectId,TypeObject,TypeConsultorId,Description,TimePlanned," +
            "PersonId,Sprint")] ActualStatus actualStatus)
        {
            if (ModelState.IsValid)
            {
                var projeto = await _context.Project
                    .Where(x => x.Id == actualStatus.ProjectId)
                    .FirstOrDefaultAsync();

                var consultor = await _context.TypeConsultors
                    .Where(x => x.Id == actualStatus.TypeConsultorId)
                    .FirstOrDefaultAsync();

                actualStatus.Project = projeto;
                actualStatus.TypeConsultor = consultor;

                actualStatus.AttCalculos();

                _context.Add(actualStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", actualStatus.CircleId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", actualStatus.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name", actualStatus.ProjectId);
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name", actualStatus.TypeConsultorId);
            return View(actualStatus);
        }

        // GET: ActualStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualStatus = await _context.ActualStatus.FindAsync(id);
            if (actualStatus == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", actualStatus.CircleId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", actualStatus.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name", actualStatus.ProjectId);
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name", actualStatus.TypeConsultorId);
            return View(actualStatus);
        }

        // POST: ActualStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CircleId,ProjectId,TypeObject,TypeConsultorId,Description,TimePlanned," +
            "PersonId,RealTime,Delivered,Sprint")] ActualStatus actualStatus)
        {
            if (id != actualStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var projeto = await _context.Project
                    .Where(x => x.Id == actualStatus.ProjectId)
                    .FirstOrDefaultAsync();

                    var consultor = await _context.TypeConsultors
                        .Where(x => x.Id == actualStatus.TypeConsultorId)
                        .FirstOrDefaultAsync();

                    actualStatus.Project = projeto;
                    actualStatus.TypeConsultor = consultor;

                    actualStatus.AttCalculos();

                    _context.Update(actualStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActualStatusExists(actualStatus.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", actualStatus.CircleId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", actualStatus.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name", actualStatus.ProjectId);
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name", actualStatus.TypeConsultorId);
            return View(actualStatus);
        }

        // GET: ActualStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualStatus = await _context.ActualStatus
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualStatus == null)
            {
                return NotFound();
            }

            return View(actualStatus);
        }

        // POST: ActualStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actualStatus = await _context.ActualStatus.FindAsync(id);
            _context.ActualStatus.Remove(actualStatus);
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
                    var actualStatus = await _context.ActualStatus.FindAsync(id);
                    _context.ActualStatus.Remove(actualStatus);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ActualStatusExists(int id)
        {
            return _context.ActualStatus.Any(e => e.Id == id);
        }
    }
}
