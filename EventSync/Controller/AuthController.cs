using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/calendar"
        };
        return Challenge(properties, "Google");
    
    }
}