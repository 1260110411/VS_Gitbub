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

namespace 断开式数据操作
{
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
        }
        public event FreshForm freshForm; //2.定义委托类型的事件
        private void FormAdd_Load(object sender, EventArgs e)
        {
            string sql = "select * from classinfo";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            cboclassinfo.DisplayMember = "CTitle";
            cboclassinfo.ValueMember = "Cid";
            cboclassinfo.DataSource = dt;
        }
        private int selectsid;
        public void ShowInfo(int sid)
        {
            selectsid = sid;
            string sql = "select * from studentinfo where sid=" + sid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql))
            {
                if(reader.Read())
                {
                    textBox1.Text = reader["SName"].ToString();
                    if(Convert.ToBoolean(reader["sGender"]))
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    dtpBirthday.Value = Convert.ToDateTime(reader["sBirthday"]);
                    textBox2.Text = reader["sphone"].ToString();
                    textBox3.Text = reader["semail"].ToString();
                    cboclassinfo.SelectedValue = reader["cid"];
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "";
            if(this.Text=="添加")
            {
                sql = "insert into studentinfo(sName,sGender,sBirthday,sPhone,sEMail,cid) values(@name,@gender,@birthday,@phone,@email,@cid)";
            }
            else
            {
                sql = "update studentinfo set sname=@name,sgender=@gender,sbirthday=@birthday,sphone=@phone,semail=@email,cid=@cid where sid="+ selectsid;
            }
            
            SqlParameter[] ps =
            {
                new SqlParameter("@name",textBox1.Text),
                new SqlParameter("@gender",radioButton1.Checked),
                new SqlParameter("@birthday",dtpBirthday.Value),
                new SqlParameter("@phone",textBox2.Text),
                new SqlParameter("@email",textBox3.Text),
                new SqlParameter("@cid",cboclassinfo.SelectedValue),
            };
            int result = SqlHelper.ExecuteNonQuery(sql, ps);
            if(result>0)
            {
                freshForm();
                this.Close();
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}
