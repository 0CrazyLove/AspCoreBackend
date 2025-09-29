
using Microsoft.Extensions.Options;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//load the .env file
Env.Load(); 

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = "Cookie";
    Options.DefaultAuthenticateScheme = "Cookie";
    Options.DefaultChallengeScheme = "Google";
})
.AddCookie("Cookie")
.AddGoogle("Google", Options =>
{
    Options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")!;
    Options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")!;
    Options.Scope.Add("https://www.googleapis.com/auth/calendar");
    Options.SaveTokens = true;
});

var app = builder.Build();


app.UseHttpsRedirection();
app.MapControllers(); // Habilita el mapeo de rutas por atributos en los controladores.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();

