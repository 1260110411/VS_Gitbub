using Microsoft.Web.FtpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.DirectoryServices;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Diagnostics;
using System.Xml;

namespace test
{
    public class WindwosUser
    {
        //WinNT用户管理
        //创建NT用户
        //传入参数：Username要创建的用户名，Userpassword用户密码，Path主文件夹路径
        public static bool CreateNTUser(string Username, string Userpassword, string Path)
        {
            DirectoryEntry obDirEntry = null;
            try
            {
                obDirEntry = new DirectoryEntry("WinNT://" + Environment.MachineName);

                DirectoryEntry obUser = obDirEntry.Children.Add(Username, "User"); //增加用户名
                obUser.Properties["FullName"].Add(Username); //用户全称
                obUser.Invoke("SetPassword", Userpassword); //用户密码
                obUser.Invoke("Put", "Description", "Test User from .NET");//用户详细描述
                                                                           //obUser.Invoke("Put","PasswordExpired",1); //用户下次登录需更改密码
                obUser.Invoke("Put", "UserFlags", 66049); //密码永不过期
                obUser.Invoke("Put", "HomeDirectory", Path); //主文件夹路径
                obUser.CommitChanges();//保存用户
                DirectoryEntry grp = obDirEntry.Children.Find("Users", "group");//Users组
                if (grp.Name != "")
                {
                    grp.Invoke("Add", obUser.Path.ToString());//将用户添加到某组
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //删除NT用户
        //传入参数：Username用户名
        public static bool DelNTUser(string Username)
        {
            try
            {
                DirectoryEntry obComputer = new DirectoryEntry("WinNt://" + Environment.MachineName);//获得计算机实例
                DirectoryEntry obUser = obComputer.Children.Find(Username, "User");//找得用户
                obComputer.Children.Remove(obUser);//删除用户
                return true;
            }
            catch
            {
                return false;
            }
        }
        //修改NT用户密码
        //传入参数：Username用户名，Userpassword用户新密码
        public static bool InitNTPwd(string Username, string Userpassword)
        {
            try
            {
                DirectoryEntry obComputer = new DirectoryEntry("WinNt://" + Environment.MachineName);
                DirectoryEntry obUser = obComputer.Children.Find(Username, "User");
                obUser.Invoke("SetPassword", Userpassword);
                obUser.CommitChanges();
                obUser.Close();
                obComputer.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 压缩、解压的类，zip是一种免费开源的压缩格式引用ICSharpCode.SharpZLib.dll程序集；
    /// rar格式是一种具有专利文件的压缩格式，是一种商业压缩格式，不开源，只能调用rar的命令行程序实现解压缩
    /// </summary>
    public class ZipFloClass
    {
        #region 使用开源ICSharpCode.SharpZLib.dll程序集
        /// <summary>
        /// 压缩为zip
        /// </summary>
        /// <param name="strFile">待压缩文件目录</param>
        /// <param name="strZip">压缩后的目标文件</param>
        public void ZipFile(string strFile, string strZip)
        {
            var len = strFile.Length;
            var strlen = strFile[len - 1];
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
            {
                strFile += Path.DirectorySeparatorChar;
            }
            ZipOutputStream outstream = new ZipOutputStream(File.Create(strZip)); //压缩文件输出流
            outstream.SetLevel(6);//压缩标准，一般为6
            zip(strFile, outstream, strFile);
            outstream.Finish();
            outstream.Close();
        }
        /// <summary>
        /// 压缩zip时调用的方法
        /// </summary>
        /// <param name="strFile">待压缩文件目录</param>
        /// <param name="outstream">压缩文件输出流</param>
        /// <param name="staticFile">待压缩文件目录</param>
        public void zip(string strFile, ZipOutputStream outstream, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
            {
                strFile += Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();
            //获取指定目录下所有文件和子目录文件名称
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            //遍历文件
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    zip(file, outstream, staticFile);
                }
                //否则，直接压缩文件
                else
                {
                    //打开文件
                    FileStream fs = File.OpenRead(file);
                    //定义缓存区对象
                    byte[] buffer = new byte[fs.Length];
                    //通过字符流，读取文件
                    fs.Read(buffer, 0, buffer.Length);
                    //得到目录下的文件（比如:D:\Debug1\test）,test
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    outstream.PutNextEntry(entry);
                    //写文件
                    outstream.Write(buffer, 0, buffer.Length);
                }
            }
        }
        /// <summary>
        /// 解压zip文件
        /// </summary>
        /// <param name="TargetFile">待解压的文件</param>
        /// <param name="fileDir">解压后放置的目标文件</param>
        /// <param name="msg">结果信息</param>
        /// <returns></returns>
        public string unZipFile(string TargetFile, string fileDir, ref string msg)
        {
            string rootFile = "";
            msg = "";
            try
            {
                //读取压缩文件（zip文件），准备解压缩
                ZipInputStream inputstream = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                inputstream.Password = "123456";
                ZipEntry entry;
                string path = fileDir;
                //解压出来的文件保存路径
                string rootDir = "";
                //根目录下的第一个子文件夹的名称
                while ((entry = inputstream.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(entry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(entry.Name);
                    //得到根目录下的第一级子文件夹下的子文件夹名称
                    string fileName = Path.GetFileName(entry.Name);
                    //根目录下的文件名称
                    if (dir != "")
                    {
                        //创建根目录下的子文件夹，不限制级别
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == "" && fileName != "")
                    {
                        //根目录下的文件
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != "" && fileName != "")
                    {
                        //根目录下的第一级子文件夹下的文件
                        if (dir.IndexOf("\\") > 0)
                        {
                            //指定文件保存路径
                            path = fileDir + "\\" + dir;
                        }
                    }
                    if (dir == rootDir)
                    {
                        //判断是不是需要保存在根目录下的文件
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压zip文件的基本步骤
                    //基本思路：遍历压缩文件里的所有文件，创建一个相同的文件
                    if (fileName != String.Empty)
                    {
                        FileStream fs = File.Create(path + "\\" + fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = inputstream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                fs.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        fs.Close();
                    }
                }
                inputstream.Close();
                msg = "解压成功！";
                return rootFile;
            }
            catch (Exception ex)
            {
                msg = "解压失败，原因：" + ex.Message;
                return "1;" + ex.Message;
            }
        }
        #endregion
        #region 使用本机的Winrar.exe
        /// <summary>
        /// 判断用户计算机是否安装了winrar压缩工具
        /// </summary>
        /// <returns></returns>
        public static bool ExistsWinRar()
        {
            try
            {
                string result = string.Empty;
                string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
                if (registryKey != null)
                {
                    result = registryKey.GetValue("").ToString();
                    return true;
                }
                registryKey.Close();
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        /// <summary>
        /// 解压RAR和ZIP文件(需存在Winrar.exe(只要自己电脑上可以解压或压缩文件就存在Winrar.exe))
        /// </summary>
        /// <param name="UnPath">解压后文件保存目录</param>
        /// <param name="rarPathName">待解压文件存放绝对路径（包括文件名称）</param>
        /// <param name="IsCover">所解压的文件是否会覆盖已存在的文件(如果不覆盖,所解压出的文件和已存在的相同名称文件不会共同存在,只保留原已存在文件)</param>
        /// <param name="PassWord">解压密码(如果不需要密码则为空)</param>
        /// <returns>true(解压成功);false(解压失败)</returns>
        public static bool UnRarOrZip(string UnPath, string rarPathName, bool IsCover, string PassWord)
        {
            if (!ExistsWinRar())
            {
                return false;//如果系统不存在Winrar直接退出
            }
            if (!Directory.Exists(UnPath))
                Directory.CreateDirectory(UnPath);
            Process Process1 = new Process();
            Process1.StartInfo.FileName = "Winrar.exe";
            Process1.StartInfo.CreateNoWindow = true;
            string cmd = "";
            if (!string.IsNullOrEmpty(PassWord) && IsCover)
                //解压加密文件且覆盖已存在文件( -p密码 )
                cmd = string.Format(" x -p{0} -o+ {1} {2} -y", PassWord, rarPathName, UnPath);
            else if (!string.IsNullOrEmpty(PassWord) && !IsCover)
                //解压加密文件且不覆盖已存在文件( -p密码 )
                cmd = string.Format(" x -p{0} -o- {1} {2} -y", PassWord, rarPathName, UnPath);
            else if (IsCover)
                //覆盖命令( x -o+ 代表覆盖已存在的文件)
                cmd = string.Format(" x -o+ {0} {1} -y", rarPathName, UnPath);
            else
                //不覆盖命令( x -o- 代表不覆盖已存在的文件)
                cmd = string.Format(" x -o- {0} {1} -y", rarPathName, UnPath);
            //命令
            Process1.StartInfo.Arguments = cmd;
            Process1.Start();
            Process1.WaitForExit();//无限期等待进程 winrar.exe 退出
                                   //Process1.ExitCode==0指正常执行，Process1.ExitCode==1则指不正常执行
            if (Process1.ExitCode == 0)
            {
                Process1.Close();
                return true;
            }
            else
            {
                Process1.Close();
                return false;
            }
        }
        /// <summary>
        /// 压缩文件成RAR或ZIP文件(需存在Winrar.exe(只要自己电脑上可以解压或压缩文件就存在Winrar.exe))
        /// </summary>
        /// <param name="filesPath">将要压缩的文件夹或文件的绝对路径</param>
        /// <param name="rarPathName">压缩后的压缩文件保存绝对路径（包括文件名称）</param>
        /// <param name="IsCover">所压缩文件是否会覆盖已有的压缩文件(如果不覆盖,所压缩文件和已存在的相同名称的压缩文件不会共同存在,只保留原已存在压缩文件)</param>
        /// <param name="PassWord">压缩密码(如果不需要密码则为空)</param>
        /// <returns>true(压缩成功);false(压缩失败)</returns>
        public static bool CondenseRarOrZip(string filesPath, string rarPathName, bool IsCover, string PassWord)
        {
            string rarPath = Path.GetDirectoryName(rarPathName);
            if (!Directory.Exists(rarPath))
                Directory.CreateDirectory(rarPath);
            Process Process1 = new Process();
            Process1.StartInfo.FileName = "Winrar.exe";
            Process1.StartInfo.CreateNoWindow = true;
            string cmd = "";
            if (!string.IsNullOrEmpty(PassWord) && IsCover)
                //压缩加密文件且覆盖已存在压缩文件( -p密码 -o+覆盖 )
                cmd = string.Format(" a -ep1 -p{0} -o+ {1} {2} -r", PassWord, rarPathName, filesPath);
            else if (!string.IsNullOrEmpty(PassWord) && !IsCover)
                //压缩加密文件且不覆盖已存在压缩文件( -p密码 -o-不覆盖 )
                cmd = string.Format(" a -ep1 -p{0} -o- {1} {2} -r", PassWord, rarPathName, filesPath);
            else if (string.IsNullOrEmpty(PassWord) && IsCover)
                //压缩且覆盖已存在压缩文件( -o+覆盖 )
                cmd = string.Format(" a -ep1 -o+ {0} {1} -r", rarPathName, filesPath);
            else
                //压缩且不覆盖已存在压缩文件( -o-不覆盖 )
                cmd = string.Format(" a -ep1 -o- {0} {1} -r", rarPathName, filesPath);
            //命令
            Process1.StartInfo.Arguments = cmd;
            Process1.Start();
            Process1.WaitForExit();//无限期等待进程 winrar.exe 退出
                                   //Process1.ExitCode==0指正常执行，Process1.ExitCode==1则指不正常执行
            if (Process1.ExitCode == 0)
            {
                Process1.Close();
                return true;
            }
            else
            {
                Process1.Close();
                return false;
            }
        }
        #endregion
    }
    #region xml
    //public static class XMLCreate
    //{
    //    #region XML元素添加子节点-王雷-2017年4月24日16:26:37  
    //        /// <summary>  
    //        /// XML元素添加子节点-王雷-2017年4月24日16:26:37  
    //        /// </summary>  
    //    public static void AddChildNode(this XmlElement src, XmlDocument doc, string name, string innerText)
    //    {
    //        XmlElement elem = doc.CreateElement(name);
    //        elem.InnerText = innerText;
    //        src.AppendChild(elem);
    //    }
    //    #endregion
    //    #region 创建xml文件-固定的格式-王雷-2017年4月24日16:24:35
    //    /// <summary>
    //    /// 创建xml文件-固定的格式-王雷-2017年4月24日16:24:35
    //    /// </summary>
    //    /// <param name="url">webservice的地址</param>
    //    static void CreateXml(string url)
    //    {
    //        //创建文档对象
    //        XmlDocument doc = new XmlDocument();
    //        //创建根节点
    //        XmlElement root = doc.CreateElement("AutoUpdater");
    //        //头声明
    //        XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
    //        doc.AppendChild(xmldecl);
    //        DirectoryInfo dicInfo = new DirectoryInfo(currentDirectory);

    //        //Updater
    //        XmlElement body1 = doc.CreateElement("Updater");
    //        Tool.AddChildNode(body1, doc, "Url", url);
    //        root.AppendChild(body1);

    //        //Application
    //        XmlElement body2 = doc.CreateElement("Application");
    //        Tool.AddEleAttr(body2, doc, "applicationId", "ItemSoft");
    //        Tool.AddChildNode(body2, doc, "EntryPoint", "ItemSoft");
    //        Tool.AddChildNode(body2, doc, "Location", ".");
    //        root.AppendChild(body2);

    //        //Files
    //        XmlElement body3 = doc.CreateElement("Files");
    //        //调用递归方法组装xml文件
    //        PopuAllDirectory(doc, body3, dicInfo);
    //        root.AppendChild(body3);

    //        //Update
    //        XmlElement body4 = doc.CreateElement("Update");
    //        root.AppendChild(body4);
    //        XmlElement body5 = doc.CreateElement("Soft");
    //        Tool.AddEleAttr(body5, doc, "Name", "BlogWriter");
    //        Tool.AddChildNode(body5, doc, "Verson", Guid.NewGuid().ToString());
    //        body4.AppendChild(body5);

    //        //追加节点
    //        doc.AppendChild(root);
    //        //保存文档
    //        doc.Save(serverXmlName);
    //    }
    //    #endregion
    //    #region XML元素添加属性-王雷-2017年4月24日16:27:46 
    //    /// <summary>  
    //    /// XML元素添加属性-王雷-2017年4月24日16:27:46 
    //    /// </summary>  
    //    public static void AddEleAttr(this XmlElement src, XmlDocument doc, string name, string value)
    //    {
    //        XmlAttribute attr = doc.CreateAttribute(name);
    //        attr.Value = value;
    //        src.Attributes.Append(attr);
    //    }
    //    #endregion
    //    //递归组装xml文件方法
    //    private static void PopuAllDirectory(XmlDocument doc, XmlElement root, DirectoryInfo dicInfo)
    //    {
    //        foreach (FileInfo f in dicInfo.GetFiles())
    //        {
    //            //排除当前目录中生成xml文件的工具文件
    //            if (f.Name != "CreateXmlTools.exe" && f.Name != "AutoupdateService.xml" && f.Name != "AutoUpdate.exe" && f.Name.LastIndexOf(".pdb") == -1 && f.Name != "UpdateList.xml" && f.Name.LastIndexOf(".vshost.exe") == -1 && f.Name.LastIndexOf(".vshost.exe.manifest") == -1)
    //            {
    //                string path = dicInfo.FullName.Replace(currentDirectory, "").Replace("\\", "/");
    //                //path = dicInfo.FullName.Replace(currentDirectory, "").Replace(@"\", "/");
    //                string folderPath = string.Empty;
    //                if (path != string.Empty)
    //                {
    //                    folderPath = path.TrimStart('/') + "/";
    //                }
    //                XmlElement child = doc.CreateElement("File");
    //                child.SetAttribute("Ver", Guid.NewGuid().ToString());
    //                child.SetAttribute("Name", folderPath + f.Name);
    //                root.AppendChild(child);
    //            }
    //        }

    //        foreach (DirectoryInfo di in dicInfo.GetDirectories())
    //            PopuAllDirectory(doc, root, di);
    //    }
    //    private static void ReadXml()
    //    {
    //        string path = "UpdateList.xml";
    //        rtbXml.ReadOnly = true;
    //        if (File.Exists(path))
    //        {
    //            rtbXml.Text = File.ReadAllText(path);
    //        }
    //    }
    //}
    #endregion
    public class XMLCreate
    {
        public XMLCreate(string Path)
        {

        }
    }
    class Program1
    {
        public void CreateXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点
            XmlNode root = xmlDoc.CreateElement("updateFile");
            xmlDoc.AppendChild(root);
            CreateNode(xmlDoc, root, "name", "heshaoqi");
            CreateNode(xmlDoc, root, "sex", "female");
            CreateNode(xmlDoc, root, "age", "23");
            try
            {
                xmlDoc.Save(@"C:\Users\PXG\Desktop\data2.xml");
            }
            catch (Exception e)
            {
                //显示错误信息
                Console.WriteLine(e.Message);
            }
        }
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, value);
            node.InnerText = value;
            parentNode.AppendChild(node);

        }
    }
    
}

