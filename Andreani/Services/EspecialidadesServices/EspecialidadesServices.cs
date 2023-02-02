
using Microsoft.Extensions.Configuration;
using Repository.Especialidades;
using Services.Services;

namespace Services.EspecialidadesServices
{
    public class EspecialidadesServices : ServiceWithDbContext
    {
        //private readonly IConfiguration Configuration;
        //public UsuarioServices(IConfiguration configuration)
        //{
        //    this.Configuration = configuration;
        //}
        public EspecialidadesServices()
        {

        }
        public List<Model.Especialidades> GetEspecialidadesRepository()
        {
            var especialidadesIO = new Especialidades(dbContext.Connection, dbContext.Transaction);
            return especialidadesIO.GetEspecialidadesIO();
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
