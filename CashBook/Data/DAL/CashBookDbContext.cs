using CashBook.DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CashBook.Data.DAL
{
    public class CashBookDbContext:IdentityDbContext
    {
        public CashBookDbContext()
        {

        }

        public CashBookDbContext(DbContextOptions<CashBookDbContext> options):base(options){
        }
        public DbSet<Entry> Entries { get; set; }
        
    }
}
