/**
 * Representa un evento de calendario con toda su información.
 * 
 * @interface CalendarEvent
 * @property {string} Summary - Título o resumen del evento
 * @property {string} Description - Descripción detallada del evento
 * @property {string} Location - Ubicación donde se realizará el evento
 * @property {string} StartDate - Fecha y hora de inicio en formato ISO 8601 (YYYY-MM-DDTHH:mm:ss.sssZ)
 * @property {string} EndDate - Fecha y hora de fin en formato ISO 8601 (YYYY-MM-DDTHH:mm:ss.sssZ)
 * 
 * @example
 * const evento: CalendarEvent = {
 *   Summary: "Reunión de Proyecto",
 *   Description: "Revisión de avances del Q4",
 *   Location: "Sala de Conferencias B",
 *   StartDate: "2025-10-20T14:00:00.000Z",
 *   EndDate: "2025-10-20T15:30:00.000Z"
 * };
 */
export interface CalendarEvent {
    Summary: string;
    Description: string;
    Location: string;
    StartDate: string;
    EndDate: string;
}