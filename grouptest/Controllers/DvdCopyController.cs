using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grouptest.Data;
using grouptest.Models;

namespace grouptest.Controllers
{
    public class DvdCopyController : Controller
    {
        ApplicationDbContext _context;

        public DvdCopyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var dvdCopy = from a in _context.DvdCopies
                          join b in _context.DvdTitles
                          on a.DvdNumber equals b.DvdNumber

                          select new DvdCopy
                          {
                              CopyNumber = a.CopyNumber,
                              DvdNumber = b.DvdNumber,
                              DvdTitleName = b.DvdTitleName,
                              DatePurchased = a.DatePurchased,
                          };
            return View(dvdCopy);
        }

        public IActionResult Delete(int id)
        {
            DvdCopy? dvdCopy = _context.DvdCopies.FirstOrDefault(m => m.CopyNumber == id);
            if (dvdCopy != null)
            {
                _context.Remove(dvdCopy);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Create()
        {
            ViewData["DvdTitleName"] = new SelectList(_context.DvdTitles, "DvdNumber", "DvdTitleName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DvdCopy dvdCopy)
        {
            dvdCopy.DvdNumber = int.Parse(dvdCopy.DvdTitleName);
            if (ModelState.IsValid)
            {
                _context.Add(dvdCopy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dvdCopy);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dvdCopy = await _context.DvdCopies.FindAsync(id);
            if (dvdCopy == null)
            {
                return NotFound();
            }
            var dvd = from a in _context.DvdCopies
                          join b in _context.DvdTitles
                          on a.DvdNumber equals b.DvdNumber
                          where a.CopyNumber == dvdCopy.CopyNumber
                          select new DvdCopy
                          {
                              CopyNumber = a.CopyNumber,
                              DvdNumber = b.DvdNumber,
                              DvdTitleName = b.DvdTitleName,
                              DatePurchased = a.DatePurchased,
                          };


            dvdCopy = dvd.FirstOrDefault();

            ViewData["DvdTitleName"] = new SelectList(_context.DvdTitles, "DvdNumber", "DvdTitleName");
            return View(dvdCopy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DvdCopy dvdCopy)
        {
            if (id != dvdCopy.CopyNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dvdCopy.DvdNumber = int.Parse(dvdCopy.DvdTitleName);
                    _context.Update(dvdCopy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DvdCopyExists(dvdCopy.CopyNumber))
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
            ViewData["DvdTitleName"] = new SelectList(_context.DvdTitles, "DvdNumber", "DvdTitleName");
            return View(dvdCopy);
        }

        private bool DvdCopyExists(int id)
        {
            return _context.DvdCopies.Any(e => e.CopyNumber == id);
        }
    }
}
