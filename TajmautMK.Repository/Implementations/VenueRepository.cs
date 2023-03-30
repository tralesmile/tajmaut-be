using Microsoft.EntityFrameworkCore;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class VenueRepository : IVenueRepository
    {
        private readonly tajmautDataContext _context;
        private readonly IHelperValidationClassService _helper;

        public VenueRepository(tajmautDataContext context, IHelperValidationClassService helper)
        {
            _context = context;
            _helper = helper;
        }

        // gets all restaurants

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            var check = await _context.Venues.ToListAsync();
            if (check.Count() > 0)
            {
                return check;
            }

            throw new CustomError(404, "No Data found!");

        }
        // filter restaurants by city
        public async Task<List<Venue>> FilterVenuesByCity(string city)
        {
            var cityVenues = await _context.Venues
            .Where(e => e.City == city)
            .ToListAsync();

            if (cityVenues.Count() > 0)
            {
                return cityVenues;
            }

            throw new CustomError(404, $"No data found");
        }
        // get restaurant by it's ID
        public async Task<Venue> GetVenuesIdAsync(int venueId)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(r => r.VenueId == venueId);
            if (venue == null)
            {
                throw new CustomError(404, $"Venue not found");
            }

            return venue;
        }
        // creates restaurant
        public async Task<Venue> CreateVenueAsync(VenuePostREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            return new Venue
            {
                VenueTypeId = request.VenueTypeId,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ModifiedBy = currentUserID,
                CreatedBy = currentUserID,
                ManagerId = currentUserID,
            };
        }
        // updates restaurant
        public async Task<Venue> UpdateVenueAsync(VenuePutREQUEST request, int VenueId)
        {
            var venue = await _context.Venues.FindAsync(VenueId);
            if (venue != null)
            {
                return venue;
            }
            throw new CustomError(404, $"Venue not found");
        }
        // saves changes to restraunt
        public async Task<Venue> SaveChanges(Venue venue, VenuePutREQUEST request)
        {
            var currentUserID = _helper.GetMe();

            venue.Name = request.Name;
            venue.Email = request.Email;
            venue.Phone = request.Phone;
            venue.City = request.City;
            venue.Address = request.Address;
            venue.ModifiedBy = currentUserID;
            venue.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return venue;
        }
        // deletes restaurant
        public async Task<Venue> DeleteVenueAsync(int VenueId)
        {
            // check's if restaurant exists 
            var check = await _context.Venues.FirstOrDefaultAsync(venue => venue.VenueId == VenueId);
            if (check != null)
            {
                return check;
            }
            throw new CustomError(404, "Venue not found!");

        }

        // delete's restaurant from DB
        public async Task<Venue> DeleteVenueDB(Venue getVenue)
        {
            _context.Venues.Remove(getVenue);

            await _context.SaveChangesAsync();

            return getVenue;
        }
        // saves & updates restaurant in the DB
        public async Task<Venue> SaveUpdatesVenueDB(Venue getVenue, VenuePutREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            getVenue.VenueTypeId=request.VenueTypeId;
            getVenue.Name = request.Name;
            getVenue.Email = request.Email;
            getVenue.Address = request.Address;
            getVenue.City = request.City;
            getVenue.Phone = request.Phone;
            getVenue.ModifiedBy = currentUserID;
            getVenue.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return getVenue;
        }
        // adds restaurant to DB
        public async Task<Venue> AddVenueToDB(Venue getResult)
        {
            _context.Venues.Add(getResult);
            await _context.SaveChangesAsync();

            return getResult;
        }
        // get restaurant by it's id
        public async Task<Venue> CheckVenueId(int venueId)
        {
            return await _context.Venues.FirstOrDefaultAsync(venue => venue.VenueId == venueId);
        }

        public async Task<bool> CheckVenueTypeId(int venueTypeId)
        {
            var check = await _context.VenueTypes.FirstOrDefaultAsync(v=>v.Venue_TypesId == venueTypeId);
            if(check != null)
            {
                return true;
            }

            throw new CustomError(404, $"Venue type not found");
        }

        public async Task<List<Venue_Types>> GetAllVenueTypes()
        {
            var venueTypes = await _context.VenueTypes.ToListAsync();

            if(venueTypes.Count()>0)
            {
                return venueTypes;
            }

            throw new CustomError(404, $"No venue Types found");
        }

        public async Task<List<Venue>> GetAllVenuesByVenueTypeID(int id)
        {
            var check = await _context.Venues.Where(v => v.VenueTypeId == id).ToListAsync();
            if(check.Count()>0)
            {
                return check;
            }

            throw new CustomError(404, $"No venues found!");

        }
    }
}