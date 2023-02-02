using Dapper;
using Model;
using System.Data;

namespace Repository.Especialidades
{
    public class Especialidades : IO
    {
		public Especialidades(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction) { }
		
        public List<Model.Especialidades> GetEspecialidadesIO()
        {
            string sql = @"
SELECT id_especialidad, id_estado, id_horario, nombre, codigo
FROM [dbo].[Especialidades] ";
            return _connection.Query<Model.Especialidades>(sql, transaction: _transaction).ToList();
        }

    }
}
