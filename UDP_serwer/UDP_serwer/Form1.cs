using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace UDP_serwer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StreamServer : Form
    {
        private UdpServer server;

        /// <summary>
        /// 
        /// </summary>
        public StreamServer()
        {
            server = new UdpServer();
            server.Message += new MessageHandler(Message_Received);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                server.Stop();
            }
            catch { }
        }

        void Message_Received(string message)
        {
            Invoke((MethodInvoker)delegate { this.Log(message); });
        }

        private void Log(string log)
        {
            textBoxLog.AppendText(log + Environment.NewLine);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textBoxSenderIP.Text != "")
            {
                try
                {
                    server.Start(textBoxSenderIP.Text);
                }
                catch { Log("Nie można ustawić adresu klienta nadającego stream."); }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            server.Stop();
        }

        private void buttonDodajAdres_Click(object sender, EventArgs e)
        {
            server.AddClientIP(textBoxDodajAdres.Text);
        }
    }
}
