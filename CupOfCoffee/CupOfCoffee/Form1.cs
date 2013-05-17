using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace CupOfCoffee
{
    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_USER_PRESENT = 0x00000004
    }

    public partial class Form1 : Form
    {
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa373208(v=vs.85).aspx
        /// </summary>
        /// <param name="esFlags"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        private System.Drawing.Icon _iconOn = null;
        private System.Drawing.Icon _iconOff = null;

        public Form1()
        {
            //this.Visible = true;
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            _iconOn = (Icon)resources.GetObject("notifyIconOn.Icon");
            _iconOff = (Icon)resources.GetObject("notifyIconOn.Icon");
            //_iconOn = Icon.FromHandle(bitmapOn.GetHicon());
            //var bitmapOff = (Bitmap)(resources.GetObject("notifyIconOn.Icon"));
            //_iconOff = Icon.FromHandle(bitmapOff.GetHicon());
            this.Menu.Items["Start"].Text = "Stop";
            this.notifyIcon1.Visible = true;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Text == "Start")
            {
                item.Text = "Stop";
                this.notifyIcon1.Icon = _iconOn;
                SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            }
            else
            {
                item.Text = "Start";
                this.notifyIcon1.Icon = _iconOff;
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
