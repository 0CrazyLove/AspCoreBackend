using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

/// <summary>
/// Controlador responsable de la autenticación de usuarios.
/// </summary>
public class AuthController : Controller
{
    /// <summary>
    /// Inicia el proceso de autenticación con Google.
    /// </summary>
    /// <returns>Un <see cref="ChallengeResult"/> que redirige al usuario al proveedor de Google para la autenticación.</returns>
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties
        {
            IsPersistent = true, //ver esto
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),//ver estoO
            RedirectUri = "/calendar"
        };

        return Challenge(properties, "Google");


    }
}