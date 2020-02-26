using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoIT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoIt.AutoItX.Run(@"F:\Xmind\XMind\XMind.exe", @"F:\Xmind\XMind");
            this.WindowState = FormWindowState.Minimized;
            AutoIt.AutoItX.WinActivate("XMind");
            AutoIt.AutoItX.Sleep(8000);
            AutoIt.AutoItX.ControlClick("XMind", "", "[CLASS:ToolbarWindow32; INSTANCE:5]", "left", 1, 23, 21);
            AutoIt.AutoItX.Sleep(5);
            AutoIt.AutoItX.MouseClick("left", 258, 177);
            AutoIt.AutoItX.Sleep(5); 
            AutoIt.AutoItX.ControlClick("选择风格", "新建", "[CLASS:Button; INSTANCE:1]", "left", 1, 44, 12);
            this.WindowState = FormWindowState.Normal;
        }
    }
}
