using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 断开式数据操作
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region 原始
            //string sql = "select * from StudentInfo";
            //using (SqlConnection conn = new SqlConnection("Data Source=PXG-PC;Initial Catalog=TEST;uid=sa;pwd=123"))
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            //    DataTable table = new DataTable();
            //    sda.Fill(table);
            //    dataGridView1.DataSource = table;
            //    conn.Close();
            //}
            #endregion
            LoadList();

        }
        private void LoadList()
        {
            #region 封装
            string sql = "select * from StudentInfo inner join ClassInfo on StudentInfo.cid=ClassInfo.cid";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;

            #endregion
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex==2)
            {
                if (Convert.ToBoolean(e.Value))
                {
                    e.Value = "男";
                }
                else
                {
                    e.Value = "女";
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdd formAdd =new FormAdd();
            formAdd.freshForm += LoadList;//3.在接收消息的类型中为事件添加方法
            formAdd.Text = "添加";
            formAdd.Show();
        }
        public event Action<int> ShowStudent;
        private void alterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            FormAdd formAdd = new FormAdd();
            formAdd.freshForm += LoadList;
            ShowStudent += formAdd.ShowInfo;
            formAdd.Text = " 修改";
            formAdd.Show();

            ShowStudent(id); //发布显示内容的消息
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string sql = "delete from StudentInfo where StudentInfo.sid=" + id;
            SqlHelper.ExecuteNonQuery(sql);
            LoadList();
        }
    }
}
