import type { CalendarEvent } from "./types.js";

export async function createEvent(event: CalendarEvent): Promise<void> {
    await fetch('/api/CalendarApi', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(event)
    });
}