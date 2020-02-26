using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace test_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtWebUrl.Text = "172.30.100.55:8011";
            txtWebUrl.ForeColor = Color.Gray;
        }
        //获取当前目录
        //string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string currentDirectory = System.Environment.CurrentDirectory;
        //服务端xml文件名称
        string serverXmlName = "AutoupdateService.xml";
        //更新文件URL前缀
        string url = string.Empty;
        private void btnCreate_Click(object sender, EventArgs e)
        {
            url = "http://" + txtWebUrl.Text.Trim();
            CreateXml();
            ReadXml();
        }
        private void ReadXml()
         {
             string path = "AutoupdateService.xml";
             rtbXml.ReadOnly = true;
             if (File.Exists(path))
             {
                 rtbXml.Text = File.ReadAllText(path);
             }
         }
//递归组装xml文件方法
private void PopuAllDirectory(XmlDocument doc, XmlElement root, DirectoryInfo dicInfo)
        {
            foreach (FileInfo f in dicInfo.GetFiles())
            {
                //排除当前目录中生成xml文件的工具文件
                if (f.Name != "CreateXmlTools.exe" && f.Name != "AutoupdateService.xml")
                {
                    string path = dicInfo.FullName.Replace(currentDirectory, "").Replace("\\", "/"); //执行两次全局替换，一次将当前目录去掉，一次将\\改为/
                    string folderPath = string.Empty;
                    if (path != string.Empty)
                    {
                        folderPath = path.TrimStart('/') + "/";//只删除字符串的头部的/
                    }
                    XmlElement child = doc.CreateElement("file");
                    child.SetAttribute("path", folderPath + f.Name);
                    child.SetAttribute("url", url + path + "/" + f.Name);
                    child.SetAttribute("lastver", FileVersionInfo.GetVersionInfo(f.FullName).FileVersion); //获取文件的版本信息
                    child.SetAttribute("size", f.Length.ToString());
                    child.SetAttribute("needRestart", "false");
                    child.SetAttribute("version", Guid.NewGuid().ToString());
                    root.AppendChild(child);
                }
            }

            foreach (DirectoryInfo di in dicInfo.GetDirectories())
                PopuAllDirectory(doc, root, di);
        }
        void CreateXml()
        {
            //创建文档对象
            XmlDocument doc = new XmlDocument();
            //创建根节点
            XmlElement root = doc.CreateElement("updateFiles");
            //头声明
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(xmldecl);
            DirectoryInfo dicInfo = new DirectoryInfo(currentDirectory);

            //调用递归方法组装xml文件
            PopuAllDirectory(doc, root, dicInfo);
            //追加节点
            doc.AppendChild(root);
            //保存文档
            doc.Save(serverXmlName);
        }

        private void txtWebUrl_Enter(object sender, EventArgs e)
        {
            txtWebUrl.ForeColor = Color.Red;
            if (txtWebUrl.Text.Trim() == "172.30.100.55:8011")
            {
                txtWebUrl.Text = string.Empty;
            }
        }
    }
}
