using Microsoft.AspNetCore.Mvc;
using api.Domain.DTOs;
using api.Domain.Interfaces;

namespace api.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public SalesController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<TicketsResponse>> GetTickets([FromQuery] TicketsQuery query)
    {
        try
        {
            var result = await _ticketService.GetTickets(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving tickets", details = ex.Message });
        }
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<TicketsResponse>> GetTicketsByEventId(string eventId, [FromQuery] TicketsQuery query)
    {
        try
        {
            var result = await _ticketService.GetTicketsByEventId(eventId, query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving tickets for event", details = ex.Message });
        }
    }

    [HttpGet("analytics/sales-count")]
    public async Task<ActionResult<List<EventSalesDto>>> GetTopEventsBySalesCount([FromQuery] int limit = 5)
    {
        try
        {
            if (limit <= 0 || limit > 100)
            {
                return BadRequest(new { error = "Limit must be between 1 and 100" });
            }

            var result = await _ticketService.GetTopEventsBySalesCount(limit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving top events by sales count", details = ex.Message });
        }
    }

    [HttpGet("analytics/total-amount")]
    public async Task<ActionResult<List<EventSalesDto>>> GetTopEventsByTotalAmount([FromQuery] int limit = 5)
    {
        try
        {
            if (limit <= 0 || limit > 100)
            {
                return BadRequest(new { error = "Limit must be between 1 and 100" });
            }

            var result = await _ticketService.GetTopEventsByTotalAmount(limit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving top events by total amount", details = ex.Message });
        }
    }
}
