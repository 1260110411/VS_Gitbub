using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 上下翻页
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private string connstr = "Data Source=PXG-PC;Initial Catalog=TEST;uid=sa;pwd=123";
        private int Index;
        private void LoadList()
        {
            try
            {
                string sql = "GetPageList";//存储过程的名称
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //制定命令类型
                    cmd.CommandType = CommandType.StoredProcedure;//指定命令类型为存储过程
                                                                  //根据存储过程来构造参数
                    SqlParameter pageIndex = new SqlParameter("@pageIndex", Index); //@pageIndex中的参数名称必须与存储过程中一致
                    SqlParameter pageSize = new SqlParameter("@pageSize", 3);
                    SqlParameter rowsCount = new SqlParameter("@rowsCount", SqlDbType.Int);
                    rowsCount.Direction = ParameterDirection.Output; //将参数设置为输出
                                                                  //为cmd添加参数
                    cmd.Parameters.Add(pageIndex);
                    cmd.Parameters.Add(pageSize);
                    cmd.Parameters.Add(rowsCount);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<StudentInfo> list = new List<StudentInfo>();
                    while (reader.Read())
                    {
                        list.Add(new StudentInfo()
                        {
                            Sid = Convert.ToInt32(reader["sid"]),
                            SName = reader["sname"].ToString()
                        });
                    }
                    dataGridView1.DataSource = list;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Index = 1;
            LoadList();
        }
    }
}
