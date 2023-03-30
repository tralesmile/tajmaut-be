using Microsoft.EntityFrameworkCore;
using tajmautAPI.Models.EntityClasses;

namespace tajmautAPI.Data
{
    public partial class tajmautDataContext : DbContext
    {
        public tajmautDataContext()
        {

        }
        public tajmautDataContext(DbContextOptions<tajmautDataContext> options) : base(options) 
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Venue> Venues { get; set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<CategoryEvent> CategoryEvents { get; set; }
        public DbSet<OnlineReservation> OnlineReservations { get; set; }





    }

}
