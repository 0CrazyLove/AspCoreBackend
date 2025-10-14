export function parseFormData(formData: FormData) {
    const startDateInput = formData.get("StartDate") as string;
    const endDateInput = formData.get("EndDate") as string;

    const startDate = new Date(startDateInput + ":00");
    const endDate = new Date(endDateInput + ":00");


    return {
        Summary: formData.get("Summary") as string,
        Description: formData.get("Description") as string,
        Location: formData.get("Location") as string,
        StartDate:startDate.toISOString(),
        EndDate: endDate.toISOString(),
    };
}
