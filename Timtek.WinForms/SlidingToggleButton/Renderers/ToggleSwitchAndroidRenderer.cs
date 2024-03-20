namespace Timtek.WinForms.SlidingToggleButton.Renderers;

public class ToggleSwitchAndroidRenderer : ToggleSwitchRendererBase
{
    #region Constructor

    public ToggleSwitchAndroidRenderer()
    {
        BorderColor = Color.FromArgb(255, 166, 166, 166);
        BackColor = Color.FromArgb(255, 32, 32, 32);
        LeftSideColor = Color.FromArgb(255, 32, 32, 32);
        RightSideColor = Color.FromArgb(255, 32, 32, 32);
        OffButtonColor = Color.FromArgb(255, 70, 70, 70);
        OnButtonColor = Color.FromArgb(255, 27, 161, 226);
        OffButtonBorderColor = Color.FromArgb(255, 70, 70, 70);
        OnButtonBorderColor = Color.FromArgb(255, 27, 161, 226);
        SlantAngle = 8;
    }

    #endregion Constructor

    #region Public Properties

    public Color BorderColor { get; set; }
    public Color BackColor { get; set; }
    public Color LeftSideColor { get; set; }
    public Color RightSideColor { get; set; }
    public Color OffButtonColor { get; set; }
    public Color OnButtonColor { get; set; }
    public Color OffButtonBorderColor { get; set; }
    public Color OnButtonBorderColor { get; set; }
    public int SlantAngle { get; set; }

    #endregion Public Properties

    #region Render Method Implementations

    public override void RenderBorder(Graphics g, Rectangle borderRectangle)
    {
        var borderColor = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled ? BorderColor.ToGrayScale() : BorderColor;

        g.SetClip(borderRectangle);

        using (var borderPen = new Pen(borderColor))
        {
            g.DrawRectangle(borderPen, borderRectangle.X, borderRectangle.Y, borderRectangle.Width - 1, borderRectangle.Height - 1);
        }
    }

    public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
    {
        var leftColor = LeftSideColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            leftColor = leftColor.ToGrayScale();

        var controlRectangle = GetInnerControlRectangle();

        g.SetClip(controlRectangle);

        var halfCathetusLength = GetHalfCathetusLengthBasedOnAngle();

        var adjustedLeftRect = new Rectangle(leftRectangle.X, leftRectangle.Y, leftRectangle.Width + halfCathetusLength,
            leftRectangle.Height);

        g.IntersectClip(adjustedLeftRect);

        using (Brush leftBrush = new SolidBrush(leftColor))
        {
            g.FillRectangle(leftBrush, adjustedLeftRect);
        }

        g.ResetClip();
    }

