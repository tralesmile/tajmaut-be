using Microsoft.EntityFrameworkCore;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Data;
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

        // gets all venues

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            var check = await _context.Venues.Include(x=>x.Venue_City).Include(x=>x.VenueType).ToListAsync();
            if (check.Count() > 0)
            {
                return check;
            }

            throw new CustomError(404, "No Data found!");

        }
        // filter venues by city
        public async Task<List<Venue>> FilterVenuesByCity(string city)
        {
            var cityVenues = await _context.Venues
            .Include(e=>e.Venue_City)
            .Include(x=>x.VenueType)
            .Where(e => e.Venue_City.CityName == city)
            .ToListAsync();

            if (cityVenues.Count() > 0)
            {
                return cityVenues;
            }

            throw new CustomError(404, $"No data found");
        }
        // get venue by it's ID
        public async Task<Venue> GetVenuesIdAsync(int venueId)
        {
            var venue = await _context.Venues.Include(x => x.Venue_City).Include(x => x.VenueType).FirstOrDefaultAsync(r => r.VenueId == venueId);
            if (venue == null)
            {
                throw new CustomError(404, $"Venue not found");
            }

            return venue;
        }
        // creates venue
        public async Task<Venue> CreateVenueAsync(VenuePostREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var getVenueCity = await GetVenueCityById(request.Venue_CityId);

            return new Venue
            {
                VenueTypeId = request.VenueTypeId,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ModifiedBy = currentUserID,
                CreatedBy = currentUserID,
                ManagerId = currentUserID,
                Venue_CityId = request.Venue_CityId,
                VenueImage = request.VenueImage,
            };
        }
        // updates venue
        public async Task<Venue> UpdateVenueAsync(VenuePutREQUEST request, int VenueId)
        {
            var venue = await _context.Venues.FindAsync(VenueId);
            if (venue != null)
            {
                return venue;
            }
            throw new CustomError(404, $"Venue not found");
        }
        // saves changes to venue
        public async Task<Venue> SaveChanges(Venue venue, VenuePutREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var getVenueCity = await GetVenueCityById(request.Venue_CityId);

            venue.Name = request.Name;
            venue.Email = request.Email;
            venue.Phone = request.Phone;
            venue.Address = request.Address;
            venue.ModifiedBy = currentUserID;
            venue.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return venue;
        }
        // deletes venue
        public async Task<Venue> DeleteVenueAsync(int VenueId)
        {
            // check's if venue exists 
            var check = await _context.Venues.FirstOrDefaultAsync(venue => venue.VenueId == VenueId);
            if (check != null)
            {
                return check;
            }
            throw new CustomError(404, "Venue not found!");

        }

        // delete's venue from DB
        public async Task<Venue> DeleteVenueDB(Venue getVenue)
        {
            _context.Venues.Remove(getVenue);

            await _context.SaveChangesAsync();

            return getVenue;
        }
        // saves & updates venue in the DB
        public async Task<Venue> SaveUpdatesVenueDB(Venue getVenue, VenuePutREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var getVenueCity = await GetVenueCityById(request.Venue_CityId);

            getVenue.VenueTypeId=request.VenueTypeId;
            getVenue.Name = request.Name;
            getVenue.Email = request.Email;
            getVenue.Address = request.Address;
            getVenue.Phone = request.Phone;
            getVenue.ModifiedBy = currentUserID;
            getVenue.ModifiedAt = DateTime.Now;
            getVenue.VenueImage = request.VenueImage;

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
        // get venue by it's id
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

        public async Task<List<Venue_City>> GetAllVenueCities()
        {
            var getCities = await _context.Venue_Cities.ToListAsync();
            if(getCities.Count()>0)
            {
                return getCities;
            }

            throw new CustomError(404, $"No data found!");
        }

        public async Task<Venue_City> GetVenueCityById(int venueCityId)
        {
            var getCity = await _context.Venue_Cities.FirstOrDefaultAsync(x=>x.Venue_CityId== venueCityId);
            if(getCity!=null)
            {
                return getCity;
            }
            throw new CustomError(404, $"Venue city not found!");
        }

        public async Task<bool> CheckVenueCityId(int venueCityId)
        {
            var check = await _context.Venue_Cities.FindAsync(venueCityId);
            if(check!=null)
            {
                return true;
            }

            throw new CustomError(404, $"Venue city not found!");
        }

        public async Task<List<Venue>> VenuesFilter(VenueFilterREQUEST request)
        {
            if (request.CityId.HasValue || request.VenueTypeId.HasValue)
            {

                var selectedFilter = await _context.Venues
                    .Include(x => x.Venue_City)
                    .Include(x => x.VenueType)
                    .Where(x =>
                    (!request.CityId.HasValue || x.Venue_CityId == request.CityId) &&
                    (!request.VenueTypeId.HasValue || x.VenueTypeId == request.VenueTypeId))
                    .ToListAsync();

                if (selectedFilter.Count() > 0)
                {
                    return selectedFilter;
                }

            }
            else
            {
                var allVenues = await GetAllVenuesAsync();

                if (allVenues.Count() > 0)
                {
                    return allVenues;
                }

            }
            throw new CustomError(404, $"No venues found");
        }

        public async Task<List<Venue>> GetAllVenuesByManager(int managerID)
        {
            var check = await _context.Venues
                .Include(x=>x.Events)
                .Include(x=>x.VenueType)
                .Include(x=>x.Venue_City)
                .Where(x=>x.ManagerId== managerID).ToListAsync();

            if(check.Count()>0)
            {
                return check;
            }

            throw new CustomError(404, $"No data found");
        }
    }
}