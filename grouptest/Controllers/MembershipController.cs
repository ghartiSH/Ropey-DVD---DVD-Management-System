using grouptest.Data;
using grouptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grouptest.Controllers
{
    public class Membership : Controller
    {
        ApplicationDbContext applicationDbContext;

        public Membership(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var membership = applicationDbContext.MembershipCategories.Select(s => s).ToList();
            return View(membership);
        }

        public IActionResult Delete(int id)
        {
            MembershipCategory? membership = applicationDbContext.MembershipCategories.FirstOrDefault(m => m.MembershipCategoryNumber == id);
            if (membership !=null)
            {
                applicationDbContext.Remove(membership);
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
        public  async Task<IActionResult> Create(MembershipCategory membershipCategory)
        {
            if (ModelState.IsValid)
            {
                MembershipCategory membership = new MembershipCategory();
                membership.MembershipCategoryDescription = membershipCategory.MembershipCategoryDescription;
                membership.MembershipCategoryTotalLoans = membershipCategory.MembershipCategoryTotalLoans;

                applicationDbContext.MembershipCategories.Add(membership);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.Clear();
            return View(membershipCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await applicationDbContext.MembershipCategories.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MembershipCategory membershipCategory)
        {
            if (id != membershipCategory.MembershipCategoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(membershipCategory);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membershipCategory.MembershipCategoryNumber))
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
            return View(membershipCategory);
        }

        private bool MembershipExists(int id)
        {
            return applicationDbContext.MembershipCategories.Any(e => e.MembershipCategoryNumber == id);
        }


    }
}
