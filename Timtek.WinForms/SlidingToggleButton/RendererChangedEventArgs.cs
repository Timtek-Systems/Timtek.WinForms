using Timtek.WinForms.SlidingToggleButton.Renderers;

namespace Timtek.WinForms.SlidingToggleButton;

public class BeforeRenderingEventArgs
{
    public BeforeRenderingEventArgs(ToggleSwitchRendererBase renderer) => Renderer = renderer;

    public ToggleSwitchRendererBase Renderer { get; set; }
}