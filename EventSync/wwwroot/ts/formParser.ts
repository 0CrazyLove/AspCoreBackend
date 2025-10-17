import type { CalendarEvent } from "./types.js";
export function parseFormData(formData: FormData) : CalendarEvent {
   
    const summary = formData.get("Summary") as string;
    const description = formData.get("Description") as string;
    const location = formData.get("Location") as string;   
    const startDateInput = formData.get("StartDate") as string;
    const endDateInput = formData.get("EndDate") as string;

    const startDate = new Date(startDateInput + ":00");
    const endDate = new Date(endDateInput + ":00");


    return {
        Summary: summary,
        Description:description,
        Location: location,
        StartDate:startDate.toISOString(),
        EndDate: endDate.toISOString(),
    };
}


