using grouptest.Data;
using grouptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grouptest.Controllers
{
    public class ProducerController : Controller
    {
        ApplicationDbContext applicationDbContext;

        public ProducerController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var prod = applicationDbContext.Producers.Select(s => s).ToList();
            return View(prod);
        }

        public IActionResult Delete(int id)
        {
            Producer? prod = applicationDbContext.Producers.FirstOrDefault(m => m.ProducerNumber == id);
            if (prod != null)
            {
                applicationDbContext.Remove(prod);
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
        public async Task<IActionResult> Create(Producer producer)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.Producers.Add(producer);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.Clear();
            return View(producer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prod = await applicationDbContext.Producers.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (id != producer.ProducerNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(producer);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.ProducerNumber))
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
            return View(producer);
        }

        private bool ProducerExists(int id)
        {
            return applicationDbContext.Producers.Any(e => e.ProducerNumber == id);
        }
    }
}
