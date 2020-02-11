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

namespace Auto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true; //显示合成脚本菜单
            tabControl2.Visible = false;
            tabControl3.Visible = false;
            tabControl4.Visible = false;
            tabControl5.Visible = false;
            tabControl1.Dock = DockStyle.Fill;//填充窗体
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false; //显示合成脚本菜单
            tabControl2.Visible = true;
            tabControl3.Visible = false;
            tabControl4.Visible = false;
            tabControl5.Visible = false;
            tabControl2.Dock = DockStyle.Fill;//填充窗体
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false; //显示合成脚本菜单
            tabControl2.Visible = false;
            tabControl3.Visible = true;
            tabControl4.Visible = false;
            tabControl5.Visible = false;
            tabControl3.Dock = DockStyle.Fill;//填充窗体
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false; //显示合成脚本菜单
            tabControl2.Visible = false;
            tabControl3.Visible = false;
            tabControl4.Visible = true;
            tabControl5.Visible = false;
            tabControl4.Dock = DockStyle.Fill;//填充窗体

            string sql = "select * from JDSoft_MES_Problem;";
            DataTable rows = ConnectSQL.Reader(sql);
            dataGridView1.DataSource = rows;
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false; //显示合成脚本菜单
            tabControl2.Visible = false;
            tabControl3.Visible = false;
            tabControl4.Visible = false;
            tabControl5.Visible = true;
            tabControl5.Dock = DockStyle.Fill;//填充窗体
        }

        private void pictureBox1_Click(object sender, EventArgs e) //最小化
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                pictureBox1.BackgroundImage = Properties.Resources.展开;
            }
            else if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                pictureBox1.BackgroundImage = Properties.Resources.最小化;
            }
        }
        
      

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveForm.ReleaseCapture();
            MoveForm.SendMessage(this.Handle, MoveForm.WM_SYSCOMMOND, MoveForm.SC_MOVE + MoveForm.HTCAPTION, 0);
        }

        private void Form1_SizeChanged(object sender, EventArgs e) //隐藏到托盘
        {
            if (this.WindowState == FormWindowState.Normal)
                notifyIcon1.Visible = true;
            else if(this.WindowState==FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) //打开托盘图标
        {
            if(this.WindowState==FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1_MouseDoubleClick(null, null);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
                Application.ExitThread();
            else
                this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e) //关闭
        {
            this.WindowState = FormWindowState.Minimized;
            pictureBox1.BackgroundImage = Properties.Resources.展开;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = false; 
            tabControl2.Visible = false;
            tabControl3.Visible = false;
            tabControl4.Visible = false;
            tabControl5.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Create_Problem cp = new Create_Problem();
            cp.Show();
        }

        private void button5_Click(object sender, EventArgs e) //刷新
        {
            string sql = "select * from JDSoft_MES_Problem;";
            DataTable rows = ConnectSQL.Reader(sql);
            dataGridView1.DataSource = rows;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql;
            if (comboBox1.SelectedIndex<0 & textBox1.Text.ToString().Trim() != "" & textBox2.Text.ToString().Trim() != "" & textBox3.Text.ToString().Trim() != "")
            {
                sql = "select * from JDSoft_MES_Problem where 提出时间 like '%" + textBox1.Text.ToString().Trim() + "%' and 解决时间 like '%" + textBox2.Text.ToString().Trim() +
                     "%' and 关闭时间 like '%" + textBox3.Text.ToString().Trim() + "%'";
            }
            else if(comboBox1.SelectedIndex < 0 & textBox1.Text.ToString().Trim() == "" & textBox2.Text.ToString().Trim() != "" & textBox3.Text.ToString().Trim() != "")
            {
                sql = "select * from JDSoft_MES_Problem where 解决时间 like '%" + textBox2.Text.ToString().Trim() +
                     "%' and 关闭时间 like '%" + textBox3.Text.ToString().Trim() + "%'";
            }
            else if(comboBox1.SelectedIndex < 0 & textBox1.Text.ToString().Trim() == "" & textBox2.Text.ToString().Trim() == "" & textBox3.Text.ToString().Trim() != "")
            {
                sql = "select * from JDSoft_MES_Problem where 关闭时间 like '%" + textBox3.Text.ToString().Trim() + "%'";
            }
            else if(comboBox1.SelectedIndex < 0 & textBox1.Text.ToString().Trim() == "" & textBox2.Text.ToString().Trim() == "" & textBox3.Text.ToString().Trim() == "")
            {
                sql = "select * from JDSoft_MES_Problem";
            }
            else
            {
               sql = "select * from JDSoft_MES_Problem where 解决状态 like '%" + comboBox1.Items[comboBox1.SelectedIndex].ToString() +
                         "%' and 提出时间 like '%" + textBox1.Text.ToString().Trim() + "%' and 解决时间 like '%" + textBox2.Text.ToString().Trim() +
                         "%' and 关闭时间 like '%" + textBox3.Text.ToString().Trim() + "%'";
            }
            
            DataTable rows = ConnectSQL.Reader(sql);
            dataGridView1.DataSource = rows;
        }

        private void button2_Click(object sender, EventArgs e) //修改
        {
            Create_Problem cp = new Create_Problem();
            cp.Text = "修改现场问题";
            cp.Show();

        }

        private void button3_Click(object sender, EventArgs e)//复制
        {
            Create_Problem cp = new Create_Problem();
            cp.Text = "复制现场问题";
            cp.Show();
        }

        private void button4_Click(object sender, EventArgs e) //删除
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int rows = ConnectSQL.Execute("delete from JDSoft_MES_Problem where 任务编号=@任务编号", new SqlParameter("@任务编号", id));
                MessageBox.Show("删除成功" + rows + "行");
            }
            catch
            {
                MessageBox.Show("未选中行！");
            }
           
            button5_Click(null, null);//刷新
        }
    }
}
