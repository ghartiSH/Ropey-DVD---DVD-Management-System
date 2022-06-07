using grouptest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using grouptest.Models.ViewModels;

namespace grouptest.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<DvdCategory> DvdCategories { get; set; }

        public DbSet<LoanType> LoanTypes { get; set; }

        public DbSet<MembershipCategory> MembershipCategories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<User> Userss { get; set; }
        public DbSet<Member> Members { get; set; }

        public DbSet<DvdTitle> DvdTitles { get; set; }

        public DbSet<DvdCopy> DvdCopies { get; set; }
        public DbSet<Loan> Loans { get; set; }     

        public DbSet<CastMember> CastMembers { get; set; }

        public DbSet<grouptest.Models.ViewModels.ChargeViewModel> ChargeViewModel { get; set; }

        public DbSet<grouptest.Models.ViewModels.DvdCopiesViewModel> DvdCopiesViewModel { get; set; }

        public DbSet<grouptest.Models.ViewModels.LoanModelView> LoanModelView { get; set; }

       


    }


}
