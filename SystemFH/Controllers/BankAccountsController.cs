using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemFH.Models;
using SystemFH.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace SystemFH.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var data = await _context.BankAccounts
                .Include(b => b.CashManagers)
                .Include(b => b.Enterprise)
                .ToListAsync();

            data.ForEach(x => x.AttCalculos());

            return View(data);
        }

        public async Task<ActionResult> PartialIndex()
        {
            var data = await _context.BankAccounts
                .Include(b => b.CashManagers)
                .Include(b => b.Enterprise)
                .ToListAsync();

            data.ForEach(x => x.AttCalculos());

            return PartialView("_PartialIndexBankAccounts", data);
        }

        //public async Task<ActionResult> Search(int pageNumber = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int totalCount = _context.BankAccounts.Count();
        //    int correctNumber = (pageNumber - 1) * pageSize;

        //    var data = await _context.BankAccounts
        //        .Where(x => x.Name.Contains(searchValue.ToLower()))
        //        .Skip(correctNumber)
        //        .Take(pageSize)
        //        .Include(b => b.CashManagers)
        //        .Include(b => b.Enterprise)
        //        .ToListAsync();

        //    data.ForEach(x => x.AttCalculos());

        //    var viewModel = new PaginationViewModel<BankAccount>
        //    {
        //        Items = data,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalCount = totalCount
        //    };

        //    return PartialView("_PartialIndexBankAccounts", viewModel);
        //}

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name");
            return View();
        }

        // POST: BankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InitialBalance,ActualBalance,EnterpriseId")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {    
                _context.Add(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name", bankAccount.EnterpriseId);
            return View(bankAccount);
            
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name", bankAccount.EnterpriseId);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.Id))
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
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name", bankAccount.EnterpriseId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            _context.BankAccounts.Remove(bankAccount);
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
                    var bankAccount = await _context.BankAccounts.FindAsync(id);
                    _context.BankAccounts.Remove(bankAccount);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool BankAccountExists(int id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }
    }
}
