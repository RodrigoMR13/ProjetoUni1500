using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemFH.Models;
using SystemFH.Data;
using System.Collections.Generic;
using System;

namespace SystemFH.Controllers
{
    [Authorize]
    public class CashManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CashManagers
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            int totalCount = _context.CashManager.Count();
            int correctNumber = (pageNumber - 1) * pageSize;

            var data = await _context.CashManager
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(c => c.Account)
                .Include(c => c.BankAccount)
                .Include(c => c.Category)
                .Include(c => c.CostCenter)
                .Include(c => c.Enterprise)
                .Include(c => c.Person)
                .ToListAsync();

            var viewModel = new PaginationViewModel<CashManager>
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
            int totalCount = _context.CashManager.Count();
            int correctNumber = (pageNumber - 1) * pageSize;

            var data = await _context.CashManager
                .Skip(correctNumber)
                .Take(pageSize)
                .Include(c => c.Account)
                .Include(c => c.BankAccount)
                .Include(c => c.Category)
                .Include(c => c.CostCenter)
                .Include(c => c.Enterprise)
                .Include(c => c.Person)
                .ToListAsync();

            var viewModel = new PaginationViewModel<CashManager>
            {
                Items = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return PartialView("_PartialIndexCashManager", viewModel);
        }

        // GET: CashManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashManager = await _context.CashManager
                .Include(c => c.Account)
                .Include(c => c.BankAccount)
                .Include(c => c.Category)
                .Include(c => c.CostCenter)
                .Include(c => c.Enterprise)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cashManager == null)
            {
                return NotFound();
            }

            return View(cashManager);
        }

        // GET: CashManagers/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name");
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.ClientProject, "Id", "Name");
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name");
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name");
            return View();
        }

        // POST: CashManagers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RealizationDate,CashDate,CashMonth,CompetMonth,AccMonth,EnterpriseId,CostCenterId," +
            "AccountId,PersonId,BankAccountId,CategoryId,Description,Value,Comp,BankBalance,EnterpriseBalance,Type")] CashManager cashManager)
        {
            if (ModelState.IsValid)
            {
                cashManager.AttCalculos();
                _context.Add(cashManager);
                await _context.SaveChangesAsync();

                var bankAccount = _context.BankAccounts
                    .Where(x => x.Id == cashManager.BankAccountId)
                    .Include(x => x.CashManagers)
                    .FirstOrDefault();

                var othersAccounts = _context.BankAccounts
                    .Where(x => x.EnterpriseId == bankAccount.EnterpriseId)
                    .Include(x => x.CashManagers)
                    .ToList();

                bankAccount.AttCalculos();
                othersAccounts.ForEach(x => x.AttCalculos());

                cashManager.BankBalance = bankAccount.ActualBalance;
                cashManager.EnterpriseBalance = othersAccounts.Sum(x => x.ActualBalance);

                _context.Update(bankAccount);
                _context.Update(cashManager);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", cashManager.AccountId);
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name", cashManager.BankAccountId);
            ViewData["CategoryId"] = new SelectList(_context.ClientProject, "Id", "Name", cashManager.CategoryId);
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name", cashManager.CostCenterId);
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name", cashManager.EnterpriseId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", cashManager.PersonId);
            return View(cashManager);
        }

        // GET: CashManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashManager = await _context.CashManager.FindAsync(id);
            if (cashManager == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", cashManager.AccountId);
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name", cashManager.BankAccountId);
            ViewData["CategoryId"] = new SelectList(_context.ClientProject, "Id", "Name", cashManager.CategoryId);
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name", cashManager.CostCenterId);
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name", cashManager.EnterpriseId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", cashManager.PersonId);
            return View(cashManager);
        }

        // POST: CashManagers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RealizationDate,CashDate,CashMonth,CompetMonth,AccMonth,EnterpriseId,CostCenterId," +
            "AccountId,PersonId,BankAccountId,CategoryId,Description,Value,Comp,BankBalance,EnterpriseBalance,Type")] CashManager cashManager)
        {
            if (id != cashManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashManager);
                    await _context.SaveChangesAsync();

                    cashManager.AttCalculos();

                    var bankAccount = _context.BankAccounts
                    .Where(x => x.Id == cashManager.BankAccountId)
                    .Include(x => x.CashManagers)
                    .FirstOrDefault();

                    var othersAccounts = _context.BankAccounts
                        .Where(x => x.EnterpriseId == bankAccount.EnterpriseId)
                        .Include(x => x.CashManagers)
                        .ToList();

                    bankAccount.AttCalculos();
                    othersAccounts.ForEach(x => x.AttCalculos());

                    cashManager.BankBalance = bankAccount.ActualBalance;
                    cashManager.EnterpriseBalance = othersAccounts.Sum(x => x.ActualBalance);

                    _context.Update(bankAccount);
                    _context.Update(cashManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashManagerExists(cashManager.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", cashManager.AccountId);
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name", cashManager.BankAccountId);
            ViewData["CategoryId"] = new SelectList(_context.ClientProject, "Id", "Name", cashManager.CategoryId);
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name", cashManager.CostCenterId);
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprise, "Id", "Name", cashManager.EnterpriseId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", cashManager.PersonId);
            return View(cashManager);
        }

        [HttpPost, ActionName("FinalizarTransição")]
        public async Task<IActionResult> FinishTransition(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var cashManager = await _context.CashManager.FindAsync(id);
                    cashManager.Comp = true;
                    cashManager.CashDate = DateTime.Now;
                    cashManager.CashMonth = DateTime.Now;

                    _context.CashManager.Update(cashManager);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: CashManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashManager = await _context.CashManager
                .Include(c => c.Account)
                .Include(c => c.BankAccount)
                .Include(c => c.Category)
                .Include(c => c.CostCenter)
                .Include(c => c.Enterprise)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cashManager == null)
            {
                return NotFound();
            }

            return View(cashManager);
        }

        // POST: CashManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashManager = await _context.CashManager.FindAsync(id);
            _context.CashManager.Remove(cashManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashManagerExists(int id)
        {
            return _context.CashManager.Any(e => e.Id == id);
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("MultipleDelete")]
        public async Task<IActionResult> MultipleDeleteConfirmed(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var cashManager = await _context.CashManager.FindAsync(id);
                    _context.CashManager.Remove(cashManager);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
