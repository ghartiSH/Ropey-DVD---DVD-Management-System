using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grouptest.Data;
using grouptest.Models;
using grouptest.Models.ViewModels;

namespace grouptest.Controllers
{
    public class LoanController : Controller
    {

        ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var loan = from a in _context.Loans
                       join b in _context.LoanTypes
                       on a.LoanTypeNumber equals b.LoanTypeNumber
                       join c in _context.DvdCopies
                       on a.CopyNumber equals c.CopyNumber
                       join d in _context.Members
                       on a.MemberNumber equals d.MemberNumber

                       select new Loan
                       {
                           LoanNumber = a.LoanNumber,
                           LoanTypeNumber = b.LoanTypeNumber,
                           LoanTypee = b.LoanTypee,
                           CopyNumber = c.CopyNumber,
                           MemberNumber = d.MemberNumber,
                           MemberLastname  = d.MemberLastname,
                           DateOut = a.DateOut,
                           DateDue = a.DateDue,
                           DateReturn = a.DateReturn
                       };
            return View(loan);
        }

        public IActionResult DisplayCharge(ChargeViewModel chargeViewModel)
        {
            return View(chargeViewModel);
        }

        public IActionResult Create()
        {
            ViewData["LoanTypee"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypee");
            ViewData["CopyNumber"] = new SelectList(_context.DvdCopies, "CopyNumber", "CopyNumber");
            ViewData["MemberLastname"] = new SelectList(_context.Members, "MemberNumber", "MemberLastname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loan loan)
        {
            loan.LoanTypeNumber = int.Parse(loan.LoanTypee);
            loan.MemberNumber = int.Parse(loan.MemberLastname);


            //Query 6 for showing the charges

            //finding the loantype
            var loanType = _context.LoanTypes.Where(x => x.LoanTypeNumber == loan.LoanTypeNumber).FirstOrDefault();

            //Adding the duration from loan type

            loan.DateDue = loan.DateOut.AddDays(loanType.LoanDuration);

           
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();

            }

            //calculating the charge
            var forCharge = from a in _context.Loans
                            join b in _context.DvdCopies
                            on a.CopyNumber equals b.CopyNumber
                            join c in _context.DvdTitles
                            on b.DvdNumber equals c.DvdNumber

                            //descending order for getting the last row at the first
                            orderby a.DateOut descending

                            select new ChargeViewModel
                            {
                                StandardCharge = c.StandardCharge,
                                PenaltyCharge = c.PenaltyCharge,
                                DvdTitleName = c.DvdTitleName,
                                DateDue = a.DateDue,
                                Message = "Please return the DVD on given due date to avoid the penalty charges",
                                TotalCharge = c.StandardCharge * loanType.LoanDuration
                            };
            var oneCharge = forCharge.First();//getting only first data
            return RedirectToAction("DisplayCharge", oneCharge);
        }

        public IActionResult Delete(int id)
        {
            Loan? loan = _context.Loans.FirstOrDefault(m => m.LoanNumber == id);
            if (loan != null)
            {
                _context.Remove(loan);
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

            var tloan = await _context.Loans.FindAsync(id);
            if (tloan == null)
            {
                return NotFound();
            }

            var loan = from a in _context.Loans
                       join b in _context.LoanTypes
                       on a.LoanTypeNumber equals b.LoanTypeNumber
                       join c in _context.DvdCopies
                       on a.CopyNumber equals c.CopyNumber
                       join d in _context.Members
                       on a.MemberNumber equals d.MemberNumber

                       where a.LoanNumber == tloan.LoanNumber
                       select new Loan
                       {
                           LoanNumber = a.LoanNumber,
                           LoanTypeNumber = b.LoanTypeNumber,
                           LoanTypee = b.LoanTypee,
                           CopyNumber = c.CopyNumber,
                           MemberNumber = d.MemberNumber,
                           MemberLastname = d.MemberLastname,
                           DateOut = a.DateOut,
                           DateDue = a.DateDue,
                           DateReturn = a.DateReturn
                       };


            tloan = loan.FirstOrDefault();

            ViewData["LoanTypee"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypee");
            ViewData["CopyNumber"] = new SelectList(_context.DvdCopies, "CopyNumber", "CopyNumber");
            ViewData["MemberLastname"] = new SelectList(_context.Members, "MemberNumber", "MemberLastname");
            return View(tloan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Loan loan)
        {
            if (id != loan.LoanNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    loan.LoanTypeNumber = int.Parse(loan.LoanTypee);
                    loan.MemberNumber = int.Parse(loan.MemberLastname);
                    _context.Update(loan);
                    await _context.SaveChangesAsync();

                    //calculating the extra price to pay
                    if (loan.DateReturn > loan.DateDue)
                    {
                       
                        int diff = (int) ((loan.DateReturn?? DateTime.Now) - (loan.DateDue ?? DateTime.Now)).TotalDays;

                        var forCharge = from a in _context.Loans.Where(l => l.LoanNumber == id)
                                        join b in _context.DvdCopies
                                        on a.CopyNumber equals b.CopyNumber
                                        join c in _context.DvdTitles
                                        on b.DvdNumber equals c.DvdNumber

                                        select new ChargeViewModel
                                        {
                                            StandardCharge = c.StandardCharge,
                                            PenaltyCharge = c.PenaltyCharge,
                                            DvdTitleName = c.DvdTitleName,
                                            DateDue = a.DateDue,
                                            DateReturn = a.DateReturn,
                                            Message = "You have been penalty charged",
                                            TotalCharge = c.PenaltyCharge * diff
                                        };
                        var toSendCharge = forCharge.First();
                        return RedirectToAction("DisplayCharge", toSendCharge);

                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanNumber))
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
            ViewData["LoanTypee"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypee");
            ViewData["CopyNumber"] = new SelectList(_context.DvdCopies, "CopyNumber", "CopyNumber");
            ViewData["MemberLastname"] = new SelectList(_context.Members, "MemberNumber", "MemberLastname");
            return View(loan);
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanNumber == id);
        }


    }
}
