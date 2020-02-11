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
    public partial class Create_Problem : Form
    {
        public Create_Problem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlParameter[] ps = new SqlParameter[16];
            ps[0] = new SqlParameter("@紧急程度", comboBox1.Items[comboBox1.SelectedIndex].ToString());
            ps[1] = new SqlParameter("@解决状态", comboBox2.Items[comboBox1.SelectedIndex].ToString());
            ps[2] = new SqlParameter("@所属版本", textBox1.Text.ToString());
            ps[3] = new SqlParameter("@问题分类", comboBox3.Items[comboBox1.SelectedIndex].ToString());
            ps[4] = new SqlParameter("@使用终端", comboBox4.Items[comboBox1.SelectedIndex].ToString());
            ps[5] = new SqlParameter("@工作内容", textBox2.Text.ToString());
            ps[6] = new SqlParameter("@提出人", textBox3.Text.ToString());
            ps[7] = new SqlParameter("@记录人", comboBox5.Items[comboBox1.SelectedIndex].ToString());
            ps[8] = new SqlParameter("@责任人", comboBox6.Items[comboBox1.SelectedIndex].ToString());
            ps[9] = new SqlParameter("@提出时间", textBox4.Text.ToString());
            ps[10] = new SqlParameter("@解决时间", textBox9.Text.ToString());
            ps[11] = new SqlParameter("@解决周期", comboBox7.Items[comboBox1.SelectedIndex].ToString());
            ps[12] = new SqlParameter("@关闭时间", textBox5.Text.ToString());
            ps[13] = new SqlParameter("@解决措施", textBox6.Text.ToString());
            ps[14] = new SqlParameter("@进展情况", textBox7.Text.ToString());
            ps[15] = new SqlParameter("@备注说明", textBox8.Text.ToString());

            string sql = "insert into JDSoft_MES_Problem(紧急程度,解决状态,所属版本,问题分类,使用终端,工作内容,提出人,记录人,责任人,提出时间,解决时间,解决周期,关闭时间,解决措施,进展情况,备注说明)" +
                       "values(@紧急程度,@解决状态,@所属版本,@问题分类,@使用终端,@工作内容,@提出人,@记录人,@责任人,@提出时间,@解决时间,@解决周期,@关闭时间,@解决措施,@进展情况,@备注说明);";
            int rows = ConnectSQL.Execute(sql, ps);
            MessageBox.Show("新增成功" + rows + "行");

        }

        private void Create_Problem_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex= 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            textBox3.Text = "李秋霈";
            textBox4.Text = DateTime.Now.ToShortDateString().ToString().ToString();
            textBox5.Text = DateTime.Now.ToShortDateString().ToString().ToString();
            textBox9.Text = DateTime.Now.ToShortDateString().ToString().ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
