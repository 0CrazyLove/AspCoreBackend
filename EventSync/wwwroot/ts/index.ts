import type { CalendarEvent } from "./types.js";
import { parseFormData } from "./formParser.js";
import { createEvent } from "./apiClient.js";

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

document.addEventListener("DOMContentLoaded", () => {
    getFormData();
});