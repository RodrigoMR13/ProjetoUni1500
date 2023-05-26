using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SystemFH.Data;
using SystemFH.Models;

namespace SystemFH.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var data = await _context.Project
                .Include(p => p.TypeConsultor)
                .ToListAsync();

            return View(data);
        }

        public async Task<ActionResult> PartialIndex()
        {
            var data = await _context.Project
                .Include(p => p.TypeConsultor)
                .ToListAsync();

            return PartialView("_PartialIndexProjects", data);
        }

        //public async Task<ActionResult> Search(int pageNumber = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int totalCount = _context.Project.Count();
        //    int correctNumber = (pageNumber - 1) * pageSize;
        //    var data = await _context.Project
        //        .Where(x => x.Name.Contains(searchValue.ToLower())
        //                    || x.Type.ToString().Contains(searchValue.ToLower())
        //                    || x.TypeConsultor.Name.Contains(searchValue.ToLower())
        //                    || x.Description.Contains(searchValue.ToLower())
        //                    || x.Enterprise.Contains(searchValue.ToLower())
        //                    || x.Duration.ToString().Contains(searchValue.ToLower())
        //                    || x.Value.ToString().Contains(searchValue.ToLower())
        //                    || x.StartDate.ToString().Contains(searchValue.ToLower())
        //                    || x.EndDate.ToString().Contains(searchValue.ToLower()))
        //        .Skip(correctNumber)
        //        .Take(pageSize)
        //        .Include(p => p.TypeConsultor)
        //        .ToListAsync();

        //    var viewModel = new PaginationViewModel<Project>
        //    {
        //        Items = data,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalCount = totalCount
        //    };

        //    return PartialView("_PartialIndexProjects", viewModel);
        //}

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.TypeConsultor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,TypeConsultorId,Description,Enterprise,Duration,Value,StartDate,EndDate,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                var consultor = await _context.TypeConsultors
                    .Where(x => x.Id == project.TypeConsultorId)
                    .FirstOrDefaultAsync();

                project.TypeConsultor = consultor;
                project.AttCalculos();

                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name", project.TypeConsultorId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name", project.TypeConsultorId);
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,TypeConsultorId,Description,Enterprise,Duration,Value,StartDate,EndDate,Status")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var consultor = await _context.TypeConsultors
                        .Where(x => x.Id == project.TypeConsultorId)
                        .FirstOrDefaultAsync();

                    project.TypeConsultor = consultor;

                    project.AttCalculos();

                    var oldProject = await _context.Project
                        .FirstOrDefaultAsync(x => x.Id == id);

                    oldProject.Value = project.Value;
                    oldProject.Name = project.Name;
                    oldProject.Type = project.Type;
                    oldProject.TypeConsultorId = project.TypeConsultorId;
                    oldProject.Description = project.Description;
                    oldProject.Enterprise = project.Enterprise;
                    oldProject.Duration = project.Duration;
                    oldProject.StartDate = project.StartDate;
                    oldProject.EndDate = project.EndDate;
                    oldProject.Status = project.Status;
                    oldProject.TypeConsultor = project.TypeConsultor;

                    _context.Update(oldProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultors, "Id", "Name", project.TypeConsultorId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.TypeConsultor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
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
                    var project = await _context.Project.FindAsync(id);
                    _context.Project.Remove(project);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
