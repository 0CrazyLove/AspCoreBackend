using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

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
            RedirectUri = "/calendar"
        };
        return Challenge(properties, "Google");

        
    }
}