using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsReminderServiceII
{
    public partial class Service1 : ServiceBase
    {
        //记录到event log中，地址是 C:\Windows\System32\winevt\Logs (双击查看即可，文件名为MyNewLog)
        private static EventLog eventLog1;
        private int eventId = 1;

        public Service1()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart.");
            log("In OnStart.");

            // Set up a timer that triggers every minute. 设置定时器
            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds 60秒执行一次
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
            log("In OnStop.");
        }

        /// <summary>
        /// 继续服务
        /// </summary>
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
            log("In OnContinue.");
        }

        /// <summary>
        /// 定时器中定时执行的任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            log("the timer");
            #region  第二次新增的代码
            //在CreateProcess 函数中同时也涉及到DuplicateTokenEx、WTSQueryUserToken、CreateEnvironmentBlock 函数的使用，
            //有兴趣的朋友可通过MSDN 进行学习。完成CreateProcess 函数创建后，就可以真正的通过它来调用应用程序了，
            //回到Service1.cs 修改一下OnStart 我们来打开一个CMD 窗口。如下代码：
            //学习链接：https://docs.microsoft.com/zh-cn/windows/win32/api/wtsapi32/nf-wtsapi32-wtssendmessagea?redirectedfrom=MSDN
            Interop.CreateProcess("cmd.exe", @"C:\Windows\System32\"); //调用cmd命令
            #endregion

        }



        /// <summary>
        /// 记录到指定路径：D:\log.txt
        /// </summary>
        /// <param name="message"></param>
        private static void log(string message)
        {
            using (FileStream stream = new FileStream("D:\\log.txt", FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{DateTime.Now}:{message}");
            }
        }
    }
}
