using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 脚本合成
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 列表显示
        /// </summary>
        //判断编码格式
        Encoding edn = Encoding.Default; //存储文本文件的读取、存储类型
        Encoding edn1 = Encoding.Default; //存储文本文件标题的读取、存储类型
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //获得了用户打开文件的文件名
            string fileName = "------------------------------" + listBox1.Text + "-------------------------------------"+ "\r\n";
            //if (path[listBox1.SelectedIndex] == "") //判断选中的文件名的文件路径是否为空
            //{
            //    MessageBox.Show("请选择脚本！");
            //    return;
            //}
            File.SetAttributes(path[listBox1.SelectedIndex], FileAttributes.Normal);  //去除文件的只读属性
           // FileStream fsRead = new FileStream(path[listBox1.SelectedIndex], FileMode.Open, FileAccess.ReadWrite);
           // StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.GetEncoding(54936));//简体中文gb2312
            //StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.GetEncoding(65001));//UTF-8
            Encoding end=GetCode(path[listBox1.SelectedIndex], Encoding.Default);
            if (checkBox1.Checked == false)
            {
                StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.UTF8);
                textBox2.AppendText("\r\n\r\n" + fileName + "\r\n\r\n" +
                        Strsw.ReadToEnd() +
                        "\r\n\r\n /****************单次分割线  " + "  *************/ \r\n\r\n");

                Strsw.Close();
            }
            else
            {
                StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], end);
                textBox2.AppendText("\r\n\r\n" + fileName + "\r\n\r\n" +
                        Strsw.ReadToEnd() +
                        "\r\n\r\n /****************单次分割线  " + "  *************/ \r\n\r\n");

                Strsw.Close();
            }

        }
        /// <summary>
        /// 打开按钮
        /// </summary>
        List<string> listName = new List<string>(); //存储文本名称
        string[] path;  //存储文本路径
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择脚本文件";
            //ofd.InitialDirectory = @"c:\";
            ofd.Multiselect = true;
            ofd.Filter = "脚本文件|*.sql|文本文件|*.txt|所有文件|*.*";
            ofd.ShowDialog();
            path = ofd.FileNames;//获得选择的所有文件的全路径
            for (int i = 0; i < path.Length; i++)
            {
                //将所有的文件名加载到listbox中
                listBox1.Items.Add(Path.GetFileName(path[i]));
                //将脚本文件的全路径存储到泛型集合中
                listName.Add(path[i]);

            }

            
        }
        public static Encoding GetCode(string fileName, Encoding defaultEncoding)
        {
            FileStream fs = File.Open(fileName, FileMode.Open);
            Encoding targetEncoding = GetEncoding(fs, defaultEncoding);
            fs.Close();
            return targetEncoding;
        }

        public static Encoding GetEncoding(FileStream fs, Encoding defaultEncoding)
         {
             Encoding targetEncoding = defaultEncoding;
             if (fs != null && fs.Length >= 2)
             {
                 byte b1 = 0;
                 byte b2 = 0;
                 byte b3 = 0;
                 byte b4 = 0;
 
                 long oriPos = fs.Seek(0, SeekOrigin.Begin);
                 fs.Seek(0, SeekOrigin.Begin);
 
                 b1 = Convert.ToByte(fs.ReadByte());
                 b2 = Convert.ToByte(fs.ReadByte());
                 if (fs.Length > 2)
                 {
                     b3 = Convert.ToByte(fs.ReadByte());
                 }
                 if (fs.Length > 3)
                 {
                     b4 = Convert.ToByte(fs.ReadByte());
                 }
 
                 //根据文件流的前4个字节判断Encoding
                 //Unicode {0xFF, 0xFE};
                 //BE-Unicode {0xFE, 0xFF};
                 //UTF8 = {0xEF, 0xBB, 0xBF};
                 if (b1 == 0xFE && b2 == 0xFF)//UnicodeBe
                 {
                     targetEncoding = Encoding.BigEndianUnicode;
                 }
                 if (b1 == 0xFF && b2 == 0xFE && b3 != 0xFF)//Unicode
                 {
                     targetEncoding = Encoding.Unicode;
                 }
                 if (b1 == 0xEF && b2 == 0xBB && b3 == 0xBF)//UTF8
                 {
                     targetEncoding = Encoding.UTF8;
                 }
 
                 fs.Seek(0, SeekOrigin.Begin);
             }
             fs.Close();
             return targetEncoding;
         }
     

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.InitialDirectory = @"C:";
            sfd.Title = "请选择要保存的文件路径";
            sfd.Filter = "数据库脚本|*.sql|文本文件|*.txt|所有文件|*.*";
            sfd.RestoreDirectory = true; //对话框记忆之前保存的目录
            sfd.ShowDialog();
            //获得用户要保存的文件的路径
            string path = sfd.FileName;
            if (path == "") //判断文件路径是否为空
            {
                return;
            }
            using (FileStream fsWrite = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fsWrite, Encoding.UTF8);
                sw.Write(textBox2.Text);
                sw.Close();
            }
            
            MessageBox.Show("保存成功！");
        }

        private void 清理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
