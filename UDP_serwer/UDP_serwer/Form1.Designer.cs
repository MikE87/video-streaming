namespace UDP_serwer
{
    partial class StreamServer
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
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonDodajAdres = new System.Windows.Forms.Button();
            this.textBoxDodajAdres = new System.Windows.Forms.TextBox();
            this.textBoxSenderIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(326, 262);
            this.textBoxLog.TabIndex = 0;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(259, 51);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(67, 31);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(259, 88);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(67, 31);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonDodajAdres
            // 
            this.buttonDodajAdres.Location = new System.Drawing.Point(219, 125);
            this.buttonDodajAdres.Name = "buttonDodajAdres";
            this.buttonDodajAdres.Size = new System.Drawing.Size(107, 23);
            this.buttonDodajAdres.TabIndex = 3;
            this.buttonDodajAdres.Text = "Dodaj adres klienta";
            this.buttonDodajAdres.UseVisualStyleBackColor = true;
            this.buttonDodajAdres.Click += new System.EventHandler(this.buttonDodajAdres_Click);
            // 
            // textBoxDodajAdres
            // 
            this.textBoxDodajAdres.Location = new System.Drawing.Point(234, 154);
            this.textBoxDodajAdres.Name = "textBoxDodajAdres";
            this.textBoxDodajAdres.Size = new System.Drawing.Size(92, 20);
            this.textBoxDodajAdres.TabIndex = 4;
            this.textBoxDodajAdres.Text = "127.0.0.1";
            this.textBoxDodajAdres.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxSenderIP
            // 
            this.textBoxSenderIP.Location = new System.Drawing.Point(226, 25);
            this.textBoxSenderIP.Name = "textBoxSenderIP";
            this.textBoxSenderIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxSenderIP.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP wysyłającego";
            // 
            // StreamServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSenderIP);
            this.Controls.Add(this.textBoxDodajAdres);
            this.Controls.Add(this.buttonDodajAdres);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxLog);
            this.Name = "StreamServer";
            this.Text = "Stream Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonDodajAdres;
        private System.Windows.Forms.TextBox textBoxDodajAdres;
        private System.Windows.Forms.TextBox textBoxSenderIP;
        private System.Windows.Forms.Label label1;
    }
}

