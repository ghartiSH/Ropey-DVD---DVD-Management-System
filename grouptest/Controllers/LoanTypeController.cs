using grouptest.Data;
using grouptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grouptest.Controllers
{
    public class LoanTypeController : Controller
    {

        ApplicationDbContext applicationDbContext;

        public LoanTypeController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var loanType = applicationDbContext.LoanTypes.Select(s => s).ToList();
            return View(loanType);
        }

        public IActionResult Delete(int id)
        {
            LoanType? loanType = applicationDbContext.LoanTypes.FirstOrDefault(m => m.LoanTypeNumber == id);
            if (loanType != null)
            {
                applicationDbContext.Remove(loanType);
                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.LoanTypes.Add(loanType);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.Clear();
            return View(loanType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanType = await applicationDbContext.LoanTypes.FindAsync(id);
            if (loanType == null)
            {
                return NotFound();
            }
            return View(loanType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanType loanType)
        {
            if (id != loanType.LoanTypeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(loanType);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanTypeExists(loanType.LoanTypeNumber))
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
            return View(loanType);
        }

        private bool LoanTypeExists(int id)
        {
            return applicationDbContext.LoanTypes.Any(e => e.LoanTypeNumber == id);
        }
    }
}
