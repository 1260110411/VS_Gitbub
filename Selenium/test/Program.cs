using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using Microsoft.Web.Administration;
using System.Windows.Forms;
using Microsoft.Web.Administration;
using System.DirectoryServices;
using System.Management;
using System.Reflection;
using test;

namespace test
{

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Console.Write("请输入指定的文件路径(请拖拽文件到此处)：");
                //string path = Console.ReadLine();
                string path = @"C:\Users\PXG\Desktop\data2.xml";
                PrintFileVersionInfo(path);
            }

            //Program1 app = new Program1();
            //app.CreateXmlFile();
            #region ceshi
            //string[] strs = new string[2];
            //string msg = "";
            ////待解压的文件
            //strs[0] = @"C:\Users\PXG\Desktop\Debug.7z";
            ////解压后放置的目标文件
            //strs[1] = @"C:\Users\PXG\Desktop\";
            //ZipFloClass uzc = new ZipFloClass();
            //msg = ZipFloClass.UnRarOrZip(strs[1], strs[0], true, "").ToString();
            //MessageBox.Show("信息：" + msg);


            //uzc.ZipFile(@"C:\Users\PXG\Desktop\待办事项提醒\", @"C:\Users\PXG\Desktop\测试的压缩包.zip");
            //string msg = "";
            //uzc.unZipFile(@"C:\Users\PXG\Desktop\待办事项提醒.zip", @"C:\Users\PXG\Desktop\",ref msg);


            //Console.WriteLine(a);
            // Console.ReadKey();

            //string[] strs = new string[2];
            ////待压缩文件目录
            //strs[0] = @"C:\Users\PXG\Desktop\待办事项提醒\";
            ////压缩后的目标文件
            //strs[1] = @"C:\Users\PXG\Desktop\测试的压缩包.zip";
            //ZipFloClass zc = new ZipFloClass();
            //zc.ZipFile(strs[0], strs[1]);

            //将程序集加载到应用程序域中
            // Use the file name to load the assembly into the current
            // application domain.
            ////Assembly a = Assembly.Load(@"test");
            ////// Get the type to use.
            ////Type myType = a.GetType("test");
            ////// Get the method to call.
            ////MethodInfo myMethod = myType.GetMethod("Main");
            ////// Create an instance.
            ////object obj = Activator.CreateInstance(myType);
            ////// Execute the method.
            ////myMethod.Invoke(obj, null);
            //从应用程序域中检索安装信息
            // Create the new application domain.
            //AppDomain domain = AppDomain.CreateDomain("MyDomain", null);

            //// Output to the console.
            //Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            //Console.WriteLine("New domain: " + domain.FriendlyName);
            //Console.WriteLine("Application base is: " + domain.BaseDirectory);
            //Console.WriteLine("Relative search path is: " + domain.RelativeSearchPath);
            //Console.WriteLine("Shadow copy files is set to: " + domain.ShadowCopyFiles);
            //AppDomain.Unload(domain);
            //AppDomainSetup.ApplicationBase 属性
            //AppDomain root = AppDomain.CurrentDomain;

            //AppDomainSetup setup = new AppDomainSetup();
            //setup.ApplicationBase =
            //    root.SetupInformation.ApplicationBase + @"MyAppSubfolder\";

            //AppDomain domain = AppDomain.CreateDomain("MyDomain", null, setup);

            //Console.WriteLine("Application base of {0}:\r\n\t{1}",
            //    root.FriendlyName, root.SetupInformation.ApplicationBase);
            //Console.WriteLine("Application base of {0}:\r\n\t{1}",
            //    domain.FriendlyName, domain.SetupInformation.ApplicationBase);

            //AppDomain.Unload(domain);
            //配置应用程序域
            //Console.WriteLine("Creating new AppDomain.");
            //AppDomain domain = AppDomain.CreateDomain("MyDomain");

            //Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            //Console.WriteLine("child domain: " + domain.FriendlyName);
            //try
            //{
            //    AppDomain.Unload(domain);
            //    Console.WriteLine();
            //    Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            //    // The following statement creates an exception because the domain no longer exists.
            //    Console.WriteLine("child domain: " + domain.FriendlyName);
            //}
            //catch (AppDomainUnloadedException e)
            //{
            //    Console.WriteLine(e.GetType().FullName);
            //    Console.WriteLine("The appdomain MyDomain does not exist.");
            //}
            #endregion
            //Console.ReadKey();
        }
        /// <summary>
        /// 打印指定文件的详细信息
        /// </summary>
        /// <param name="path">指定文件的路径</param>
        static void PrintFileVersionInfo(string path)
        {
            System.IO.FileInfo fileInfo = null;
            try
            {
                fileInfo = new System.IO.FileInfo(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // 其他处理异常的代码
            }
            // 如果文件存在
            if (fileInfo != null && fileInfo.Exists)
            {
                System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(path);
                Console.WriteLine("文件名称=" + info.FileName);
                Console.WriteLine("产品名称=" + info.ProductName);
                Console.WriteLine("公司名称=" + info.CompanyName);
                Console.WriteLine("文件版本=" + info.FileVersion);
                Console.WriteLine("产品版本=" + info.ProductVersion);
                // 通常版本号显示为「主版本号.次版本号.生成号.专用部件号」
                Console.WriteLine("系统显示文件版本：" + info.ProductMajorPart + '.' + info.ProductMinorPart + '.' + info.ProductBuildPart + '.' + info.ProductPrivatePart);
                Console.WriteLine("文件说明=" + info.FileDescription);
                Console.WriteLine("文件语言=" + info.Language);
                Console.WriteLine("原始文件名称=" + info.OriginalFilename);
                Console.WriteLine("文件版权=" + info.LegalCopyright);

                Console.WriteLine("文件大小=" + System.Math.Ceiling(fileInfo.Length / 1024.0) + " KB");
            }
            else
            {
                Console.WriteLine("指定的文件路径不正确!");
            }
            // 末尾空一行
            Console.WriteLine();
        }
    }
}
