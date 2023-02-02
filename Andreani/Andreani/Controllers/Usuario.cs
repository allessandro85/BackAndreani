using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Services.Conections;
using Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

namespace Andreani.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Usuario 
    {
        private readonly IConfiguration Configuration;
        public Usuario(IConfiguration config)
        {
            this.Configuration = config;
        }

        [HttpPost]
        public IActionResult ValidarUsuario(string usuario, string pass)
        {
            IDbConnectionFactory dbConnectionFactory2 = new SqlConnectionFactory(Configuration.GetConnectionString("ProyectDB"));
            AppConfig.Instance.Set(dbConnectionFactory: dbConnectionFactory2, appID: "", appName: "ProuectoDB");

            Services.UsuarioServices.UsuarioServices usuarioServices = new Services.UsuarioServices.UsuarioServices();
            var respMetodo = usuarioServices.GetUsuarioRepository(usuario);

            var identity = new ClaimsIdentity(usuario);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.ToString()),
                new Claim(ClaimTypes.Name, pass.ToString())
            };
            var identity2 = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity2);

            var respuestaToken = Model.Usuario.validarToken(identity2, respMetodo);
            if(respuestaToken == "Usuario Incorrecto")
            {
                return new UnauthorizedResult();
            }

            return new OkObjectResult(new { userid = usuario });
        }

    }
}
