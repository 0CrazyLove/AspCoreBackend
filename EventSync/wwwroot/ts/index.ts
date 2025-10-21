import type { CalendarEvent } from "./types.js";
import { parseFormData } from "./formParser.js";
import { createEvent } from "./apiClient.js";

/**
 * Obtiene los datos del formulario y configura el evento de envío.
 * Escucha el evento submit del formulario, procesa los datos y crea un evento de calendario.
 * 
 * @async
 * @returns {Promise<void>} No retorna ningún valor
 * 
 * @example
 * // Se ejecuta automáticamente al cargar el DOM
 * getFormData();
 */
export async function getFormData(): Promise<void> {
    const form = document.getElementById("form") as HTMLFormElement;
    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        const formData = new FormData(form);
        const event: CalendarEvent = parseFormData(formData);
        await createEvent(event);
        form.reset();
    })
}

/**
 * Inicializa la aplicación cuando el DOM está completamente cargado.
 * Configura el manejador del formulario para capturar eventos de calendario.
 */
document.addEventListener("DOMContentLoaded", () => {
    getFormData();
});