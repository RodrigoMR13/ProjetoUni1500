using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemFH.Models;
using SystemFH.Data;

namespace SystemFH.Controllers
{
    [Authorize]
    public class DayTimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DayTimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DayTimes
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var data = await _context.DayTime
                .Include(d => d.ActualStatus)
                .ToListAsync();

            int totalCount = data.Count;
            int correctNumber = (pageNumber - 1) * pageSize;

            data = await _context.DayTime
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(d => d.ActualStatus)
                .ToListAsync();

            var viewModel = new PaginationViewModel<DayTime>
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
            var data = await _context.DayTime
                .Include(d => d.ActualStatus)
                .ToListAsync();

            int totalCount = data.Count;
            int correctNumber = (pageNumber - 1) * pageSize;

            data = await _context.DayTime
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(d => d.ActualStatus)
                .ToListAsync();

            var viewModel = new PaginationViewModel<DayTime>
            {
                Items = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return PartialView(viewModel);
        }

        // GET: DayTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTime = await _context.DayTime
                .Include(d => d.ActualStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayTime == null)
            {
                return NotFound();
            }

            return View(dayTime);
        }

        // GET: DayTimes/Create
        public IActionResult Create()
        {
            ViewData["ActualStatusId"] = new SelectList(_context.ActualStatus, "Id", "Description");
            return View();
        }

        // POST: DayTimes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActualStatusId,Data,RealTime,Delivered")] DayTime dayTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dayTime);
                await _context.SaveChangesAsync();

                var actualStatus = await _context.ActualStatus
                    .Where(x => x.Id == dayTime.ActualStatusId)
                    .Include(x => x.Project)
                    .Include(x => x.TypeConsultor)
                    .Include(x => x.DayTimes)
                    .FirstOrDefaultAsync();

                actualStatus.AttCalculos();

                _context.Update(actualStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActualStatusId"] = new SelectList(_context.ActualStatus, "Id", "Description", dayTime.ActualStatusId);
            return View(dayTime);
        }

        // GET: DayTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTime = await _context.DayTime.FindAsync(id);
            if (dayTime == null)
            {
                return NotFound();
            }
            ViewData["ActualStatusId"] = new SelectList(_context.ActualStatus, "Id", "Description", dayTime.ActualStatusId);
            return View(dayTime);
        }

        // POST: DayTimes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActualStatusId,Data,RealTime,Delivered")] DayTime dayTime)
        {
            if (id != dayTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldDayTime = await _context.DayTime
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

                    _context.Update(dayTime);
                    await _context.SaveChangesAsync();

                    var actualStatus = await _context.ActualStatus
                        .Where(x => x.Id == oldDayTime.ActualStatusId)
                        .Include(x => x.Project)
                        .Include(x => x.TypeConsultor)
                        .Include(x => x.DayTimes)
                        .FirstOrDefaultAsync();

                    actualStatus.AttCalculos();

                    _context.Update(actualStatus);
                    await _context.SaveChangesAsync();

                    actualStatus = await _context.ActualStatus
                        .Where(x => x.Id == dayTime.ActualStatusId)
                        .Include(x => x.Project)
                        .Include(x => x.TypeConsultor)
                        .Include(x => x.DayTimes)
                        .FirstOrDefaultAsync();

                    actualStatus.AttCalculos();

                    _context.Update(actualStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayTimeExists(dayTime.Id))
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
            ViewData["ActualStatusId"] = new SelectList(_context.ActualStatus, "Id", "Description", dayTime.ActualStatusId);
            return View(dayTime);
        }

        // GET: DayTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTime = await _context.DayTime
                .Include(d => d.ActualStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayTime == null)
            {
                return NotFound();
            }

            return View(dayTime);
        }

        // POST: DayTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayTime = await _context.DayTime.FindAsync(id);
            _context.DayTime.Remove(dayTime);
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
                    var dayTime = await _context.DayTime.FindAsync(id);
                    _context.DayTime.Remove(dayTime);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool DayTimeExists(int id)
        {
            return _context.DayTime.Any(e => e.Id == id);
        }
    }
}
