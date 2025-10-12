using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;

/// <summary>
/// Implementación del servicio para interactuar con la API de Google Calendar.
/// </summary>
public class GoogleCalendarService : ICalendarService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GoogleCalendarService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Obtiene una lista de eventos del calendario principal del usuario.
    /// </summary>
    private CalendarService InitializerService(string accessToken)
    {
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var initializer = new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        };
        var calendarService = new CalendarService(initializer);

        return calendarService;
    }
    public async Task<IList<GoogleCalendar>> GetEventsAsync()
    {
        var accessToken = await ValidateAccessTokenAsync();

        var CalendarService = InitializerService(accessToken);

        var events = await CalendarService.Events.List("primary").ExecuteAsync();

        var eventList = events.Items.Select(e => new GoogleCalendar
        {
            Id = e.Id,
            Summary = e.Summary,
            Description = e.Description,
            Location = e.Location,
            StartDate = e.Start.DateTimeDateTimeOffset?.DateTime ?? (DateTime.TryParse(e.Start.Date, out var startDate) ? startDate : null),
            EndDate = e.End.DateTimeDateTimeOffset?.DateTime ?? (DateTime.TryParse(e.End.Date, out var endDate) ? endDate : null),
            Organizer = e.Organizer?.DisplayName ?? e.Organizer?.Email ?? string.Empty,

        }).ToList(); //estudiar esto

        return eventList;
    }

    public async Task<Event> CreateEventAsync(Event calendarEvent)
    {
        var accessToken = await ValidateAccessTokenAsync();

        var calendarService = InitializerService(accessToken);

        var createdEvent = await calendarService.Events.Insert(calendarEvent, "primary").ExecuteAsync();

        return createdEvent;
    }

    private async Task<string?> GetAccessTokenAsync()
    {
        if (_httpContextAccessor.HttpContext == null) return null;

        var access_token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
    
        return access_token;
    }

    private async Task<string> ValidateAccessTokenAsync()
    {
        var accessToken = await GetAccessTokenAsync();

        if (string.IsNullOrEmpty(accessToken))
        {
            throw new InvalidOperationException("No se pudo obtener el token de acceso. El usuario podría necesitar autenticarse de nuevo.");
        }
        return accessToken;
    }
}
