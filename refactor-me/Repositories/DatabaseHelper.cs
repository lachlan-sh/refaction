using System.Data.SqlClient;
using System.Web;

namespace refactor_me.Models
{
    public class DatabaseHelper
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        private static SqlConnection NewConnection()
        {
            var connstr = ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
            return new SqlConnection(connstr);
        }

        public static void ExecuteSql(string sqlToExecute)
        {
            var conn = DatabaseHelper.NewConnection();
            conn.Open();
            var cmd = new SqlCommand(sqlToExecute, conn);
            cmd.ExecuteNonQuery();
        }

        public static SqlDataReader ReadQueryResult(string sqlQuery)
        {
            var conn = DatabaseHelper.NewConnection();
            var cmd = new SqlCommand(sqlQuery, conn);
            conn.Open();

            return cmd.ExecuteReader();
        }
    }
}