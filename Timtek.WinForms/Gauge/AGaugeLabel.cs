using System.ComponentModel;

namespace Timtek.WinForms.Gauge;

public class AGaugeLabel
{
    private static readonly Font DefaultFont = Control.DefaultFont;
    private Color _Color = Color.FromKnownColor(KnownColor.WindowText);
    private Font _Font = DefaultFont;
    private string _Name;
    private Point _Position;
    private string _Text;
    private AGauge Owner;

    [Browsable(true)]
    [Category("Design")]
    [DisplayName("(Name)")]
    [Description("Instance Name.")]
    public string Name
    {
        get => _Name;
        set
        {
            NotifyChanging();
            _Name = value;
            NotifyChanged();
        }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("The color of the caption text.")]
    public Color Color
    {
        get => _Color;
        set
        {
            NotifyChanging();
            _Color = value;
            NotifyOwner();
            NotifyChanged();
        }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("The text of the caption.")]
    public string Text
    {
        get => _Text;
        set
        {
            NotifyChanging();
            _Text = value;
            NotifyOwner();
            NotifyChanged();
        }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("The position of the caption.")]
    public Point Position
    {
        get => _Position;
        set
        {
            NotifyChanging();
            _Position = value;
            NotifyOwner();
            NotifyChanged();
        }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Font of Text.")]
    public Font Font
    {
        get => _Font;
        set
        {
            NotifyChanging();
            _Font = value;
            NotifyOwner();
            NotifyChanged();
        }
    }

    [Browsable(false)]
    public void SetOwner(AGauge value) => Owner = value;

    private void NotifyOwner()
    {
        if (Owner != null) Owner.RepaintControl();
    }

    private void NotifyChanging()
    {
        if (Owner != null) Owner.NotifyChanging(nameof(AGauge.GaugeLabels));
    }

    private void NotifyChanged()
    {
        if (Owner != null) Owner.NotifyChanged(nameof(AGauge.GaugeLabels));
    }

    public void ResetFont()
    {
        NotifyChanging();
        _Font = DefaultFont;
        NotifyChanged();
    }

    private bool ShouldSerializeFont() => _Font != DefaultFont;
}