using DVDRetal.Entity;
using Microsoft.EntityFrameworkCore;

namespace DVDRetal.DataBase
{
    public class DVDContext:DbContext
    {


        public DVDContext(DbContextOptions<DVDContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<DVD> DVDs { get; set; }



    }
}
