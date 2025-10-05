using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

/// <summary>
/// Implementación del servicio para interactuar con la API de Google Calendar.
/// </summary>
public class GoogleCalendarService : IGoogleCalendarService
{
    private const string ApplicationName = "GoogleCalendarDemo"; //por que se pone const para la variable ApplicationName

    /// <summary>
    /// Obtiene una lista de eventos del calendario principal del usuario.
    /// </summary>
    /// <param name="accessToken">El token de acceso de OAuth 2.0 para autorizar la solicitud a la API.</param>
    /// <returns>Una lista de eventos del calendario.</returns>
    public async Task<IList<Event>> GetEventsAsync(string accessToken)
    {
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var initializer = new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        };

        var CalendarService = new CalendarService(initializer);

        var events = await CalendarService.Events.List("primary").ExecuteAsync();

        return events.Items;

    }
}
