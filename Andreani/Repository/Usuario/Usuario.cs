using Dapper;
using Model;
using System.Data;

namespace Repository.Usuario
{
    public class Usuario : IO
    {
		public Usuario(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction) { }
		
        public List<Model.Usuario> GetUsuarioIO(string usuario)
        {
            string sql = @"
SELECT id_usuario, id_persona, usuario, nombre, apellido, documento, email ,telefono
FROM [dbo].[Usuarios] ";
            return _connection.Query<Model.Usuario>(sql, transaction: _transaction).ToList();
        }

        public Task<int> DeleteLogicalPersonIO(int id)
        {
            string sql = @"
UPDATE Persona
set Activo = 0
Where Persona.ID = @ID
";

            return _connection.ExecuteAsync(sql, param: new { ID = id }, transaction: _transaction);
        }

        public Task<int> ResetAllActivePersonIO()
        {
            string sql = @"
UPDATE Persona
set Activo = 1
";

            return _connection.ExecuteAsync(sql, transaction: _transaction);
        }


        //public List<Persona> GetAllPersonIO()
        //{
        //    string sql = @"
        //SELECT ID,Nombre,Apellido,Provincia,Dni,Telefono,Activo,Email,Profiles,Skills
        //FROM [dbo].[Persona]";

        //    return _connection.Query<Persona>(sql, transaction: _transaction).ToList();
        //}
    }
}
