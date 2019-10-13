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

namespace WindowsReminderService
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
        /// 定时器中定时执行的任务数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            log("the timer");
            string AlertTxt = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\AlertTxt\Alert.txt"); //获取当前路径：AppDomain.CurrentDomain.BaseDirectory用于类
                                                                                                                          // Application.StartupPath用于Winform
            Interop.ShowMessageBox(AlertTxt, "待办事项提醒");
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
