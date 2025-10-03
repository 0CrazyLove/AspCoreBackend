using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
public class CalendarController : Controller
{
    public async Task<IActionResult> Index() //preguntar si el "Task" viene del async y que significa lo que hay entre los <> del Task
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var credential = GoogleCredential.FromAccessToken(token);

        var services = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "GoogleCalendarDemo"
        });

        var events = await services.Events.List("primary").ExecuteAsync();

        return View(events.Items);
    }

    
}