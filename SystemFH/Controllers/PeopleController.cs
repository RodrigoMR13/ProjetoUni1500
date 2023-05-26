using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemFH.Data;

namespace SystemFH.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var data = await _context.People
                .Include(p => p.Circle)
                .ToListAsync();

            return View(data);
        }

        public async Task<ActionResult> PartialIndex()
        {
            var data = await _context.People
                .Include(p => p.Circle)
                .ToListAsync();

            return PartialView("_PartialIndexPeople", data);
        }

        public async Task<IActionResult> Aluno()
        {
            var dataAluno = await _context.People
                .Where(x => x.Type == TypePerson.Mentorado)
                .Include(p => p.Circle)
                .ToListAsync();

            return View("Index", dataAluno);
        }

        public async Task<IActionResult> Professor()
        {
            var dataProfessor = await _context.People
                .Where(x => x.Type == TypePerson.Mentor)
                .Include(p => p.Circle)
                .ToListAsync();

            return View("Index", dataProfessor);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Circle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        //public async Task<ActionResult> Search(int pageNumber = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int totalCount = _context.People.Count();
        //    int correctNumber = (pageNumber - 1) * pageSize;

        //    var data = await _context.People
        //        .Where(x => x.Circle.Name.Contains(searchValue.ToLower())
        //                    || x.Type.ToString().Contains(searchValue.ToLower())
        //                    || x.Name.Contains(searchValue.ToLower())
        //                    || x.Email.Contains(searchValue.ToLower())
        //                    || x.Phone.Contains(searchValue.ToLower()))
        //        .Skip(correctNumber)
        //        .Take(pageSize)
        //        .Include(p => p.Circle)
        //        .ToListAsync();

        //    var viewModel = new PaginationViewModel<Person>
        //    {
        //        Items = data,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalCount = totalCount
        //    };

        //    return PartialView("_PartialIndexPeople", viewModel);
        //}

        // GET: People/Create

        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name");
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CircleId,Type,Name,CPF,Email,Phone,DateBorn,NivelStudy,University,GraduateDate,DateRegister,Enterprise,Recommendation,IsStudying")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.DateRegister = DateTime.Now;

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", person.CircleId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", person.CircleId);
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CircleId,Type,Name,CPF,Email,Phone,DateBorn,NivelStudy,University," +
            "GraduateDate,Enterprise,Recommendation,IsStudying")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userPerson = _context.People.FirstOrDefault(x => x.Id == id);

                    userPerson.DateRegister = DateTime.Now;
                    userPerson.CircleId = person.CircleId;
                    userPerson.Type = person.Type;
                    userPerson.Name = person.Name;
                    userPerson.CPF = person.CPF;
                    userPerson.Email = person.Email;
                    userPerson.Phone = person.Phone;
                    userPerson.DateBorn = person.DateBorn;
                    userPerson.NivelStudy = person.NivelStudy;
                    userPerson.University = person.University;
                    userPerson.GraduateDate = person.GraduateDate;
                    userPerson.Enterprise = person.Enterprise;
                    userPerson.Recommendation = person.Recommendation;
                    userPerson.IsStudying = person.IsStudying;
                    userPerson.UserName = person.Name;

                    _context.Update(userPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", person.CircleId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Circle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
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
                    var people = await _context.People.FindAsync(id);
                    _context.People.Remove(people);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
