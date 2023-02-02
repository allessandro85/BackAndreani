using System.Data;

namespace Repository
{
    public abstract class IO
    {
		protected private readonly IDbConnection _connection;
		protected private IDbTransaction _transaction;
		public void SetTransaction(IDbTransaction dbTransaction) => _transaction = dbTransaction;

		protected IO(IDbConnection connection, IDbTransaction transaction)
		{
			_connection = connection;
			_transaction = transaction;
		}

	}
}
