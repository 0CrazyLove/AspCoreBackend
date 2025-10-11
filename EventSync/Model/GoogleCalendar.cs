public class GoogleCalendar
{
    public string Id { get; set; } = string.Empty;                 // ID único del evento
    public string Summary { get; set; } = string.Empty;            // Título del evento
    public string Description { get; set; } = string.Empty;       // Descripción o detalles
    public string Location { get; set; } = string.Empty;       // Lugar (opcional)
    public DateTime? StartDate { get; set; }        // Fecha/hora inicio
    public DateTime? EndDate { get; set; }          // Fecha/hora fin
    public string Organizer { get; set; } = string.Empty;       // Nombre del organizador  

}
