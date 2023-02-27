using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using tajmautAPI.Models;

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
        public DbSet<Restaurant> Restaurants { get; set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<CategoryEvent> CategoryEvents { get; set; }
        public DbSet<OnlineReservation> OnlineReservations { get; set; }





    }

}
