using grouptest.Data;
using grouptest.Models;
using grouptest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace grouptest.Controllers
{
    public class MainController : Controller
    {
        ApplicationDbContext _context;

        public MainController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult FunctionOneView(DvdViewModel functionOneViewModel)
        {
            int actID = int.Parse(functionOneViewModel.ActorSurname);
            var viewModel = from a in _context.CastMembers
                            join b in _context.Actors
                            on a.ActorNumber equals b.ActorNumber
                            join c in _context.DvdTitles
                            on a.DvdNumber equals c.DvdNumber

                            where a.ActorNumber == actID
                            select new DvdViewModel
                            {
                                ActorNumber = b.ActorNumber,
                                ActorSurname = b.ActorSurname,
                                DvdNumber = c.DvdNumber,
                                DvdTitleName = c.DvdTitleName,
                                DateReleased = c.DateReleased
                            };
            return View(viewModel);
        }

        public IActionResult FunctionOne()
        {
            ViewData["ActorSurname"] = new SelectList(_context.Actors, "ActorNumber", "ActorSurname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FunctionOne(DvdViewModel functionOneViewModel)
        {
            int actorId = int.Parse(functionOneViewModel.ActorSurname);

            return RedirectToAction("FunctionOneView", functionOneViewModel);
          
        }

        public IActionResult FunctionTwoView(DvdViewModel functionTwoViewModel)
        {
            int actID = int.Parse(functionTwoViewModel.ActorSurname);

            //var loans = _context.Loans.Select(x => x.CopyNumber).ToArray();

            var viewModel = (from a in _context.CastMembers.Where(x => x.ActorNumber == actID)
                            join b in _context.DvdTitles
                            on a.DvdNumber equals b.DvdNumber
                            join c in _context.DvdCopies
                            on a.DvdNumber equals c.DvdNumber
                            group c by new { a.DvdNumber, b.DvdTitleName, a.ActorNumber} into table3

                            select new DvdViewModel
                            {
                                DvdNumber = table3.Key.DvdNumber,
                                DvdTitleName = table3.Key.DvdTitleName,
                                NoOfCopies = table3.Count(),
                                ActorNumber = table3.Key.ActorNumber,
                                
                            }).ToList();
    

            return View(viewModel);
        }

        public IActionResult FunctionTwo()
        {
            ViewData["ActorSurname"] = new SelectList(_context.Actors, "ActorNumber", "ActorSurname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FunctionTwo(DvdViewModel functionTwoViewModel)
        {
            int actorId = int.Parse(functionTwoViewModel.ActorSurname);

            return RedirectToAction("FunctionTwoView", functionTwoViewModel);

        }

        public IActionResult FunctionFourView()
        {

            

            var viewModel = from a in _context.CastMembers
                            join b in _context.Actors
                            on a.ActorNumber equals b.ActorNumber
                            join c in _context.DvdTitles
                            on a.DvdNumber equals c.DvdNumber
                            join d in _context.Producers
                            on c.ProducerNumber equals d.ProducerNumber
                            join e in _context.Studios
                            on c.StudioNumber equals e.StudioNumber
                            orderby c.DateReleased, b.ActorSurname


                            select new DvdViewModel
                            {
                                ActorNumber = b.ActorNumber,
                                ActorSurname = b.ActorSurname,
                                DvdNumber = c.DvdNumber,
                                DvdTitleName = c.DvdTitleName,
                                DateReleased = c.DateReleased,
                                ProducerNumber = d.ProducerNumber,
                                StudioNumber = e.StudioNumber,
                                ProducerName = d.ProducerName,
                                StudioName = e.StudioName,
                            };
            
                            
            return View(viewModel);
        }

        public IActionResult FunctionThreeView(Member member)
        {
            int memberId = member.MemberNumber;

            //var loans = _context.Loans.Select(x => x.CopyNumber).ToArray();

            var viewModel = (from a in _context.Loans.Where(x => x.MemberNumber == memberId && x.DateOut >= DateTime.Today.AddDays(-31))
                             join b in _context.Members
                             on a.MemberNumber equals b.MemberNumber
                             join c in _context.DvdCopies
                             on a.CopyNumber equals c.CopyNumber
                             join d in _context.DvdTitles
                             on c.DvdNumber equals d.DvdNumber
                             select new LoanModelView
                             {
                                 LoanNumber = a.LoanNumber,
                                 MemberNumber = b.MemberNumber,
                                 CopyNumber = c.CopyNumber,
                                 DvdTitleName = d.DvdTitleName,
                                 DateOut = a.DateOut,
                                 DateDue = a.DateDue,
                                 DateReturn = a.DateReturn,

                             }).ToList();


            return View(viewModel);
        }

        public IActionResult FunctionThree()
        {
            ViewData["MemberNumber"] = new SelectList(_context.Members, "MemberNumber", "MemberNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FunctionThree(Member member)
        {
            return RedirectToAction("FunctionThreeView", member);

        }

        public IActionResult FunctionFiveView(DvdCopy dvdCopy)
        {
            int dvdCopyNo = dvdCopy.CopyNumber;

            //var loans = _context.Loans.Select(x => x.CopyNumber).ToArray();

            var viewModel = (from a in _context.Loans.Where(x => x.CopyNumber == dvdCopyNo)
                             join b in _context.Members
                             on a.MemberNumber equals b.MemberNumber
                             join c in _context.DvdCopies
                             on a.CopyNumber equals c.CopyNumber
                             join d in _context.DvdTitles
                             on c.DvdNumber equals d.DvdNumber

                             orderby a.DateOut descending

                             select new LoanModelView
                             {
                                 LoanNumber = a.LoanNumber,
                                 MemberNumber = b.MemberNumber,
                                 MemberLastname = b.MemberLastname,
                                 CopyNumber = c.CopyNumber,
                                 DvdTitleName = d.DvdTitleName,
                                 DateOut = a.DateOut,
                                 DateDue = a.DateDue,
                                 DateReturn = a.DateReturn,

                             }).First();


            return View(viewModel);
        }

        public IActionResult FunctionFive()
        {
            ViewData["CopyNumber"] = new SelectList(_context.DvdCopies, "CopyNumber", "CopyNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FunctionFive(DvdCopy dvdCopy)
        {
            return RedirectToAction("FunctionFiveView", dvdCopy);

        }


        public IActionResult FunctionTen()
        {
            DateTime yearAgo = DateTime.Today.AddDays(-365);
            var expired = from a in _context.DvdCopies.Where(d => d.DatePurchased < yearAgo)
                              //join b in _context.Loans.Where(l => l.DateReturn == null)
                          select new DvdCopiesViewModel
                          {
                              CopyNumber = a.CopyNumber,
                              DatePurchased = a.DatePurchased
                          };

            return View(expired);
        }

        public IActionResult DeleteExpired(int id)
        {
            DvdCopy? dvd = _context.DvdCopies.FirstOrDefault(d => d.CopyNumber == id);
            if (dvd != null)
            {
                _context.Remove(dvd);
                _context.SaveChanges();
                return RedirectToAction("FunctionTen");
            }
            return View();
        }

        public IActionResult FunctionEleven()
        {
            var loanDetails = from a in _context.Loans.Where(z => z.DateReturn == null).OrderBy(x => x.DateOut)
                              join b in _context.Members
                              on a.MemberNumber equals b.MemberNumber
                              join c in _context.DvdCopies
                              on a.CopyNumber equals c.CopyNumber
                              join d in _context.DvdTitles.OrderBy(z => z.DvdTitleName)
                              on c.DvdNumber equals d.DvdNumber

                              group a by new {a.DateOut, a.LoanNumber, a.CopyNumber, b.MemberNumber, b.MemberLastname, d.DvdTitleName, a.DateDue, a.DateReturn}
                              into grp
                              select new LoanModelView
                              {
                                  LoanNumber = grp.Key.LoanNumber,
                                  CopyNumber = grp.Key.CopyNumber,
                                  MemberNumber = grp.Key.MemberNumber,
                                  MemberLastname = grp.Key.MemberLastname,
                                  DvdTitleName = grp.Key.DvdTitleName,
                                  DateDue = grp.Key.DateDue,
                                  DateOut = grp.Key.DateOut,
                                  DateReturn = grp.Key.DateReturn,
                                  TotalLoans = grp.Count(),
                              };
            return View(loanDetails);
        }

        public IActionResult FunctionTwelve()
        {
            DateTime monthAgo = DateTime.Today.AddDays(-30);
            var loanDetails = from a in _context.Loans.Where(z => z.DateOut < monthAgo).OrderBy(x => x.DateOut)
                              join b in _context.Members
                              on a.MemberNumber equals b.MemberNumber
                              join c in _context.DvdCopies
                              on a.CopyNumber equals c.CopyNumber
                              join d in _context.DvdTitles.OrderBy(z => z.DvdTitleName)
                              on c.DvdNumber equals d.DvdNumber

                              select new LoanModelView
                              {
                                  LoanNumber = a.LoanNumber,
                                  CopyNumber = c.CopyNumber,
                                  MemberNumber = b.MemberNumber,
                                  MemberLastname = b.MemberLastname,
                                  MemberFirstname = b.MemberFirstname,
                                  MemberAddress = b.MemberAddress,
                                  DvdTitleName = d.DvdTitleName,
                                  DateOut = a.DateOut,
                                  NoOfDays = (int) (DateTime.Today - a.DateOut).TotalDays,

                              };
            return View(loanDetails);
        }

        public IActionResult FunctionThirteen()
        {
            DateTime monthAgo = DateTime.Today.AddDays(-31);
            var loanDetails = from a in _context.DvdCopies
                              join b in _context.Loans.Where(x=> x.DateOut < monthAgo)
                              on a.CopyNumber equals b.CopyNumber
                              join c in _context.DvdTitles
                              on a.DvdNumber equals c.DvdNumber
                              
                              select new LoanModelView
                              {
                                  CopyNumber = a.CopyNumber,
                                  DvdTitleName = c.DvdTitleName,
                                  DateOut = b.DateOut,
                                  NoOfDays = (int)(DateTime.Today - b.DateOut).TotalDays,
                              };
            return View(loanDetails);
        }

        public IActionResult FunctionEight()
        {
            var loanDetails = from a in _context.Members
                              join b in _context.Loans
                              on a.MemberNumber equals b.MemberNumber
                              join c in _context.MembershipCategories
                              on a.MembershipID equals c.MembershipCategoryNumber

                              group b by new { a.MemberNumber,a.MemberFirstname, a.MemberLastname,a.MembershipID, c.MembershipCategoryTotalLoans} into grp
                             
                              select new LoanModelView
                              {
                                  MembershipCategoryTotalLoans = grp.Key.MembershipCategoryTotalLoans,
                                  MemberShipID = grp.Key.MembershipID,
                                  MemberNumber = grp.Key.MemberNumber,
                                  TotalLoans = grp.Count(),
                                  MemberLastname = grp.Key.MemberLastname,
                                  MemberFirstname = grp.Key.MemberFirstname,

                              };
            return View(loanDetails);
        }


    }
}
