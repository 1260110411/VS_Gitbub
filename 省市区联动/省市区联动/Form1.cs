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

namespace 省市区联动
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string connstr = @"Data Source=PXG-PC;Initial Catalog=TEST;uid=sa;pwd=123";
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProvince();
            LoadCity();
            LoadDistinct();
        }
        private void LoadProvince()
        {
            List<AreaInfo> list = new List<AreaInfo>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string sql = "select * from s_province";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //AreaInfo Areinf = new AreaInfo();
                    //Areinf.Title = reader["ProvinceName"].ToString();
                    //Areinf.Id= Convert.ToInt32(reader["ProvinceId"]);
                    //list.Add(Areinf);
                    list.Add(
                    new AreaInfo()
                    {
                        Id = Convert.ToInt32(reader["ProvinceId"]),
                        Title = reader["ProvinceName"].ToString()
                    }
                    );

                }
            }
            //cboProvince.DisplayMember = "Title";//显示属性
            //cboProvince.ValueMember = "ID";//值属性
            cboProvince.DataSource = list;
        }
        private void LoadCity()
        {
            List<AreaInfo> list = new List<AreaInfo>();
            //int pid = Convert.ToInt32(cboProvince.SelectedValue);
            int pid = (cboProvince.SelectedItem as AreaInfo).Id;
            string sql = "select * from s_city where provinceid=@pid";
            using (SqlConnection conn=new SqlConnection(connstr))
            {
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.Add(new SqlParameter("@pid",pid));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    list.Add(new AreaInfo()
                    {
                        Id = Convert.ToInt32(reader["cityid"]),
                        Title = reader["cityName"].ToString()
                    });
                }

            }
            //cboCity.DisplayMember = "Title";
            //cboCity.ValueMember = "Id";
            cboCity.DataSource = list;
        }
        private void LoadDistinct()
        {
            //int cid = Convert.ToInt32(cboCity.SelectedValue);
            int cid = (cboCity.SelectedItem as AreaInfo).Id;
            string sql = "select * from s_distinct where cityid=@cid";
            List<AreaInfo> list = new List<AreaInfo>();
            using (SqlConnection conn=new SqlConnection(connstr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cid",cid));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    list.Add(new AreaInfo()
                    {
                        Id = Convert.ToInt32(reader["DistinctId"]),
                        Title = reader["DistinctName"].ToString()
                    }
                    );
                }
            }
            //cboDistinct.DisplayMember = "Title";
            //cboDistinct.ValueMember = "Id";
            cboDistinct.DataSource = list;
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity();
            LoadDistinct();
        }

        private void cboCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistinct();
        }
    }
}
