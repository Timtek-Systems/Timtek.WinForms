﻿namespace Timtek.WinForms.SampleApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ledSlowBlink = new LedIndicator();
            ledHeartbeat = new LedIndicator();
            annunciatorPanel1 = new AnnunciatorPanel();
            annunciatorReady = new Annunciator();
            annunciator1 = new Annunciator();
            annunciatorPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // ledSlowBlink
            // 
            ledSlowBlink.AutoSize = true;
            ledSlowBlink.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ledSlowBlink.Cadence = CadencePattern.BlinkSlow;
            ledSlowBlink.LabelText = "LED Slow Blink";
            ledSlowBlink.Location = new Point(9, 9);
            ledSlowBlink.Margin = new Padding(0);
            ledSlowBlink.Name = "ledSlowBlink";
            ledSlowBlink.Size = new Size(110, 16);
            ledSlowBlink.TabIndex = 0;
            // 
            // ledHeartbeat
            // 
            ledHeartbeat.AutoSize = true;
            ledHeartbeat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ledHeartbeat.Cadence = CadencePattern.Heartbeat;
            ledHeartbeat.LabelText = "LED Heartbeat";
            ledHeartbeat.Location = new Point(9, 25);
            ledHeartbeat.Margin = new Padding(0);
            ledHeartbeat.Name = "ledHeartbeat";
            ledHeartbeat.Size = new Size(108, 16);
            ledHeartbeat.Status = TrafficLight.Red;
            ledHeartbeat.TabIndex = 1;
            // 
            // annunciatorPanel1
            // 
            annunciatorPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            annunciatorPanel1.BackColor = Color.FromArgb(64, 0, 0);
            annunciatorPanel1.Controls.Add(annunciatorReady);
            annunciatorPanel1.Controls.Add(annunciator1);
            annunciatorPanel1.Location = new Point(177, 12);
            annunciatorPanel1.Name = "annunciatorPanel1";
            annunciatorPanel1.Size = new Size(443, 100);
            annunciatorPanel1.TabIndex = 2;
            // 
            // annunciatorReady
            // 
            annunciatorReady.ActiveColor = Color.DarkSeaGreen;
            annunciatorReady.AutoSize = true;
            annunciatorReady.BackColor = Color.FromArgb(64, 0, 0);
            annunciatorReady.Cadence = CadencePattern.Wink;
            annunciatorReady.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            annunciatorReady.ForeColor = Color.DarkSeaGreen;
            annunciatorReady.InactiveColor = Color.FromArgb(96, 4, 4);
            annunciatorReady.Location = new Point(3, 0);
            annunciatorReady.Name = "annunciatorReady";
            annunciatorReady.Size = new Size(60, 22);
            annunciatorReady.TabIndex = 0;
            annunciatorReady.Text = "READY";
            // 
            // annunciator1
            // 
            annunciator1.ActiveColor = Color.FromArgb(200, 4, 4);
            annunciator1.AutoSize = true;
            annunciator1.BackColor = Color.FromArgb(64, 0, 0);
            annunciator1.Cadence = CadencePattern.BlinkAlarm;
            annunciator1.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            annunciator1.ForeColor = Color.FromArgb(200, 4, 4);
            annunciator1.InactiveColor = Color.FromArgb(96, 4, 4);
            annunciator1.Location = new Point(69, 0);
            annunciator1.Name = "annunciator1";
            annunciator1.Size = new Size(50, 22);
            annunciator1.TabIndex = 1;
            annunciator1.Text = "FAIL";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 450);
            Controls.Add(annunciatorPanel1);
            Controls.Add(ledHeartbeat);
            Controls.Add(ledSlowBlink);
            Name = "Form1";
            Text = "Form1";
            annunciatorPanel1.ResumeLayout(false);
            annunciatorPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LedIndicator ledSlowBlink;
        private LedIndicator ledHeartbeat;
        private AnnunciatorPanel annunciatorPanel1;
        private Annunciator annunciatorReady;
        private Annunciator annunciator1;
    }
}
