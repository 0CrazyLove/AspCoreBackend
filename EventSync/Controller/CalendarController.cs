using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador para gestionar las operaciones del calendario. Requiere autorización.
/// </summary>
[Authorize]
public class CalendarController : Controller
{
    private readonly IGoogleCalendarService _calendarService;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CalendarController"/>.
    /// </summary>
    /// <param name="calendarService">El servicio para interactuar con la API de Google Calendar.</param>
    public CalendarController(IGoogleCalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    /// <summary>
    /// Muestra la página principal del calendario con los eventos del usuario.
    /// </summary>
    /// <returns>Una vista con la lista de eventos del calendario.</returns>
    public async Task<IActionResult> Index()
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        if (string.IsNullOrEmpty(token)) return Unauthorized(); //que hace el return Unauthorized?

        var events = await _calendarService.GetEventsAsync(token);

        return View(events);
    }
}