    public override void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth)
    {
        var rightColor = RightSideColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            rightColor = rightColor.ToGrayScale();

        var controlRectangle = GetInnerControlRectangle();

        g.SetClip(controlRectangle);

        var halfCathetusLength = GetHalfCathetusLengthBasedOnAngle();

        var adjustedRightRect = new Rectangle(rightRectangle.X - halfCathetusLength, rightRectangle.Y,
            rightRectangle.Width + halfCathetusLength, rightRectangle.Height);

        g.IntersectClip(adjustedRightRect);

        using (Brush rightBrush = new SolidBrush(rightColor))
        {
            g.FillRectangle(rightBrush, adjustedRightRect);
        }

        g.ResetClip();
    }

    public override void RenderButton(Graphics g, Rectangle buttonRectangle)
    {
        var controlRectangle = GetInnerControlRectangle();

        g.SetClip(controlRectangle);

        var fullCathetusLength = GetCathetusLengthBasedOnAngle();
        var dblFullCathetusLength = 2 * fullCathetusLength;

        var polygonPoints = new Point[4];

        var adjustedButtonRect = new Rectangle(buttonRectangle.X - fullCathetusLength, controlRectangle.Y,
            buttonRectangle.Width + dblFullCathetusLength, controlRectangle.Height);

        if (SlantAngle > 0)
        {
            polygonPoints[0] = new Point(adjustedButtonRect.X + fullCathetusLength, adjustedButtonRect.Y);
            polygonPoints[1] = new Point(adjustedButtonRect.X + adjustedButtonRect.Width - 1, adjustedButtonRect.Y);
            polygonPoints[2] = new Point(adjustedButtonRect.X + adjustedButtonRect.Width - fullCathetusLength - 1,
                adjustedButtonRect.Y + adjustedButtonRect.Height - 1);
            polygonPoints[3] = new Point(adjustedButtonRect.X, adjustedButtonRect.Y + adjustedButtonRect.Height - 1);
        }
        else
        {
            polygonPoints[0] = new Point(adjustedButtonRect.X, adjustedButtonRect.Y);
            polygonPoints[1] = new Point(adjustedButtonRect.X + adjustedButtonRect.Width - fullCathetusLength - 1,
                adjustedButtonRect.Y);
            polygonPoints[2] = new Point(adjustedButtonRect.X + adjustedButtonRect.Width - 1,
                adjustedButtonRect.Y + adjustedButtonRect.Height - 1);
            polygonPoints[3] = new Point(adjustedButtonRect.X + fullCathetusLength,
                adjustedButtonRect.Y + adjustedButtonRect.Height - 1);
        }

        g.IntersectClip(adjustedButtonRect);

        var buttonColor = ToggleSwitch.Checked ? OnButtonColor : OffButtonColor;
        var buttonBorderColor = ToggleSwitch.Checked ? OnButtonBorderColor : OffButtonBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
        {
            buttonColor = buttonColor.ToGrayScale();
            buttonBorderColor = buttonBorderColor.ToGrayScale();
        }

        using (var buttonPen = new Pen(buttonBorderColor))
        {
            g.DrawPolygon(buttonPen, polygonPoints);
        }

        using (Brush buttonBrush = new SolidBrush(buttonColor))
        {
            g.FillPolygon(buttonBrush, polygonPoints);
        }

        var buttonImage = ToggleSwitch.ButtonImage ??
                          (ToggleSwitch.Checked ? ToggleSwitch.OnButtonImage : ToggleSwitch.OffButtonImage);
        var buttonText = ToggleSwitch.Checked ? ToggleSwitch.OnText : ToggleSwitch.OffText;

        if (buttonImage != null || !string.IsNullOrEmpty(buttonText))
        {
            var alignment = ToggleSwitch.ButtonImage != null ? ToggleSwitch.ButtonAlignment :
                ToggleSwitch.Checked ? ToggleSwitch.OnButtonAlignment : ToggleSwitch.OffButtonAlignment;

            if (buttonImage != null)
            {
                var imageSize = buttonImage.Size;
                Rectangle imageRectangle;

                var imageXPos = adjustedButtonRect.X;

                var scaleImage = ToggleSwitch.ButtonImage != null ? ToggleSwitch.ButtonScaleImageToFit :
                    ToggleSwitch.Checked ? ToggleSwitch.OnButtonScaleImageToFit : ToggleSwitch.OffButtonScaleImageToFit;

                if (scaleImage)
                {
                    var canvasSize = adjustedButtonRect.Size;
                    var resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                    if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Center)
                        imageXPos = (int)(adjustedButtonRect.X + (adjustedButtonRect.Width - (float)resizedImageSize.Width) / 2);
                    else if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Right)
                        imageXPos = (int)(adjustedButtonRect.X + (float)adjustedButtonRect.Width - resizedImageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(adjustedButtonRect.Y + (adjustedButtonRect.Height - (float)resizedImageSize.Height) / 2),
                        resizedImageSize.Width, resizedImageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(buttonImage, imageRectangle, 0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel,
                            ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImage(buttonImage, imageRectangle);
                }
                else
                {
                    if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Center)
                        imageXPos = (int)(adjustedButtonRect.X + (adjustedButtonRect.Width - (float)imageSize.Width) / 2);
                    else if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Right)
                        imageXPos = (int)(adjustedButtonRect.X + (float)adjustedButtonRect.Width - imageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(adjustedButtonRect.Y + (adjustedButtonRect.Height - (float)imageSize.Height) / 2), imageSize.Width,
                        imageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(buttonImage, imageRectangle, 0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel,
                            ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImageUnscaled(buttonImage, imageRectangle);
                }
            }
            else if (!string.IsNullOrEmpty(buttonText))
            {
                var buttonFont = ToggleSwitch.Checked ? ToggleSwitch.OnFont : ToggleSwitch.OffFont;
                var buttonForeColor = ToggleSwitch.Checked ? ToggleSwitch.OnForeColor : ToggleSwitch.OffForeColor;

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                    buttonForeColor = buttonForeColor.ToGrayScale();

                var textSize = g.MeasureString(buttonText, buttonFont);

                float textXPos = adjustedButtonRect.X;

                if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Center)
                    textXPos = adjustedButtonRect.X + (adjustedButtonRect.Width - textSize.Width) / 2;
                else if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Right)
                    textXPos = adjustedButtonRect.X + (float)adjustedButtonRect.Width - textSize.Width;

                var textRectangle = new RectangleF(textXPos,
                    adjustedButtonRect.Y + (adjustedButtonRect.Height - textSize.Height) / 2, textSize.Width, textSize.Height);

                using (Brush textBrush = new SolidBrush(buttonForeColor))
                {
                    g.DrawString(buttonText, buttonFont, textBrush, textRectangle);
                }
            }
        }

        g.ResetClip();
    }

    #endregion Render Method Implementations

    #region Helper Method Implementations

    public Rectangle GetInnerControlRectangle()
    {
        return new Rectangle(1, 1, ToggleSwitch.Width - 2, ToggleSwitch.Height - 2);
    }

    public int GetCathetusLengthBasedOnAngle()
    {
        if (SlantAngle == 0)
            return 0;

        double degrees = Math.Abs(SlantAngle);
        var radians = degrees * (Math.PI / 180);
        var length = Math.Tan(radians) * ToggleSwitch.Height;

        return (int)length;
    }

    public int GetHalfCathetusLengthBasedOnAngle()
    {
        if (SlantAngle == 0)
            return 0;

        double degrees = Math.Abs(SlantAngle);
        var radians = degrees * (Math.PI / 180);
        var length = Math.Tan(radians) * ToggleSwitch.Height / 2;

        return (int)length;
    }

    public override int GetButtonWidth()
    {
        var buttonWidth = (double)ToggleSwitch.Width / 2;
        return (int)buttonWidth;
    }

    public override Rectangle GetButtonRectangle()
    {
        var buttonWidth = GetButtonWidth();
        return GetButtonRectangle(buttonWidth);
    }

    public override Rectangle GetButtonRectangle(int buttonWidth)
    {
        var buttonRect = new Rectangle(ToggleSwitch.ButtonValue, 0, buttonWidth, ToggleSwitch.Height);
        return buttonRect;
    }

    #endregion Helper Method Implementations
}