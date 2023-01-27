using AttestationAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectBack.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuarioController : PYController
    {
        private readonly IConfiguration config;
        public UsuarioController(IConfiguration config)
        {
            this.config = config;   
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string usuario, string contraseña)
        {
            ProyectRepository.UsuarioRepository.UsuarioRepository personaRepository = new(dbContext);
            var algo = personaRepository.GetUsuarioRepository();

            var newusuario = new Usuario()
            {
                //Cuil = usuario.Cuil,
                //Nombre = usuario.Nombre,
                //Apellido = usuario.Apellido,
                //Rol = usuario.IdRolNavigation.NRol,
                //Matricula = usuario.Matricula,
                //Profesion = usuario.IdProfesionNavigation.Descripcion,
                //Estado = usuario.Estado,
                //FecAlta = usuario.FecAlta,
                //UsrAlta = usuario?.UsrAlta,
                //FecModif = usuario?.FecModif,
                //UsrModif = usuario?.UsrModif
            };

            var SecretKey = config.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);

            return Ok(tokencreado);
        }

        [HttpGet]
        [AllowAnonymous]
        public Usuario GetAll()
        {
            ProyectRepository.UsuarioRepository.UsuarioRepository personaRepository = new(dbContext);
            return personaRepository.GetUsuarioRepository();
        }

        //[HttpPut]
        //[AllowAnonymous]
        //public Task<int> DeletePerson(int id)
        //{
        //    PersonRepository personaRepository = new(dbContext);
        //    return personaRepository.DeleteLogicalPersonRepository(id);
        //}

        //[HttpPut]
        //[AllowAnonymous]
        //public Task<int> ResetAllActive()
        //{
        //    PersonRepository personaRepository = new(dbContext);
        //    return personaRepository.ResetAllActiveRepository();
        //}

    }
}