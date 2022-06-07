using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grouptest.Data;
using grouptest.Models;

namespace grouptest.Controllers
{
    public class DvdTitleController : Controller
    {

        ApplicationDbContext _context;

        CastController? _castController;

        public DvdTitleController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var dvdTitle = from a in _context.DvdTitles
                           join b in _context.Studios
                           on a.StudioNumber equals b.StudioNumber
                           join c in _context.Producers
                           on a.ProducerNumber equals c.ProducerNumber
                           join d in _context.DvdCategories
                           on a.CategoryNumber equals d.CategoryNumber
                           select new DvdTitle
                           {
                               DvdNumber = a.DvdNumber,
                               DvdTitleName = a.DvdTitleName,
                               StudioNumber = b.StudioNumber,
                               StudioName = b.StudioName,
                               ProducerNumber = c.ProducerNumber,
                               ProducerName = c.ProducerName,
                               CategoryNumber = d.CategoryNumber,
                               CategoryName = d.CategoryDescription,
                               DateReleased = a.DateReleased,
                               StandardCharge = a.StandardCharge,
                               PenaltyCharge = a.PenaltyCharge,
                           };
            return View(dvdTitle);
        }

        public IActionResult Create()
        {
            ViewData["StudioName"] = new SelectList(_context.Studios, "StudioNumber", "StudioName");
            ViewData["ProducerName"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName");
            ViewData["CategoryName"] = new SelectList(_context.DvdCategories, "CategoryNumber", "CategoryDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DvdTitle dvdTitle)
        {
            dvdTitle.StudioNumber = int.Parse(dvdTitle.StudioName);
            dvdTitle.ProducerNumber = int.Parse(dvdTitle.ProducerName);
            dvdTitle.CategoryNumber = int.Parse(dvdTitle.CategoryName);

            if (ModelState.IsValid)
            {
                _context.Add(dvdTitle);
                await _context.SaveChangesAsync();

                //for function 9
                //on everey new DVD title creation redirecting to create a new cast member row
                return RedirectToAction("Create", "Cast");
            }     
            return View(dvdTitle);
        }

        public IActionResult Delete(int id)
        {
            DvdTitle? dvdTitle = _context.DvdTitles.FirstOrDefault(m => m.DvdNumber == id);
            if (dvdTitle != null)
            {
                _context.Remove(dvdTitle);
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

            var dvdTitle = await _context.DvdTitles.FindAsync(id);
            if (dvdTitle == null)
            {
                return NotFound();
            }

            var dvd = from a in _context.DvdTitles
                           join b in _context.Studios
                           on a.StudioNumber equals b.StudioNumber
                           join c in _context.Producers
                           on a.ProducerNumber equals c.ProducerNumber
                           join d in _context.DvdCategories
                           on a.CategoryNumber equals d.CategoryNumber

                           where a.DvdNumber == dvdTitle.DvdNumber
                           select new DvdTitle
                           {
                               DvdNumber = a.DvdNumber,
                               DvdTitleName = a.DvdTitleName,
                               StudioNumber = b.StudioNumber,
                               StudioName = b.StudioName,
                               ProducerNumber = c.ProducerNumber,
                               ProducerName = c.ProducerName,
                               CategoryNumber = d.CategoryNumber,
                               CategoryName = d.CategoryDescription,
                               DateReleased = a.DateReleased,
                               StandardCharge = a.StandardCharge,
                               PenaltyCharge = a.PenaltyCharge,
                           };


            dvdTitle = dvd.FirstOrDefault();

            ViewData["StudioName"] = new SelectList(_context.Studios, "StudioNumber", "StudioName");
            ViewData["ProducerName"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName");
            ViewData["CategoryName"] = new SelectList(_context.DvdCategories, "CategoryNumber", "CategoryDescription");
            return View(dvdTitle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DvdTitle dvdTitle)
        {
            if (id != dvdTitle.DvdNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dvdTitle.StudioNumber = int.Parse(dvdTitle.StudioName);
                    dvdTitle.ProducerNumber = int.Parse(dvdTitle.ProducerName);
                    dvdTitle.CategoryNumber = int.Parse(dvdTitle.CategoryName);
                    _context.Update(dvdTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DvdExists(dvdTitle.DvdNumber))
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
            ViewData["StudioName"] = new SelectList(_context.Studios, "StudioNumber", "StudioName");
            ViewData["ProducerName"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName");
            ViewData["CategoryName"] = new SelectList(_context.DvdCategories, "CategoryNumber", "CategoryDescription");
            return View(dvdTitle);
        }

        private bool DvdExists(int id)
        {
            return _context.DvdTitles.Any(e => e.DvdNumber == id);
        }
    }
}
