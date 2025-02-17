﻿
using Ejemplo_05_Areas.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Ejemplo_05_0_Integracion.Services;

namespace Ejemplo_05_Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles = "Admin")]
public class AccountController : Controller
{
    UsuariosService _service = new UsuariosService();

    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    async public Task<ViewResult> Login(string ReturnUrl)
    {
        return View(new UsuarioModel
        {
            ReturnUrl = ReturnUrl
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UsuarioModel usuario, string returnUrl = "/")
    {
        var result = _service.VerificarLogin(usuario);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Usuario o contraseña no válidos.");
            return View();
        }
      
        var claims = new List<Claim>()
        {
             new Claim(ClaimTypes.Name, usuario.Nombre),
        };

        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("Cookies", principal);

        return Redirect(returnUrl);
    }

    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
        

        await HttpContext.SignOutAsync();
        return Redirect(returnUrl);
    }
}
