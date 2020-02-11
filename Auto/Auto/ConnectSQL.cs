using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto
{
    class ConnectSQL
    {
        public static string connstring = "Data Source=PXG-PC;Initial Catalog=JDSOFT_Auto;User ID=sa;Password=123;";
        /// <summary>
                    /// 完成增、删、查、改
                    /// </summary>
                    /// <param name="sql"></param>
                    /// <param name="ps"></param>
                    /// <returns></returns>
        public static int Execute(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //指定参数
                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行查询，返回sqldatareader，一定要关闭
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static SqlDataReader Reader(string sql, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            //指定参数
            if (ps != null)
            {
                cmd.Parameters.AddRange(ps);
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 查询数据库返回Datatable
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns></returns>
        public static DataTable Reader(string sql)
        {
            using (System.Data.SqlClient.SqlConnection connection = new SqlConnection(connstring))
            {
                //强大的SqlDataAdapter 
                System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();
                //Fill 方法会执行一系列操作 connection.open command.reader 等等
                //反正到最后就把 sql语句执行一遍,然后把结果集插入到 ds 里.
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }
    }
}
