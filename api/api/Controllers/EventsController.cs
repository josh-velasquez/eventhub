using Microsoft.AspNetCore.Mvc;
using api.Domain.Interfaces;
using api.Domain.DTOs;

namespace api.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<EventsResponse>> GetEvents([FromQuery] EventsQuery query)
    {
        try
        {
            var response = await _eventService.GetEvents(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving events", details = ex.Message });
        }
    }
}
