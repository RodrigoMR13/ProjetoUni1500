using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemFH.Data;
using SystemFH.Models;

namespace SystemFH.Controllers
{
    [Authorize]
    public class TypeConsultorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeConsultorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeConsultors
        public async Task<IActionResult> Index()
        {
            var data = await _context.TypeConsultors
                .ToListAsync();

            return View(data);
        }

        public async Task<ActionResult> PartialIndex()
        {
            var data = await _context.TypeConsultors
                .ToListAsync();

            return PartialView("_PartialIndexTypeConsultors", data);
        }

        //public async Task<ActionResult> Search(int pageNumber = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int totalCount = _context.TypeConsultors.Count();
        //    int correctNumber = (pageNumber - 1) * pageSize;
        //    var data = await _context.TypeConsultors
        //        .Where(x => x.Name.Contains(searchValue.ToLower())
        //                    || x.Fee.ToString().Contains(searchValue.ToLower()))
        //        .Skip(correctNumber)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    var viewModel = new PaginationViewModel<TypeConsultor>
        //    {
        //        Items = data,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalCount = totalCount
        //    };

        //    return PartialView("_PartialIndexTypeConsultors", viewModel);
        //}

        // GET: TypeConsultors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeConsultor = await _context.TypeConsultors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeConsultor == null)
            {
                return NotFound();
            }

            return View(typeConsultor);
        }

        // GET: TypeConsultors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeConsultors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Fee")] TypeConsultor typeConsultor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeConsultor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeConsultor);
        }

        // GET: TypeConsultors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeConsultor = await _context.TypeConsultors.FindAsync(id);
            if (typeConsultor == null)
            {
                return NotFound();
            }
            return View(typeConsultor);
        }

        // POST: TypeConsultors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Fee")] TypeConsultor typeConsultor)
        {
            if (id != typeConsultor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeConsultor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeConsultorExists(typeConsultor.Id))
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
            return View(typeConsultor);
        }

        // GET: TypeConsultors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeConsultor = await _context.TypeConsultors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeConsultor == null)
            {
                return NotFound();
            }

            return View(typeConsultor);
        }

        // POST: TypeConsultors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeConsultor = await _context.TypeConsultors.FindAsync(id);
            _context.TypeConsultors.Remove(typeConsultor);
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
                    var typeConsultors = await _context.TypeConsultors.FindAsync(id);
                    _context.TypeConsultors.Remove(typeConsultors);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool TypeConsultorExists(int id)
        {
            return _context.TypeConsultors.Any(e => e.Id == id);
        }
    }
}
