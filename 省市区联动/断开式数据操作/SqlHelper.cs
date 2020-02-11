using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 断开式数据操作
{
    public static partial class SqlHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["dbtest"].ConnectionString;
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps) //params表示可变参数，可以没有
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
        public static DataTable ExecuteTable(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                //用于进行select操作，可以通过selectcommand属性获取此操作的sqlcommand对象
                adapter.SelectCommand.Parameters.AddRange(ps);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static SqlDataReader ExecuteReader(string sql,params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(ps);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);

        }
    }
}
