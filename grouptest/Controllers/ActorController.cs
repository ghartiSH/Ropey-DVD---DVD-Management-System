using grouptest.Data;
using grouptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grouptest.Controllers
{
    public class ActorController : Controller
    {
        ApplicationDbContext applicationDbContext;

        public ActorController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var actor = applicationDbContext.Actors.Select(s => s).ToList();
            return View(actor);
        }

        public IActionResult Delete(int id)
        {
            Actor? actor = applicationDbContext.Actors.FirstOrDefault(m => m.ActorNumber == id);
            if (actor != null)
            {
                applicationDbContext.Remove(actor);
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
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.Actors.Add(actor);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.Clear();
            return View(actor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await applicationDbContext.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.ActorNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(actor);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ActorNumber))
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
            return View(actor);
        }

        private bool ActorExists(int id)
        {
            return applicationDbContext.Actors.Any(e => e.ActorNumber == id);
        }
    }
}
