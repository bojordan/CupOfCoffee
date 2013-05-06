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
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        public Form1()
        {
            //this.Visible = true;
            InitializeComponent();

            this.timer1.Start();
            this.Menu.Items["Start"].Text = "Stop";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            this.label1.Text = "Ticked last at: " + DateTime.Now.ToLongTimeString();
            this.Menu.Items["Ticked"].Text = string.Format("Ticked: {0}", DateTime.Now.ToLongTimeString());
        }

        private void Start_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Text == "Start")
            {
                this.timer1.Start();
                item.Text = "Stop";
            }
            else
            {
                this.timer1.Stop();
                item.Text = "Start";
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
