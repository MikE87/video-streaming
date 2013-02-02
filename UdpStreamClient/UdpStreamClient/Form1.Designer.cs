namespace UdpStreamClient
{
    partial class Klient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonStreamuj = new System.Windows.Forms.Button();
            this.pictureBoxLocalCamera = new System.Windows.Forms.PictureBox();
            this.trackBarTimeInterval = new System.Windows.Forms.TrackBar();
            this.buttonOdbieraj = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxKolor = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelJakosc = new System.Windows.Forms.Label();
            this.numericUpDownJakosc = new System.Windows.Forms.NumericUpDown();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerUpdateFPS = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocalCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJakosc)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStreamuj
            // 
            this.buttonStreamuj.Location = new System.Drawing.Point(12, 264);
            this.buttonStreamuj.Name = "buttonStreamuj";
            this.buttonStreamuj.Size = new System.Drawing.Size(75, 23);
            this.buttonStreamuj.TabIndex = 3;
            this.buttonStreamuj.Text = "Streamuj";
            this.buttonStreamuj.UseVisualStyleBackColor = true;
            this.buttonStreamuj.Click += new System.EventHandler(this.buttonWyslij_Click);
            // 
            // pictureBoxLocalCamera
            // 
            this.pictureBoxLocalCamera.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxLocalCamera.Name = "pictureBoxLocalCamera";
            this.pictureBoxLocalCamera.Size = new System.Drawing.Size(320, 240);
            this.pictureBoxLocalCamera.TabIndex = 4;
            this.pictureBoxLocalCamera.TabStop = false;
            // 
            // trackBarTimeInterval
            // 
            this.trackBarTimeInterval.Enabled = false;
            this.trackBarTimeInterval.Location = new System.Drawing.Point(338, 12);
            this.trackBarTimeInterval.Name = "trackBarTimeInterval";
            this.trackBarTimeInterval.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarTimeInterval.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarTimeInterval.Size = new System.Drawing.Size(45, 275);
            this.trackBarTimeInterval.TabIndex = 10;
            this.trackBarTimeInterval.Value = 10;
            this.trackBarTimeInterval.ValueChanged += new System.EventHandler(this.trackBarTimeInterval_ValueChanged);
            // 
            // buttonOdbieraj
            // 
            this.buttonOdbieraj.Location = new System.Drawing.Point(93, 264);
            this.buttonOdbieraj.Name = "buttonOdbieraj";
            this.buttonOdbieraj.Size = new System.Drawing.Size(75, 23);
            this.buttonOdbieraj.TabIndex = 11;
            this.buttonOdbieraj.Text = "Odbieraj";
            this.buttonOdbieraj.UseVisualStyleBackColor = true;
            this.buttonOdbieraj.Click += new System.EventHandler(this.buttonOdbieraj_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(203, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Port UDP:";
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(261, 267);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownPort.TabIndex = 14;
            this.numericUpDownPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownPort.Value = new decimal(new int[] {
            10101,
            0,
            0,
            0});
            // 
            // checkBoxKolor
            // 
            this.checkBoxKolor.AutoSize = true;
            this.checkBoxKolor.Checked = true;
            this.checkBoxKolor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKolor.Location = new System.Drawing.Point(7, 293);
            this.checkBoxKolor.Name = "checkBoxKolor";
            this.checkBoxKolor.Size = new System.Drawing.Size(71, 17);
            this.checkBoxKolor.TabIndex = 15;
            this.checkBoxKolor.Text = "w kolorze";
            this.checkBoxKolor.UseVisualStyleBackColor = true;
            this.checkBoxKolor.CheckedChanged += new System.EventHandler(this.checkBoxKolor_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Serwer IP:";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(261, 291);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(101, 20);
            this.textBoxServerIP.TabIndex = 17;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 320);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(374, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel1.Text = "Lokalne ip: ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // labelJakosc
            // 
            this.labelJakosc.AutoSize = true;
            this.labelJakosc.Location = new System.Drawing.Point(84, 294);
            this.labelJakosc.Name = "labelJakosc";
            this.labelJakosc.Size = new System.Drawing.Size(44, 13);
            this.labelJakosc.TabIndex = 19;
            this.labelJakosc.Text = "Jakość:";
            // 
            // numericUpDownJakosc
            // 
            this.numericUpDownJakosc.Location = new System.Drawing.Point(134, 291);
            this.numericUpDownJakosc.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownJakosc.Name = "numericUpDownJakosc";
            this.numericUpDownJakosc.Size = new System.Drawing.Size(34, 20);
            this.numericUpDownJakosc.TabIndex = 20;
            this.numericUpDownJakosc.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownJakosc.ValueChanged += new System.EventHandler(this.numericUpDownJakosc_ValueChanged);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel3.Text = "fps:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel4.Text = "0";
            // 
            // timerUpdateFPS
            // 
            this.timerUpdateFPS.Interval = 1000;
            this.timerUpdateFPS.Tick += new System.EventHandler(this.timerUpdateFPS_Tick);
            // 
            // Klient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 342);
            this.Controls.Add(this.numericUpDownJakosc);
            this.Controls.Add(this.labelJakosc);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxKolor);
            this.Controls.Add(this.buttonStreamuj);
            this.Controls.Add(this.numericUpDownPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOdbieraj);
            this.Controls.Add(this.trackBarTimeInterval);
            this.Controls.Add(this.pictureBoxLocalCamera);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(390, 380);
            this.MinimumSize = new System.Drawing.Size(390, 380);
            this.Name = "Klient";
            this.Text = "Stream Client";
            this.Load += new System.EventHandler(this.Klient_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Klient_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocalCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJakosc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStreamuj;
        private System.Windows.Forms.PictureBox pictureBoxLocalCamera;
        private System.Windows.Forms.TrackBar trackBarTimeInterval;
        private System.Windows.Forms.Button buttonOdbieraj;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.CheckBox checkBoxKolor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label labelJakosc;
        private System.Windows.Forms.NumericUpDown numericUpDownJakosc;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Timer timerUpdateFPS;
    }
}

