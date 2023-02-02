using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Conections;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Andreani.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Token
    {
        private readonly IConfiguration Configuration;
        public Token(IConfiguration config)
        {
            this.Configuration = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateToken(string usuario, string pass)
        {
            var SecretKey = Configuration.GetValue<string>("SecretKey");
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

            return new JsonResult(new { tokencreado });

        }
    }
}
