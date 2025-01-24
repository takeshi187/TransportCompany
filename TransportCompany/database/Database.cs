using System.Data;
using System.Data.SqlClient;
namespace TransportCompany.database
{
    public class Database
    {
        SqlConnection sqlConnection = new SqlConnection(@"Initial Catalog=TransportCompanyDB;Integrated Security=True");
        public void OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
}
