using Google.Apis.Calendar.v3.Data;

/// <summary>
/// Define el contrato para un servicio que interactúa con la API de Google Calendar.
/// </summary>
public interface ICalendarService
{

    /// <summary>
    /// Obtiene eventos del calendario de forma asíncrona.
    /// </summary>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene una lista de eventos (<see cref="Event"/>).</returns>
    Task<IList<GoogleCalendar>> GetEventsAsync();

    /// <summary>
    /// Crea un nuevo evento en el calendario de forma asíncrona.
    /// </summary>
    /// <param name="calendarEvent">El objeto de evento a crear.</param>
    /// <returns>El evento creado por la API de Google Calendar.</returns>
    Task<Event> CreateEventAsync(Event calendarEvent);
}