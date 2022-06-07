using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grouptest.Data;
using grouptest.Models;

namespace grouptest.Controllers
{
    public class CastController : Controller
    {
        ApplicationDbContext _context;

        public CastController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cast = from a in _context.CastMembers
                       join b in _context.Actors
                       on a.ActorNumber equals b.ActorNumber
                       join c in _context.DvdTitles
                       on a.DvdNumber equals c.DvdNumber

                       select new CastMember
                       {
                           Id = a.Id,
                           ActorNumber = b.ActorNumber,
                           ActorSurname = b.ActorSurname,
                           DvdNumber = c.DvdNumber,
                           DvdTitleName = c.DvdTitleName
                       };
            return View(cast);
        }

        public IActionResult Create()
        {
            ViewData["DvdTitleName"] = new SelectList(_context.DvdTitles, "DvdNumber", "DvdTitleName");
            ViewData["ActorSurname"] = new SelectList(_context.Actors, "ActorNumber", "ActorSurname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CastMember castMember)
        {
            castMember.ActorNumber = int.Parse(castMember.ActorSurname);
            castMember.DvdNumber = int.Parse(castMember.DvdTitleName);
            if (ModelState.IsValid)
            {
                _context.Add(castMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(castMember);
        }

        public IActionResult Delete(int id)
        {
            CastMember? castMember = _context.CastMembers.FirstOrDefault(m => m.Id == id);
            if (castMember != null)
            {
                _context.Remove(castMember);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMember = await _context.CastMembers.FindAsync(id);
            if (castMember == null)
            {
                return NotFound();
            }
            var cast = from a in _context.CastMembers
                       join b in _context.Actors
                       on a.ActorNumber equals b.ActorNumber
                       join c in _context.DvdTitles
                       on a.DvdNumber equals c.DvdNumber
                       where a.Id == castMember.Id
                       select new CastMember
                       {
                           Id = a.Id,
                           ActorNumber = b.ActorNumber,
                           ActorSurname = b.ActorSurname,
                           DvdNumber = c.DvdNumber,
                           DvdTitleName = c.DvdTitleName
                       };

            castMember = cast.FirstOrDefault(); 
            ViewData["DvdTitleName"] = new SelectList(_context.DvdTitles, "DvdNumber", "DvdTitleName");
            ViewData["ActorSurname"] = new SelectList(_context.Actors, "ActorNumber", "ActorSurname");

            return View(castMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CastMember castMember)
        {
            if (id != castMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    castMember.ActorNumber = int.Parse(castMember.ActorSurname);
                    castMember.DvdNumber = int.Parse(castMember.DvdTitleName);
                    _context.Update(castMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CastMemberExists(castMember.Id))
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
            ViewData["ActorSurname"] = new SelectList(_context.Actors, "ActorNumber", "ActorSurname");
            return View(castMember);
        }

        private bool CastMemberExists(int id)
        {
            return _context.CastMembers.Any(e => e.Id == id);
        }
    }
}
