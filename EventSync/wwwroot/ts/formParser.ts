import type { CalendarEvent } from "./types.js";

/**
 * Convierte los datos del formulario en un objeto CalendarEvent.
 * Extrae los campos del formulario y formatea las fechas al formato ISO 8601.
 * 
 * @param {FormData} formData - Datos del formulario HTML que contiene la información del evento
 * @returns {CalendarEvent} Objeto con los datos del evento formateados
 * 
 * @example
 * const formData = new FormData(formElement);
 * const event = parseFormData(formData);
 * // Returns: { 
 * //   Summary: "Reunión", 
 * //   Description: "Reunión de equipo",
 * //   Location: "Sala 1",
 * //   StartDate: "2025-10-18T10:00:00.000Z",
 * //   EndDate: "2025-10-18T11:00:00.000Z"
 * // }
 * 
 * @description
 * Los campos esperados en el FormData son:
 * - Summary: Título del evento
 * - Description: Descripción detallada
 * - Location: Ubicación del evento
 * - StartDate: Fecha y hora de inicio (formato: YYYY-MM-DDTHH:mm)
 * - EndDate: Fecha y hora de fin (formato: YYYY-MM-DDTHH:mm)
 */
export function parseFormData(formData: FormData): CalendarEvent {
   
    const summary = formData.get("Summary") as string;
    const description = formData.get("Description") as string;
    const location = formData.get("Location") as string;   
    const startDateInput = formData.get("StartDate") as string;
    const endDateInput = formData.get("EndDate") as string;
    const startDate = new Date(startDateInput + ":00");
    const endDate = new Date(endDateInput + ":00");
    
    return {
        Summary: summary,
        Description: description,
        Location: location,
        StartDate: startDate.toISOString(),
        EndDate: endDate.toISOString(),
    };
}