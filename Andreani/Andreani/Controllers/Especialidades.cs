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
    public class Especialidades
    {
        private readonly IConfiguration Configuration;
        public Especialidades(IConfiguration config)
        {
            this.Configuration = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<Model.Especialidades> GetAll()
        {
            IDbConnectionFactory dbConnectionFactory2 = new SqlConnectionFactory(Configuration.GetConnectionString("ProyectDB"));
            AppConfig.Instance.Set(dbConnectionFactory: dbConnectionFactory2, appID: "", appName: "ProuectoDB");

            Services.EspecialidadesServices.EspecialidadesServices especialidadServices = new Services.EspecialidadesServices.EspecialidadesServices();
            return especialidadServices.GetEspecialidadesRepository().ToList();

        }


    }
}
