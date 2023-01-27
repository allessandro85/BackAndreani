using ProyectIO.PersonIO;
using ProyectRepository.Services;
using Model;

namespace ProyectRepository.UsuarioRepository
{
    public class UsuarioRepository : ServiceWithDbContext
    {
        public UsuarioRepository(DBContext dBContext) : base(dBContext) { }
        public Usuario GetUsuarioRepository()
        {
            var usuarioIO = new UsuarioIO(_dbContext.Connection, _dbContext.Transaction);
            return usuarioIO.GetUsuarioIO();
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
