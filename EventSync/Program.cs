
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


Env.Load();

builder.Services.AddScoped<ICalendarService, GoogleCalendarService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Cookie";
    options.DefaultSignInScheme = "Cookie";
    options.DefaultChallengeScheme = "Google";
})
.AddCookie("Cookie")
.AddGoogle("Google", options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")!;
    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")!;
    options.Scope.Add("https://www.googleapis.com/auth/calendar");
    options.SaveTokens = true;
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers(); // Habilita el mapeo de rutas por atributos en los controladores.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();

