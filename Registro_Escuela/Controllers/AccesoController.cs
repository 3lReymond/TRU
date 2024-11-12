using Microsoft.AspNetCore.Mvc;
using Registro_Escuela.Models;
using Registro_Escuela.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using NuGet.Configuration;

namespace Registro_Escuela.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> Index(Usuario _usuario)
        {
            DA_logica _da_usuario = new DA_logica();

            var usuario = _da_usuario.ValidarUsuario(_usuario.Correo, _usuario.Clave);
            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,  usuario.Nombre),
                    new Claim ("correo",usuario.Correo )
                };

                foreach(string rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol)); 
                }

                var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));


                 return RedirectToAction("Listar","Mantenedor");
            }
            else
            {
                return View();
            }

            
        }
        public async Task <IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Listar", "Mantenedor");
        }

    }

}
