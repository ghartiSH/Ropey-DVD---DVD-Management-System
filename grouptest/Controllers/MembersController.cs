using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grouptest.Data;
using grouptest.Models;

namespace grouptest.Controllers
{
    public class MembersController : Controller
    {
        ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var applicationDbContext = _context.Members.Include(m => m.MembershipCategory);

            var members = from a in _context.Members
                          join b in _context.MembershipCategories
                          on a.MembershipID equals b.MembershipCategoryNumber

                          select new Member
                          {
                              MemberNumber = a.MemberNumber,
                              MemberFirstname = a.MemberFirstname,
                              MemberLastname = a.MemberLastname,
                              MemberAddress = a.MemberAddress,
                              MemberDob = a.MemberDob,
                              MembershipID = b.MembershipCategoryNumber,
                              MembershipName = b.MembershipCategoryDescription
                          };

            return View(members);
        }

        public IActionResult Create()
        {
            ViewData["MembershipName"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryDescription");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Member member)
        {
            member.MembershipID = int.Parse(member.MembershipName);
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            var members = from a in _context.Members
                          join b in _context.MembershipCategories
                          on a.MembershipID equals b.MembershipCategoryNumber

                          where a.MemberNumber == member.MemberNumber
                          select new Member
                          {
                              MemberNumber = a.MemberNumber,
                              MemberFirstname = a.MemberFirstname,
                              MemberLastname = a.MemberLastname,
                              MemberAddress = a.MemberAddress,
                              MemberDob = a.MemberDob,
                              MembershipID = b.MembershipCategoryNumber,
                              MembershipName = b.MembershipCategoryDescription
                          };

            member = members.FirstOrDefault();   

            ViewData["MembershipName"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryDescription");
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (id != member.MemberNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    member.MembershipID = int.Parse(member.MembershipName);
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberNumber))
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
            ViewData["MembershipName"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryDescription");
            return View(member);
        }


        public IActionResult Delete(int id)
        {
            Member? member = _context.Members.FirstOrDefault(m => m.MemberNumber == id);
            if (member != null)
            {
                _context.Remove(member);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberNumber == id);
        }

    }
}
