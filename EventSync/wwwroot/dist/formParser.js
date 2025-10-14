export function parseFormData(formData) {
    const startDateInput = formData.get("StartDate");
    const endDateInput = formData.get("EndDate");
    const startDate = new Date(startDateInput + ":00");
    const endDate = new Date(endDateInput + ":00");
    return {
        Summary: formData.get("Summary"),
        Description: formData.get("Description"),
        Location: formData.get("Location"),
        StartDate: startDate.toISOString(),
        EndDate: endDate.toISOString(),
    };
}
//# sourceMappingURL=formParser.js.map