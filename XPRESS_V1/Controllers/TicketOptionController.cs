using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                OptionDescription = o.OptionDescription,
                IsSelected = o.IsSelected
            }));
        }

        // Create a new ticket option
        [HttpPost("{requestId}/ticketoption")]
        public async Task<IActionResult> CreateTicketOption(int requestId, [FromBody] TicketOptionDTO dto)
        {
            var travelRequest = await _travelRequestService.GetTravelRequestByIdAsync(requestId);
            if (travelRequest == null)
            {
                return NotFound("Travel request not found.");
            }

            var ticketOption = new TicketOption
            {
                RequestId = requestId,
                OptionDescription = dto.OptionDescription,
                CreatedAt = DateTime.UtcNow,
                IsSelected = false // default value
            };

            var created = await _ticketOptionService.CreateTicketOptionAsync(ticketOption);

            var resultDto = new TicketOptionDTO
            {
                OptionId = created.OptionId,
                RequestId = created.RequestId,
                OptionDescription = created.OptionDescription,
                IsSelected = created.IsSelected
            };

            // Update travel request status (e.g., to "Options Created")
            //var statusId = 4; // Replace with actual status ID
            //await _travelRequestService.UpdateTravelRequestStatusAsync(requestId, statusId, DateTime.UtcNow);

            return CreatedAtAction(nameof(GetTicketOptionById), new { optionId = created.OptionId }, resultDto);
        }


        // Get ticket option by ID
        [HttpGet("{optionId}")]
        public async Task<IActionResult> GetTicketOptionById(int optionId)
        {
            var option = await _ticketOptionService.GetTicketOptionByIdAsync(optionId);
            if (option == null) return NotFound("Ticket Option not found.");

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

        // Delete a ticket option (with requestId)
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


        // ------------- Ticket Selection Complete Code ------------- 


        // API
        [HttpPut("{requestId}/{optionId}/select")]
        public async Task<IActionResult> SelectTicketOption(int requestId, int optionId, [FromBody] TicketOptionSelectionDTO dto)
        {
            var travelRequest = await _travelRequestService.GetTravelRequestByIdAsync(requestId);
            if (travelRequest == null)
            {
                return NotFound("Travel request not found.");
            }

            // Deselect all other options for this request
            await _ticketOptionService.DeselectAllOptionsForRequestAsync(requestId);

            var selectedOption = await _ticketOptionService.GetTicketOptionByIdAsync(optionId);
            if (selectedOption == null || selectedOption.RequestId != requestId)
            {
                return NotFound("Ticket option not found for this request.");
            }

            // Select the chosen option
            await _ticketOptionService.SelectTicketOptionAsync(selectedOption.OptionId);

            // Update the travel request
            travelRequest.SelectedTicketOptionId = selectedOption.OptionId;
            await _travelRequestService.UpdateTravelRequestAsync(requestId, travelRequest);

            return Ok(new { message = $"Option {selectedOption.OptionId} selected successfully." });
        }

    }
}
