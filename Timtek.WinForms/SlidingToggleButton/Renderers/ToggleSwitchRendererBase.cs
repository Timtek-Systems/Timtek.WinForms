using System.Drawing.Drawing2D;

namespace Timtek.WinForms.SlidingToggleButton.Renderers;

public abstract class ToggleSwitchRendererBase
{
    #region Private Members

    #endregion Private Members

    #region Constructor

    internal void SetToggleSwitch(ToggleSwitch toggleSwitch)
    {
        ToggleSwitch = toggleSwitch;
    }

    internal ToggleSwitch ToggleSwitch { get; private set; }

    #endregion Constructor

    #region Render Methods

    public void RenderBackground(PaintEventArgs e)
    {
        if (ToggleSwitch == null)
            return;

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);

        FillBackground(e.Graphics, controlRectangle);

        RenderBorder(e.Graphics, controlRectangle);
    }

    public void RenderControl(PaintEventArgs e)
    {
        if (ToggleSwitch == null)
            return;

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var buttonRectangle = GetButtonRectangle();
        var totalToggleFieldWidth = ToggleSwitch.Width - buttonRectangle.Width;

        if (buttonRectangle.X > 0)
        {
            var leftRectangle = new Rectangle(0, 0, buttonRectangle.X, ToggleSwitch.Height);

            if (leftRectangle.Width > 0)
                RenderLeftToggleField(e.Graphics, leftRectangle, totalToggleFieldWidth);
        }

        if (buttonRectangle.X + buttonRectangle.Width < e.ClipRectangle.Width)
        {
            var rightRectangle = new Rectangle(buttonRectangle.X + buttonRectangle.Width, 0,
                ToggleSwitch.Width - buttonRectangle.X - buttonRectangle.Width, ToggleSwitch.Height);

            if (rightRectangle.Width > 0)
                RenderRightToggleField(e.Graphics, rightRectangle, totalToggleFieldWidth);
        }

        RenderButton(e.Graphics, buttonRectangle);
    }

    public void FillBackground(Graphics g, Rectangle controlRectangle)
    {
        var backColor = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
            ? ToggleSwitch.BackColor.ToGrayScale()
            : ToggleSwitch.BackColor;

        using (Brush backBrush = new SolidBrush(backColor))
        {
            g.FillRectangle(backBrush, controlRectangle);
        }
    }

    public abstract void RenderBorder(Graphics g, Rectangle borderRectangle);
    public abstract void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth);
    public abstract void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth);
    public abstract void RenderButton(Graphics g, Rectangle buttonRectangle);

    #endregion Render Methods

    #region Helper Methods

    public abstract int GetButtonWidth();
    public abstract Rectangle GetButtonRectangle();
    public abstract Rectangle GetButtonRectangle(int buttonWidth);

    #endregion Helper Methods
}