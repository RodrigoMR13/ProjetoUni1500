using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemFH.Models;
using SystemFH.Data;
using System.Collections.Generic;
using System;

namespace SystemFH.Controllers
{
    [Authorize]
    public class CirclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CirclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Circles
        public async Task<IActionResult> Index()
        {
            var data = await _context.Circles
                .ToListAsync();

            return View(data);
        }

        public async Task<ActionResult> PartialIndex()
        {
            var data = await _context.Circles
                .ToListAsync();

            return PartialView("_PartialIndexCircles", data);
        }

        //public async Task<ActionResult> Search(int pageNumber = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int totalCount = _context.Circles.Count();
        //    int correctNumber = (pageNumber - 1) * pageSize;
        //    var data = await _context.Circles
        //        .Where(x => x.Name.Contains(searchValue.ToLower()))
        //        .Skip(correctNumber)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    var viewModel = new PaginationViewModel<Circle>
        //    {
        //        Items = data,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalCount = totalCount
        //    };

        //    return PartialView("_PartialIndexCircles", viewModel);
        //}

        // GET: Circles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // GET: Circles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Circles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Circle circle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(circle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(circle);
        }

        // GET: Circles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circles.FindAsync(id);
            if (circle == null)
            {
                return NotFound();
            }
            return View(circle);
        }

        // POST: Circles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Circle circle)
        {
            if (id != circle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(circle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircleExists(circle.Id))
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
            return View(circle);
        }

        // GET: Circles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var circle = await _context.Circles.FindAsync(id);
            _context.Circles.Remove(circle);
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
                    var circle = await _context.Circles.FindAsync(id);
                    _context.Circles.Remove(circle);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool CircleExists(int id)
        {
            return _context.Circles.Any(e => e.Id == id);
        }
    }
}
