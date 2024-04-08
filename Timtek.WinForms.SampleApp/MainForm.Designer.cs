namespace Timtek.WinForms.SampleApp
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ledSlowBlink = new LedIndicator();
            ledHeartbeat = new LedIndicator();
            annunciatorPanel1 = new AnnunciatorPanel();
            annunciatorReady = new Annunciator();
            annunciatorFail = new Annunciator();
            muteToggle = new SlidingToggleButton.ToggleSwitch();
            mainFormViewModelBindingSource = new BindingSource(components);
            RelayCommandButton = new Button();
            toggleSwitch1 = new SlidingToggleButton.ToggleSwitch();
            MvvmGroup = new GroupBox();
            annunciatorPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainFormViewModelBindingSource).BeginInit();
            MvvmGroup.SuspendLayout();
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
            annunciatorPanel1.Controls.Add(annunciatorFail);
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
            // annunciatorFail
            // 
            annunciatorFail.ActiveColor = Color.FromArgb(200, 4, 4);
            annunciatorFail.AutoSize = true;
            annunciatorFail.BackColor = Color.FromArgb(64, 0, 0);
            annunciatorFail.Cadence = CadencePattern.BlinkAlarm;
            annunciatorFail.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            annunciatorFail.ForeColor = Color.FromArgb(200, 4, 4);
            annunciatorFail.InactiveColor = Color.FromArgb(96, 4, 4);
            annunciatorFail.Location = new Point(69, 0);
            annunciatorFail.Name = "annunciatorFail";
            annunciatorFail.Size = new Size(50, 22);
            annunciatorFail.TabIndex = 1;
            annunciatorFail.Text = "FAIL";
            // 
            // muteToggle
            // 
            muteToggle.ButtonImage = (Image)resources.GetObject("muteToggle.ButtonImage");
            muteToggle.Command = null;
            muteToggle.Location = new Point(177, 145);
            muteToggle.Name = "muteToggle";
            muteToggle.OffFont = new Font("Segoe UI", 9F);
            muteToggle.OffText = "Run";
            muteToggle.OnFont = new Font("Segoe UI", 9F);
            muteToggle.OnText = "Mute";
            muteToggle.Size = new Size(120, 34);
            muteToggle.Style = SlidingToggleButton.ToggleSwitch.ToggleSwitchStyle.Fancy;
            muteToggle.TabIndex = 3;
            muteToggle.CheckedChanged += MuteToggleCheckedChanged;
            // 
            // mainFormViewModelBindingSource
            // 
            mainFormViewModelBindingSource.DataSource = typeof(MainFormViewModel);
            // 
            // RelayCommandButton
            // 
            RelayCommandButton.DataBindings.Add(new Binding("Command", mainFormViewModelBindingSource, "ButtonClickRelayCommand", true, DataSourceUpdateMode.OnPropertyChanged));
            RelayCommandButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RelayCommandButton.Location = new Point(131, 22);
            RelayCommandButton.Name = "RelayCommandButton";
            RelayCommandButton.Size = new Size(175, 33);
            RelayCommandButton.TabIndex = 4;
            RelayCommandButton.Text = "Beep Sound";
            RelayCommandButton.UseVisualStyleBackColor = true;
            // 
            // toggleSwitch1
            // 
            toggleSwitch1.ButtonImage = (Image)resources.GetObject("toggleSwitch1.ButtonImage");
            toggleSwitch1.Command = null;
            toggleSwitch1.DataBindings.Add(new Binding("Command", mainFormViewModelBindingSource, "EnableDisableToggleCommand", true, DataSourceUpdateMode.OnPropertyChanged));
            toggleSwitch1.Location = new Point(6, 22);
            toggleSwitch1.Name = "toggleSwitch1";
            toggleSwitch1.OffFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toggleSwitch1.OffForeColor = SystemColors.HighlightText;
            toggleSwitch1.OffText = "Disabled";
            toggleSwitch1.OnFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toggleSwitch1.OnText = "Enabled";
            toggleSwitch1.Size = new Size(119, 33);
            toggleSwitch1.Style = SlidingToggleButton.ToggleSwitch.ToggleSwitchStyle.Fancy;
            toggleSwitch1.TabIndex = 6;
            // 
            // MvvmGroup
            // 
            MvvmGroup.Controls.Add(toggleSwitch1);
            MvvmGroup.Controls.Add(RelayCommandButton);
            MvvmGroup.Location = new Point(177, 200);
            MvvmGroup.Name = "MvvmGroup";
            MvvmGroup.Size = new Size(442, 68);
            MvvmGroup.TabIndex = 7;
            MvvmGroup.TabStop = false;
            MvvmGroup.Text = "MVVM Relay Command";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 450);
            Controls.Add(MvvmGroup);
            Controls.Add(muteToggle);
            Controls.Add(annunciatorPanel1);
            Controls.Add(ledHeartbeat);
            Controls.Add(ledSlowBlink);
            Name = "MainForm";
            Text = "Form1";
            annunciatorPanel1.ResumeLayout(false);
            annunciatorPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainFormViewModelBindingSource).EndInit();
            MvvmGroup.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
            }

        #endregion

        private LedIndicator ledSlowBlink;
        private LedIndicator ledHeartbeat;
        private AnnunciatorPanel annunciatorPanel1;
        private Annunciator annunciatorReady;
        private Annunciator annunciatorFail;
        private SlidingToggleButton.ToggleSwitch muteToggle;
        private SlidingToggleButton.ToggleSwitch RelayCommandExecutionToggle;
        private Button RelayCommandButton;
        private BindingSource mainFormViewModelBindingSource;
        private SlidingToggleButton.ToggleSwitch toggleSwitch1;
        private GroupBox MvvmGroup;
        }
}
