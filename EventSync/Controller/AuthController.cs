using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties
        {
            RedirectUri = "/calendar"
        }, "Google");
    }
}