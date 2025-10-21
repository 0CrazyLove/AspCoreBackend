using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
/// <summary>
/// Implementación del servicio para interactuar con la API de Google Calendar.
/// </summary>
public class GoogleCalendarService : ICalendarService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GoogleCalendarService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Obtiene una lista de eventos del calendario principal del usuario.
    /// </summary>
    private CalendarService InitializerService(string accessToken)
    {
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var initializer = new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        };
        var calendarService = new CalendarService(initializer);

        return calendarService;
    }
    public async Task<IList<GoogleCalendar>> GetEventsAsync()
    {
        var accessToken = await GetValidatedAccessTokenAsync();

        var CalendarService = InitializerService(accessToken);

        var events = await CalendarService.Events.List("primary").ExecuteAsync();

        var eventList = events.Items.Select(e => new GoogleCalendar
        {
            Id = e.Id,
            Summary = e.Summary,
            Description = e.Description,
            Location = e.Location,
            StartDate = e.Start.DateTimeDateTimeOffset?.DateTime ?? (DateTime.TryParse(e.Start.Date, out var startDate) ? startDate : null),
            EndDate = e.End.DateTimeDateTimeOffset?.DateTime ?? (DateTime.TryParse(e.End.Date, out var endDate) ? endDate : null),
            Organizer = e.Organizer?.DisplayName ?? e.Organizer?.Email ?? string.Empty,

        }).ToList(); //estudiar esto

        return eventList;
    }

    public async Task<Event> CreateEventAsync(Event calendarEvent)
    {
        var accessToken = await GetValidatedAccessTokenAsync();

        var calendarService = InitializerService(accessToken);

        var createdEvent = await calendarService.Events.Insert(calendarEvent, "primary").ExecuteAsync();

        return createdEvent;
    }
    private async Task<string?> GetRefreshTokenAsync()
    {
        if (_httpContextAccessor.HttpContext is null) return null;
        var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
        return string.IsNullOrWhiteSpace(refreshToken) ? null : refreshToken;
    }


    private async Task<string?> RequestNewAccessTokenAsync(string refreshToken)
    {
        const string tokenEndpoint = "https://oauth2.googleapis.com/token";
        var clientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");

        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "client_id", clientId! },
        { "client_secret", clientSecret! },
        { "refresh_token", refreshToken },
        { "grant_type", "refresh_token" }
    });
        using var client = new HttpClient();

        var response = await client.PostAsync(tokenEndpoint, content);
        var json = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<JsonElement>(json);

        return tokenResponse.TryGetProperty("access_token", out var token) ? token.GetString() : null;
    }

    private async Task UpdateAuthenticationAsync(string accessToken) 
    {
        var authProperties = new AuthenticationProperties();
    
        authProperties.StoreTokens([new AuthenticationToken
        {
            Name = "access_token",
            Value = accessToken
                 
        }

    ]);

        var httpContext = _httpContextAccessor.HttpContext;

        await httpContext!.SignInAsync(httpContext!.User, authProperties);
    }

    private async Task<string?> RefreshAccessTokenAsync()
    {
        var refreshToken = await GetRefreshTokenAsync();
        if (refreshToken is null) return null;

        var newAccessToken = await RequestNewAccessTokenAsync(refreshToken);
        if (newAccessToken is null) return null;

        await UpdateAuthenticationAsync(newAccessToken);
        return newAccessToken;
    }


    private async Task<string?> GetAccessTokenAsync()
    {
        if (_httpContextAccessor.HttpContext is null) return null;

        var access_token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

        return string.IsNullOrWhiteSpace(access_token) ? null : access_token;
    }

    private async Task<string> GetValidatedAccessTokenAsync()
    {
        var token = await GetAccessTokenAsync();

        if (token is not null) return token;

        var refreshedToken = await RefreshAccessTokenAsync();

        if (refreshedToken is not null) return refreshedToken;

        throw new InvalidOperationException("No se pudo obtener el token de acceso. El usuario necesita autenticarse.");
    }
}