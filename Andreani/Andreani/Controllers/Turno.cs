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
    public class Turno
    {
        private readonly IConfiguration Configuration;
        public Turno(IConfiguration config)
        {
            this.Configuration = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public string GetAll()
        {
           return "";

        }

        [HttpGet]
        [AllowAnonymous]
        public string GetById()
        {
            return "";
        }

    }
}
