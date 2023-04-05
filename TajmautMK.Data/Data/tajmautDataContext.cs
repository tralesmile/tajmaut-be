using Microsoft.EntityFrameworkCore;
using tajmautAPI.Models.EntityClasses;
using TajmautMK.Common.Models.EntityClasses;

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
        public DbSet<Venue_Types> VenueTypes { get; set; }
        public DbSet<ForgotPassEntity> ForgotPassEntity { get; set; }
        public DbSet<Venue_City> Venue_Cities { get; set; }

    }

}
