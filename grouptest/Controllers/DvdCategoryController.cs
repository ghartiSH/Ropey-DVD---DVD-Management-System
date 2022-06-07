using grouptest.Data;
using grouptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grouptest.Controllers
{
    public class DvdCategoryController : Controller
    {
        ApplicationDbContext applicationDbContext;

        public DvdCategoryController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var dvdCat = applicationDbContext.DvdCategories.Select(s => s).ToList();
            return View(dvdCat);
        }

        public IActionResult Delete(int id)
        {
            DvdCategory? dvdCat = applicationDbContext.DvdCategories.FirstOrDefault(m => m.CategoryNumber == id);
            if (dvdCat != null)
            {
                applicationDbContext.Remove(dvdCat);
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
        public async Task<IActionResult> Create(DvdCategory dvdCategory)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.DvdCategories.Add(dvdCategory);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.Clear();
            return View(dvdCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dvdCat = await applicationDbContext.DvdCategories.FindAsync(id);
            if (dvdCat == null)
            {
                return NotFound();
            }
            return View(dvdCat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DvdCategory dvdCategory)
        {
            if (id != dvdCategory.CategoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(dvdCategory);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DvdCategoryExists(dvdCategory.CategoryNumber))
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
            return View(dvdCategory);
        }

        private bool DvdCategoryExists(int id)
        {
            return applicationDbContext.DvdCategories.Any(e => e.CategoryNumber == id);
        }
    }
}
