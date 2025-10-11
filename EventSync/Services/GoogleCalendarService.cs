using System.Reflection.Metadata.Ecma335;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

/// <summary>
/// Implementación del servicio para interactuar con la API de Google Calendar.
/// </summary>
public class GoogleCalendarService : ICalendarService
{

    /// <summary>
    /// Obtiene una lista de eventos del calendario principal del usuario.
    /// </summary>
    /// <param name="accessToken">El token de acceso de OAuth 2.0 para autorizar la solicitud a la API.</param>
    /// <returns>Una lista de eventos del calendario.</returns>
    /// 
    private CalendarService InitializerService(string accessToken)
    {
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var initializer = new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        };
        var CalendarService = new CalendarService(initializer);

        return CalendarService;
    }
    public async Task<IList<GoogleCalendar>> GetEventsAsync(string accessToken)
    {
        var CalendarService =  InitializerService(accessToken);

        var events = await CalendarService.Events.List("primary").ExecuteAsync();

        var resultado = events.Items.Select(e => new GoogleCalendar
        {
            Id = e.Id,
            Summary = e.Summary,
            Description = e.Description,
            Location = e.Location,
            StartDate = e.Start.DateTimeDateTimeOffset?.DateTime ?? (DateTime.TryParse(e.Start.Date, out var startDate) ? startDate : null),
            EndDate = e.End.DateTimeDateTimeOffset?.DateTime ?? (DateTime.TryParse(e.End.Date, out var endDate) ? endDate : null),
            Organizer = e.Organizer?.DisplayName ?? e.Organizer?.Email ?? string.Empty,

        }).ToList();

        return resultado;
    }

    public async Task<Event> CreateEventAsync(Event calendarEvent, string accessToken)
    {
        var calendarService =  InitializerService(accessToken);
                                                                                                    

    }

}
