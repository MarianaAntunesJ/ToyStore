using System.Data;
using System.Data.SqlClient;

namespace ToyStore.DAL
{
    class Connection
    {
        private SqlConnection _connection;

        public Connection()
        {
            string cadeiaConexao = @"Data Source=DESKTOP-EBARGUA\SQLEXPRESS;Initial Catalog=ToyStore;Integrated Security=True";
            _connection = new SqlConnection(cadeiaConexao);
            _connection.Open();
        }

        public void Disconnect()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public SqlConnection ReturnConnection()
        {
            return _connection;
        }
    }
}
