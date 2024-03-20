namespace Timtek.WinForms.SampleApp;

public partial class Form1 : Form
{
    public Form1() => InitializeComponent();

    private void MuteToggleCheckedChanged(object sender, EventArgs e)
    {
        annunciatorReady.Mute = muteToggle.Checked;
        annunciatorFail.Mute = muteToggle.Checked;
    }
}