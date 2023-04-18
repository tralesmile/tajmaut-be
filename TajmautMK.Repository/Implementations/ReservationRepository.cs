﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public ReservationRepository(tajmautDataContext ctx, IHelperValidationClassService helper)
        {
            _ctx = ctx;
            _helper = helper;
        }

        //change reservation status - accepted , declined
        public async Task<bool> ChangeReservationStatus(OnlineReservation onlineReservation)
        {
            if (onlineReservation.IsActive)
            {
                onlineReservation.IsActive = false;
            }
            else
            {
                onlineReservation.IsActive = true;
            }
            await _ctx.SaveChangesAsync();
            return true;
        }

        //create reservations and save to DB
        public async Task<OnlineReservation> CreateReservation(ReservationREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var reservation = new OnlineReservation
            {
                VenueId = request.VenueId,
                UserId = currentUserID,
                EventId = request.EventId,
                NumberGuests = request.NumberGuests,
                Phone = request.Phone,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                ModifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedBy = currentUserID,
                CreatedBy = currentUserID,
            };

            _ctx.OnlineReservations.Add(reservation);
            await _ctx.SaveChangesAsync();

            return reservation;
        }

        //delete reservation
        public async Task<bool> DeleteReservation(OnlineReservation onlineReservation)
        {
            _ctx.OnlineReservations.Remove(onlineReservation);
            await _ctx.SaveChangesAsync();
            return true;
        }

        //all reservations by user
        public async Task<List<OnlineReservation>> GetAllGetReservationsByUser(int userId)
        {
            var check = await _ctx.OnlineReservations.Where(us => us.UserId == userId).ToListAsync();
            if (check.Count() > 0)
            {
                return check;
            }
            return null;
        }

        //all reservations
        public async Task<List<OnlineReservation>> GetAllReservations()
        {
            var result = await _ctx.OnlineReservations.ToListAsync();
            if (result.Count() > 0)
            {
                return result;
            }

            throw new CustomError(404, $"No reservations found");
        }

        //reservation by id
        public async Task<OnlineReservation> GetReservationByID(int reservationId)
        {
            var check = await _ctx.OnlineReservations
                .Include(x=>x.Venue)
                .Include(x=>x.User)
                .Include(x=>x.Event)
                .FirstOrDefaultAsync(x=>x.OnlineReservationId==reservationId);
            if (check != null)
            {
                return check;
            }
            throw new CustomError(404, $"Reservation not found");
        }

        //if reservation exists
        public async Task<bool> ReservationExistsID(int reservationId)
        {
            var check = await _ctx.OnlineReservations.FindAsync(reservationId);
            if (check != null)
            {
                return true;
            }
            throw new CustomError(404, $"Reservation not found");
        }
    }
}
