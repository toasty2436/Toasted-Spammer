using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using Hotkeys;

namespace R6S_Chat_Spammer
{
    public partial class Spammer : Form
    {
        private Hotkeys.GlobalHotkey ghk1;
        private Hotkeys.GlobalHotkey ghk2;

        private Keys GetKey(IntPtr LParam)
        {
            return (Keys)(LParam.ToInt32() >> 16);
        }

        public Spammer()
        {
            InitializeComponent();
            ghk1 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F8, this);
            ghk1.Register();
            ghk2 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F9, this);
            ghk2.Register();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Spammer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void HandleHotkey1()
        {
            Timer.Enabled = true;
        }

        private void HandleHotkey2()
        {
            Timer.Enabled = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
                switch (GetKey(m.LParam))
                {
                    case Keys.F8:
                        HandleHotkey1();
                        break;
                    case Keys.F9:
                        HandleHotkey2();
                        break;
                }
            base.WndProc(ref m);
        }

        private bool isRunning = true;

        private void SpamCity_Load(object sender, EventArgs e)
        {
            // Attached to the key press status
            Thread TH = new Thread(Keyboardd);
            TH.SetApartmentState(ApartmentState.STA);
            CheckForIllegalCrossThreadCalls = false;
            TH.Start();
        }

        // Check the key you press status
        void Keyboardd()
        {
            while (isRunning)
            {
                Thread.Sleep(40);
                if ((Keyboard.GetKeyStates(Key.F8) & KeyStates.Down) > 0)
                {
                    
                }
                if ((Keyboard.GetKeyStates(Key.F9) & KeyStates.Down) > 0)
                {
                    
                }
            }
        }
        
        // Messages from the chat box that will be sent to in-game
        private void Timer_Tick(object sender, EventArgs e)
        {
            
            SendKeys.Send(spamBox.Text);
            SendKeys.Send("{Enter}");
        }
        // Timer rate section
        private void IntervalCounter_ValueChanged(object sender, EventArgs e)
        {
            Timer.Interval = Convert.ToInt16(IntervalCounter.Value);
        }

        // Creater youtube link
        private void HyperLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/kzXNCFzvAS");
        }
        // Safely exits out of program
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Stops the while loop and unregister hotkeys when program is closed for key press status
        private void SpamCity_FormClosed(object sender, FormClosedEventArgs e)
        {
            ghk1.Unregiser();
            ghk2.Unregiser();
            isRunning = false;
        }

        private void SetInitial_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetFinal_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}

