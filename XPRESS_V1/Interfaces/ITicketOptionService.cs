using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface ITicketOptionService
    {
        Task<TicketOption> CreateTicketOptionAsync(TicketOption ticketOption);
        Task<TicketOption> GetTicketOptionByIdAsync(int optionId);
        Task<IEnumerable<TicketOption>> GetAllTicketOptionsAsync();
        Task<TicketOption> UpdateTicketOptionAsync(int optionId, TicketOption ticketOption);
        Task<bool> DeleteTicketOptionAsync(int optionId);
        Task<IEnumerable<TicketOption>> GetTicketOptionsByRequestAsync(int requestId);
    }

}
