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

namespace 自动合成
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
            string fileName = "------------------------------" + listBox1.Text + "-------------------------------------" + "\r\n";
            //if (path[listBox1.SelectedIndex] == "") //判断选中的文件名的文件路径是否为空
            //{
            //    MessageBox.Show("请选择脚本！");
            //    return;
            //}
            File.SetAttributes(path[listBox1.SelectedIndex], FileAttributes.Normal);  //去除文件的只读属性
                                                                                      // FileStream fsRead = new FileStream(path[listBox1.SelectedIndex], FileMode.Open, FileAccess.ReadWrite);
                                                                                      // StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.GetEncoding(54936));//简体中文gb2312
                                                                                      //StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.GetEncoding(65001));//UTF-8
            Encoding end = GetCode(path[listBox1.SelectedIndex], Encoding.Default);
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
        /// <summary>
        /// 打开按钮
        /// </summary>
        static string[] path;  //存储文本路径
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<string> listName = new List<string>(); //存储文本名称
            //读取到列表
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
                //listName.Add(path[i]);
            }
        }
        /// <summary>
        /// 自动合成的打开按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //static string[] DMembersAll = { "马渊宙", "曹县林", "张亚雄", "葛广超" }; //程序员成员名称

        private void button1_Click(object sender, EventArgs e)
        {
            string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\合成的脚本"; //获取桌面路径

            if (!Directory.Exists(DesktopPath)) //判断桌面是否存在文件夹“合成的脚本”
            {
                Directory.CreateDirectory(DesktopPath);
            }
            else if (Directory.GetFiles(DesktopPath).Length +
                Directory.GetDirectories(DesktopPath).Length != 0) //判断桌面文件夹“合成的脚本”是否为空,判断是否有文件及文件夹
            {
                // 删除当前文件夹下所有文件
                foreach (string strFile in Directory.GetFiles(DesktopPath))
                {
                    File.Delete(strFile);
                }
                // 删除当前文件夹下所有子文件夹(递归)
                foreach (string strDir in Directory.GetDirectories(DesktopPath))
                {
                    Directory.Delete(strDir, true);
                }
            }
            List<string> listName = new List<string>(); //存储文本名称
            //读取到列表
            listBox1.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择脚本文件";
            //ofd.InitialDirectory = @"c:\";
            ofd.Multiselect = true;
            ofd.Filter = "脚本文件|*.sql|文本文件|*.txt|所有文件|*.*";
            ofd.ShowDialog();
            path = ofd.FileNames;//获得选择的所有文件的全路径
            #region 测试代码-解析文件名并自动合并
            //List<string> DMembers=new List<string>();//存储脚本中提取到的所有开发名
            //for (int i = 0; i < path.Length; i++)
            //{
            //    //将所有的文件名加载到listbox中
            //    listName.Add(Path.GetFileName(path[i]));  //将需要的文件名添加到list中
            //    listBox1.Items.Add(listName[i]);
            //    try
            //    {
            //        string ComplexSQL = DesktopPath + "\\" + textBox1.Text + "_" + listName[i].Split('_')[1] + ".sql"; //合成脚本的名称
            //        if (ExitMember(DMembers.ToArray(), listName[i].Split('_')[1]) )  //判断是否存在姓名
            //        {
            //            DMembers.Add(listName[i].Split('_')[1]); //提取脚本名称中姓名字段
            //            //创建对应开发的合成脚本
            //            if (ExitMember(Directory.GetFiles(DesktopPath), ComplexSQL)) //判断需合成的脚本是否存在,不存在则创建该脚本文件
            //            {
            //                FileStream fs1 = new FileStream(ComplexSQL,FileMode.Create, FileAccess.Write); //创建需要写入的文件
            //                fs1.Close();
            //            }
            //            //写入脚本
            //            WriteIntoSql(path[i],ComplexSQL);
            //        }
            //        else
            //        {
            //            if (ExitMember(Directory.GetFiles(DesktopPath), ComplexSQL)) //判断需合成的脚本是否存在,不存在则创建该脚本文件
            //            {
            //                FileStream fs1 = new FileStream(ComplexSQL, FileMode.Create, FileAccess.Write); //创建需要写入的文件
            //                fs1.Close();
            //            }
            //            //写入脚本
            //            WriteIntoSql(path[i], ComplexSQL);
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("存在命名错误的脚本，系统自动跳过该脚本合成!"); //出现几个未解析的脚本则提示几次
            //        continue;
            //    }
            //}


            ////合并后自动加载到listview
            #endregion
            #region 读取脚本并记录状态到DataTable
      
            //使用DataTable存储开发人员及其脚本关系
            DataTable tblDatas = new DataTable("ListNameSQL");
            DataColumn dc = null;

            dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;//自动增加
            dc.AutoIncrementSeed = 1;//起始为1
            dc.AutoIncrementStep = 1;//步长为1
            dc.AllowDBNull = false;//
            dc = tblDatas.Columns.Add("DNames", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("WaitComplexSQLPath", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("WaitComplexSQLOrder", Type.GetType("System.String"));

            //string[][] ListNameSQL; //存储合成脚本名和待合成脚本关系
            for (int i = 0; i < path.Length; i++)
            {
                listName.Add(Path.GetFileName(path[i]));  //将需要的文件名添加到list中
                listBox1.Items.Add(listName[i]); //将脚本添加到listbox
                DataRow newRow;
                newRow = tblDatas.NewRow();
                try
                {
                    newRow["DNames"] = listName[i].Split('_')[1];
                    newRow["WaitComplexSQLPath"] = path[i];
                    newRow["WaitComplexSQLOrder"] = int.Parse(Path.GetFileName(path[i]).Split('.')[0]); //存储脚本顺序
                    tblDatas.Rows.Add(newRow); //将该路径加入对应开发的合成脚本列表中
                }
                catch
                {
                    MessageBox.Show("存在命名错误的脚本，系统自动跳过该脚本合成!");
                }
                
            }
            #endregion

            #region 根据DataTable排序并进行脚本合成
            tblDatas.DefaultView.Sort = "DNames ASC,WaitComplexSQLOrder ASC"; //根据姓名和脚本顺序排序
            tblDatas = tblDatas.DefaultView.ToTable();//将排序后的Datatable重新输出
            
            string ComplexSQL = null;
            for (int i = 0; i < tblDatas.Rows.Count; i++)
            {
                if (Convert.ToInt32(tblDatas.Rows[i][3])==1) //判断脚本顺序是否等于1，等于1则需创建合成后的脚本文件
                {
                    ComplexSQL = DesktopPath + "\\" + textBox1.Text + "_" + tblDatas.Rows[i][1].ToString()+ ".sql";

                    comboBox1.Items.Add(ComplexSQL);//合并后自动加载到comboBox1

                    FileStream fs1 = new FileStream(ComplexSQL, FileMode.Create, FileAccess.Write); //创建需要写入的文件
                    fs1.Close();
                    WriteIntoSql(tblDatas.Rows[i][2].ToString(), ComplexSQL);
                }
                else
                {
                    WriteIntoSql(tblDatas.Rows[i][2].ToString(), ComplexSQL);
                }
            }
            #endregion
            comboBox1.SelectedIndex = 0;//默认加载列表第一个合成脚本
        }

        /// <summary>
        /// 判断数组中是否存在某成员
        /// </summary>
        /// <param name="Members"></param>
        /// <param name="Member"></param>
        /// <returns></returns>
        public static bool ExitMember(string[] Members,string Member)
        {
            int flag = 0;
            int a = Members.Length;
            if (Members.Length!=0)
            {
                foreach (var item in Members)
                {
                    if (item == Member)
                    {
                        flag = 1;
                    }
                }
            }
            if(flag==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void WriteIntoSql(string OriginalFileName,string ComplexFilePath)
        {
            //获得了用户打开文件的文件名
            string fileName = "\r\n\r\n"+"------------------------------" + Path.GetFileName(OriginalFileName)
                + "-------------------------------------" + "\r\n"+ "\r\n\r\n";

            File.SetAttributes(OriginalFileName, FileAttributes.Normal);  //去除文件的只读属性
            // FileStream fsRead = new FileStream(path[listBox1.SelectedIndex], FileMode.Open, FileAccess.ReadWrite);
            // StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.GetEncoding(54936));//简体中文gb2312
            //StreamReader Strsw = new StreamReader(path[listBox1.SelectedIndex], Encoding.GetEncoding(65001));//UTF-8
            Encoding end = GetCode(OriginalFileName, Encoding.Default);
            if (checkBox1.Checked == false)
            {
                StreamReader Strsw = new StreamReader(OriginalFileName, Encoding.UTF8);  //读取未合成的脚本
                string SQL=fileName+Strsw.ReadToEnd()+ "\r\n\r\n /****************单次分割线  " + "  *************/ \r\n\r\n"; //读脚本文件并添加分割线
                StreamWriter sw = new StreamWriter(ComplexFilePath,true,Encoding.UTF8); //写入合成后的脚本
                sw.Write(SQL);
                sw.Close();
                Strsw.Close();
            }
            else
            {
                StreamReader Strsw = new StreamReader(OriginalFileName, end);
                string SQL = fileName +Strsw.ReadToEnd()+"\r\n\r\n /****************单次分割线  " + "  *************/ \r\n\r\n";
                StreamWriter sw = new StreamWriter(ComplexFilePath, true, end); //写入合成后的脚本
                sw.Write(SQL);
                sw.Close();
                Strsw.Close();
                Strsw.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Encoding end = GetCode(comboBox1.Text, Encoding.Default);
            if (checkBox1.Checked == false)
            {
                StreamReader Strsw = new StreamReader(comboBox1.Text, Encoding.UTF8);
                textBox2.Text = Strsw.ReadToEnd();
                Strsw.Close();
            }
            else
            {
                StreamReader Strsw = new StreamReader(comboBox1.Text,end);
                textBox2.Text = Strsw.ReadToEnd();
                Strsw.Close();
            }
        }
    }
}
