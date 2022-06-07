using grouptest.Data;
using grouptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grouptest.Controllers
{
    public class StudioController : Controller
    {
        ApplicationDbContext applicationDbContext;

        public StudioController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var studio = applicationDbContext.Studios.Select(s => s).ToList();
            return View(studio);
        }

        public IActionResult Delete(int id)
        {
            Studio? studio = applicationDbContext.Studios.FirstOrDefault(m => m.StudioNumber == id);
            if (studio != null)
            {
                applicationDbContext.Remove(studio);
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
        public async Task<IActionResult> Create(Studio studio)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.Studios.Add(studio);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.Clear();
            return View(studio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await applicationDbContext.Studios.FindAsync(id);
            if (studio == null)
            {
                return NotFound();
            }
            return View(studio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Studio studio)
        {
            if (id != studio.StudioNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(studio);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudioExists(studio.StudioNumber))
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
            return View(studio);
        }

        private bool StudioExists(int id)
        {
            return applicationDbContext.Studios.Any(e => e.StudioNumber == id);
        }
    }
}
