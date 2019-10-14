using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsReminderServiceIII
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Set up a timer that triggers every minute. 设置定时器
            Timer timer = new Timer();
            timer.Interval = 3600000; // 60 seconds 60秒执行一次
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
        }
        /// <summary>
        /// 定时器中定时执行的任务数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            string AlertTxt = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\AlertTxt\Alert.txt"); //获取当前路径：AppDomain.CurrentDomain.BaseDirectory用于类                                                                                                           // Application.StartupPath用于Winform
            Interop.ShowMessageBox(AlertTxt, "待办事项提醒");
            using (SpeechSynthesizer synth = new SpeechSynthesizer())//引用system.speech,语音朗读该文字
            {
                synth.Speak(AlertTxt);
            }
        }
    }
}
