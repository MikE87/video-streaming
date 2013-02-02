using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DirectX.Capture;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace UdpStreamClient
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Klient : Form
    {
        private UdpStreamClient streamClient;
        private bool wysyla = false;
        private bool odbiera = false;
        private int[] tt;

        /// <summary>
        /// 
        /// </summary>
        public Klient()
        {
            InitializeComponent();
        }

        private void Klient_Load(object sender, EventArgs e)
        {
            pictureBoxLocalCamera.WaitOnLoad = true;
            streamClient = new UdpStreamClient(pictureBoxLocalCamera, "127.0.0.1");
            toolStripStatusLabel2.Text = streamClient.GetLocalIP();
            tt = new []{50, 56, 63, 72, 83, 100, 125, 167, 250, 500, 1000};
        }

        private void Klient_FormClosing(object sender, FormClosingEventArgs e)
        {
            streamClient.StopRecievingStream();
            streamClient.StopStreaming();
        }

        private void buttonWyslij_Click(object sender, EventArgs e)
        {
            if (wysyla)
            {
                streamClient.StopStreaming();
                buttonStreamuj.Text = "Streamuj";
                buttonOdbieraj.Enabled = true;
                checkBoxKolor.Enabled = true;
                trackBarTimeInterval.Enabled = false;
                wysyla = false;
                timerUpdateFPS.Stop();
            }
            else
            {
                if (textBoxServerIP.Text != "")
                {
                    streamClient.SetRemoteIP(textBoxServerIP.Text);
                }
                else
                {
                    MessageBox.Show("Podaj adres ip serwera.");
                    return;
                }

                streamClient.StartStreaming();
                buttonStreamuj.Text = "Zatrzymaj";
                buttonOdbieraj.Enabled = false;
                checkBoxKolor.Enabled = false;
                trackBarTimeInterval.Enabled = true;
                wysyla = true;
                timerUpdateFPS.Start();
            }
        }

        private void buttonOdbieraj_Click(object sender, EventArgs e)
        {
            if (odbiera)
            {
                streamClient.StopRecievingStream();
                buttonOdbieraj.Text = "Odbieraj";
                buttonStreamuj.Enabled = true;
                numericUpDownPort.Enabled = true;
                odbiera = false;
                timerUpdateFPS.Stop();
            }
            else
            {
                if (textBoxServerIP.Text != "")
                {
                    streamClient.SetRemoteIP(textBoxServerIP.Text);
                }
                else
                {
                    MessageBox.Show("Podaj adres ip serwera.");
                    return;
                }
                streamClient.SetlocalUdpPort((int)numericUpDownPort.Value);
                streamClient.StartRecievingStream();
                buttonOdbieraj.Text = "Zatrzymaj";
                buttonStreamuj.Enabled = false;
                numericUpDownPort.Enabled = false;
                odbiera = true;
                timerUpdateFPS.Start();
            }
        }

        private void trackBarTimeInterval_ValueChanged(object sender, EventArgs e)
        {
            streamClient.SetTimeInterval(tt[trackBarTimeInterval.Value]);
        }

        private void checkBoxKolor_CheckedChanged(object sender, EventArgs e)
        {
            streamClient.SetImageInColor(checkBoxKolor.Checked);
        }

        private void numericUpDownJakosc_ValueChanged(object sender, EventArgs e)
        {
            streamClient.SetImageQuality((long)numericUpDownJakosc.Value);
        }

        private void timerUpdateFPS_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel4.Text = streamClient.GetFramesCount();
            streamClient.ResetFramesCount();
        }
    }
}
