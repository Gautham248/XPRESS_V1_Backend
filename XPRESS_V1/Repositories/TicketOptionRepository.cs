using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class TicketOptionRepository : ITicketOptionService
    {
        private readonly ApiDbContext _context;

        public TicketOptionRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<TicketOption> CreateTicketOptionAsync(TicketOption ticketOption)
        {
            ticketOption.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.TicketOptions.AddAsync(ticketOption);
            await _context.SaveChangesAsync();
            return ticketOption;
        }

        public async Task<TicketOption> GetTicketOptionByIdAsync(int optionId)
        {
            return await _context.TicketOptions
                .Include(to => to.TravelRequest)
                .Include(to => to.Creator)
                .FirstOrDefaultAsync(to => to.OptionId == optionId);
        }

        public async Task<IEnumerable<TicketOption>> GetAllTicketOptionsAsync()
        {
            return await _context.TicketOptions
                .Include(to => to.TravelRequest)
                .Include(to => to.Creator)
                .ToListAsync();
        }

        public async Task<TicketOption> UpdateTicketOptionAsync(int optionId, TicketOption ticketOption)
        {
            var existingOption = await _context.TicketOptions.FindAsync(optionId);
            if (existingOption == null)
                return null;

            existingOption.RequestId = ticketOption.RequestId;
            existingOption.OptionDescription = ticketOption.OptionDescription;
            existingOption.AirlineName = ticketOption.AirlineName;
            existingOption.FlightNumber = ticketOption.FlightNumber;
            existingOption.DepartureTime = ticketOption.DepartureTime;
            existingOption.ArrivalTime = ticketOption.ArrivalTime;
            existingOption.Price = ticketOption.Price;
            existingOption.BookingClass = ticketOption.BookingClass;
            existingOption.AdditionalDetails = ticketOption.AdditionalDetails;
            existingOption.IsAvailable = ticketOption.IsAvailable;
            existingOption.CreatedBy = ticketOption.CreatedBy;

            await _context.SaveChangesAsync();
            return existingOption;
        }

        public async Task<bool> DeleteTicketOptionAsync(int optionId)
        {
            var ticketOption = await _context.TicketOptions.FindAsync(optionId);
            if (ticketOption == null)
                return false;

            _context.TicketOptions.Remove(ticketOption);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TicketOption>> GetTicketOptionsByRequestAsync(int requestId)
        {
            return await _context.TicketOptions
                .Where(to => to.RequestId == requestId)
                .Include(to => to.Creator)
                .ToListAsync();
        }
    }
}
