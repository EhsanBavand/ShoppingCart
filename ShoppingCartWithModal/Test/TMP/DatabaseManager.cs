using MySql.Data.MySqlClient;
using System.Web.Configuration;

namespace SShoppingCart.TMP
{
    public class DatabaseManager
    {
        public static MySqlConnection GetOpenConnection()
        {
            var connection = new MySqlConnection(WebConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();
            return connection;
        }
    }
}