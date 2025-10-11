using Google.Apis.Calendar.v3.Data;

/// <summary>
/// Define el contrato para un servicio que interactúa con la API de Google Calendar.
/// </summary>
public interface ICalendarService
{
   
    /// <summary>
    /// Obtiene eventos del calendario de forma asíncrona.
    /// </summary>
    /// <param name="accessToken">El token de acceso para la autenticación con la API de Google.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene una lista de eventos (<see cref="Event"/>).</returns>
     Task<IList<GoogleCalendar>> GetEventsAsync(string accessToken);
}