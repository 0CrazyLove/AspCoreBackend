using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CalendarApiController : ControllerBase
{
    private readonly ICalendarService _calendarservice;

    public CalendarApiController(ICalendarService calendarService)
    {
        _calendarservice = calendarService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateEventAsync([FromBody] GoogleCalendar dtos)
    {
        if (dtos == null) return BadRequest();
        var calendarEvent = new Event
        {
            Summary = dtos.Summary,
            Description = dtos.Description,
            Location = dtos.Location,
            Start = new EventDateTime
            {
                DateTimeDateTimeOffset = dtos.StartDate,

            },
            End = new EventDateTime
            {
                DateTimeDateTimeOffset = dtos.EndDate

            }
        };

        var createdEvent = await _calendarservice.CreateEventAsync(calendarEvent);

        return Ok(createdEvent); 
    }



}