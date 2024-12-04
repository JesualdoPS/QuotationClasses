using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Calc
{
    public class SQLConnection
    {
        private readonly string connectionString;

        public SQLConnection()
        {
            //connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            connectionString = "Data Source=DESKTOP-A7TJSGV;Initial Catalog=dbCalculator;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public SqlConnection Connect()
        {
            return new SqlConnection(connectionString);
        }
    }
}
