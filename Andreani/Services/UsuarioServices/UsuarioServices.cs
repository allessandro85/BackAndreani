
using Microsoft.Extensions.Configuration;
using Repository.Usuario;
using Services.Services;

namespace Services.UsuarioServices
{
    public class UsuarioServices : ServiceWithDbContext
    {
        //private readonly IConfiguration Configuration;
        //public UsuarioServices(IConfiguration configuration)
        //{
        //    this.Configuration = configuration;
        //}
        public UsuarioServices()
        {

        }
        public List<Model.Usuario> GetUsuarioRepository(string usuario)
        {
            var usuarioIO = new Usuario(dbContext.Connection, dbContext.Transaction);
            return usuarioIO.GetUsuarioIO(usuario);
        }


        //public Task<int> DeleteLogicalPersonRepository(int id)
        //{
        //    var personaIO = new PersonIO(_dbContext.Connection, _dbContext.Transaction);
        //    return personaIO.DeleteLogicalPersonIO(id);
        //}

        //public Task<int> ResetAllActiveRepository()
        //{
        //    var personaIO = new PersonIO(_dbContext.Connection, _dbContext.Transaction);
        //    return personaIO.ResetAllActivePersonIO();
        //}

        //public List<Persona> GetAllPersonRepository()
        //{
        //    var personaIO = new PersonIO(_dbContext.Connection, _dbContext.Transaction);
        //    return personaIO.GetAllPersonIO();
        //}
    }
}
