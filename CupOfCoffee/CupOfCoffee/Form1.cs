using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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

        private Icon _iconOn = null;
        private Icon _iconOff = null;

        private bool _isOn = false;

        public Form1()
        {
            //this.Visible = true;
            InitializeComponent();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            _iconOn = (Icon)resources.GetObject("notifyIconOn.Icon");
            _iconOff = (Icon)resources.GetObject("notifyIconOff.Icon");
            this.ToggleKeepAwakeState();
            this.notifyIcon1.Visible = true;
        }

        private void ToggleKeepAwakeState()
        {
            if (_isOn)
            {
                this.Menu.Items["Start"].Text = "Start";
                this.notifyIcon1.Icon = _iconOff;
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
            else
            {
                this.Menu.Items["Start"].Text = "Stop";
                this.notifyIcon1.Icon = _iconOn;
                SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            }
            _isOn = !_isOn;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.ToggleKeepAwakeState();
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ToggleKeepAwakeState();
            }
        }
    }
}
