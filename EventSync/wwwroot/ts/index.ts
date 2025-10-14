import type { CalendarEvent } from "./types.js";
import { parseFormData } from "./formParser.js";
import { createEvent } from "./apiClient.js";



export async function getFormData() {
    const form = document.getElementById("form") as HTMLFormElement;
    form.addEventListener("submit", async function (e) {
        e.preventDefault();
        const event: CalendarEvent = parseFormData(new FormData(form));
        await createEvent(event);
        form.reset();
    })
}

document.addEventListener("DOMContentLoaded", function () {
    getFormData();
});