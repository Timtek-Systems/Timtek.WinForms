namespace Timtek.WinForms.SampleApp;

public partial class MainForm : Form
{
    private bool enableFlag;

    public MainForm(MainFormViewModel viewModel)
    {
        InitializeComponent();
        DataBind(viewModel);
    }

    private void MuteToggleCheckedChanged(object sender, EventArgs e)
    {
        annunciatorReady.Mute = muteToggle.Checked;
        annunciatorFail.Mute = muteToggle.Checked;
    }

    private void DataBind(MainFormViewModel viewModel) => mainFormViewModelBindingSource.DataSource = viewModel;
}