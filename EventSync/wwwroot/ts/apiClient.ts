import type { CalendarEvent } from "./types.js";

/**
 * Crea un nuevo evento de calendario en el servidor mediante una petición POST.
 * Envía los datos del evento al endpoint de la API en formato JSON.
 * 
 * @async
 * @param {CalendarEvent} event - Objeto con los datos del evento a crear
 * @returns {Promise<void>} No retorna ningún valor
 * 
 * @example
 * const nuevoEvento: CalendarEvent = {
 *   Summary: "Capacitación",
 *   Description: "Taller de TypeScript",
 *   Location: "Online",
 *   StartDate: "2025-10-25T09:00:00.000Z",
 *   EndDate: "2025-10-25T12:00:00.000Z"
 * };
 * 
 * await createEvent(nuevoEvento);
 * 
 * @description
 * Realiza una petición POST a '/api/CalendarApi' con:
 * - Headers: Content-Type application/json
 * - Body: Evento serializado en JSON
 */
export async function createEvent(event: CalendarEvent): Promise<void> {
    await fetch('/api/CalendarApi', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(event)
    });
}