using Microsoft.AspNetCore.Mvc;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketOptionsController : ControllerBase
    {
        private readonly ITicketOptionService _ticketOptionService;
        private readonly ITravelRequestService _travelRequestService;

        public TicketOptionsController(
            ITicketOptionService ticketOptionService,
            ITravelRequestService travelRequestService)
        {
            _ticketOptionService = ticketOptionService;
            _travelRequestService = travelRequestService;
        }

        // Get ticket options for a specific request
        [HttpGet("{requestId}/ticketoption")]
        public async Task<IActionResult> GetTicketOptionsByRequest(int requestId)
        {
            var options = await _ticketOptionService.GetTicketOptionsByRequestAsync(requestId);
            return Ok(options.Select(o => new TicketOptionDTO
            {
                OptionId = o.OptionId,
                RequestId = o.RequestId,
                OptionDescription = o.OptionDescription
            }));
        }

        // Create a new ticket option
        [HttpPost]
        public async Task<IActionResult> CreateTicketOption([FromBody] TicketOptionDTO dto)
        {
            var ticketOption = new TicketOption
            {
                RequestId = dto.RequestId,
                OptionDescription = dto.OptionDescription
            };

            var created = await _ticketOptionService.CreateTicketOptionAsync(ticketOption);

            var resultDto = new TicketOptionDTO
            {
                OptionId = created.OptionId,
                RequestId = created.RequestId,
                OptionDescription = created.OptionDescription
            };

            return CreatedAtAction(nameof(GetTicketOptionById), new { optionId = created.OptionId }, resultDto);
        }

        // Get ticket option by ID
        [HttpGet("{optionId}")]
        public async Task<IActionResult> GetTicketOptionById(int optionId)
        {
            var option = await _ticketOptionService.GetTicketOptionByIdAsync(optionId);
            if (option == null) return NotFound();

            return Ok(new TicketOptionDTO
            {
                OptionId = option.OptionId,
                RequestId = option.RequestId,
                OptionDescription = option.OptionDescription
            });
        }

        // Update a ticket option (with requestId)
        [HttpPut("{requestId}/{optionId}")]
        public async Task<IActionResult> UpdateTicketOption(int requestId, int optionId, [FromBody] TicketOptionDTO dto)
        {
            var travelRequest = await _travelRequestService.GetTravelRequestByIdAsync(requestId);
            if (travelRequest == null)
            {
                return NotFound("Travel request not found.");
            }

            var existingOption = await _ticketOptionService.GetTicketOptionByIdAsync(optionId);
            if (existingOption == null || existingOption.RequestId != requestId)
            {
                return NotFound("Ticket option not found for this request.");
            }

            existingOption.OptionDescription = dto.OptionDescription;

            var updated = await _ticketOptionService.UpdateTicketOptionAsync(optionId, existingOption);

            return Ok(new TicketOptionDTO
            {
                OptionId = updated.OptionId,
                RequestId = updated.RequestId,
                OptionDescription = updated.OptionDescription
            });
        }

        [HttpDelete("{requestId}/ticketoption/{optionId}")]
        public async Task<IActionResult> DeleteTicketOption(int requestId, int optionId)
        {
            var ticketOption = await _ticketOptionService.GetTicketOptionByIdAsync(optionId);

            if (ticketOption == null)
            {
                return NotFound("Ticket option not found.");
            }

            if (ticketOption.RequestId != requestId)
            {
                return BadRequest("Ticket option does not belong to the specified travel request.");
            }

            var deleted = await _ticketOptionService.DeleteTicketOptionAsync(optionId);
            if (!deleted)
            {
                return StatusCode(500, "Failed to delete the ticket option.");
            }

            // Update request status
            //var newStatusId = 5;
            //var nowUtc = DateTime.UtcNow;
            //await _travelRequestService.UpdateTravelRequestStatusAsync(requestId, newStatusId, nowUtc);

            return NoContent();
        }


    }
}
