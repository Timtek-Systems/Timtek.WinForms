using System.Drawing.Drawing2D;

namespace Timtek.WinForms.SlidingToggleButton.Renderers;

public class ToggleSwitchOSXRenderer : ToggleSwitchRendererBase, IDisposable
{
    #region Constructor

    private GraphicsPath _innerControlPath;

    public ToggleSwitchOSXRenderer()
    {
        OuterBorderColor = Color.FromArgb(255, 108, 108, 108);
        InnerBorderColor1 = Color.FromArgb(255, 137, 138, 139);
        InnerBorderColor2 = Color.FromArgb(255, 167, 168, 169);
        BackColor1 = Color.FromArgb(255, 108, 108, 108);
        BackColor2 = Color.FromArgb(255, 163, 163, 163);
        ButtonNormalBorderColor1 = Color.FromArgb(255, 147, 147, 148);
        ButtonNormalBorderColor2 = Color.FromArgb(255, 167, 168, 169);
        ButtonNormalSurfaceColor1 = Color.FromArgb(255, 250, 250, 250);
        ButtonNormalSurfaceColor2 = Color.FromArgb(255, 224, 224, 224);
        ButtonHoverBorderColor1 = Color.FromArgb(255, 145, 146, 147);
        ButtonHoverBorderColor2 = Color.FromArgb(255, 164, 165, 166);
        ButtonHoverSurfaceColor1 = Color.FromArgb(255, 238, 238, 238);
        ButtonHoverSurfaceColor2 = Color.FromArgb(255, 213, 213, 213);
        ButtonPressedBorderColor1 = Color.FromArgb(255, 138, 138, 140);
        ButtonPressedBorderColor2 = Color.FromArgb(255, 158, 158, 160);
        ButtonPressedSurfaceColor1 = Color.FromArgb(255, 187, 187, 187);
        ButtonPressedSurfaceColor2 = Color.FromArgb(255, 168, 168, 168);
        ButtonShadowColor1 = Color.FromArgb(50, 0, 0, 0);
        ButtonShadowColor2 = Color.FromArgb(0, 0, 0, 0);

        ButtonShadowWidth = 7;
        CornerRadius = 4;
    }

    public void Dispose()
    {
        if (_innerControlPath != null)
            _innerControlPath.Dispose();
    }

    #endregion Constructor

    #region Public Properties

    public Color OuterBorderColor { get; set; }
    public Color InnerBorderColor1 { get; set; }
    public Color InnerBorderColor2 { get; set; }
    public Color BackColor1 { get; set; }
    public Color BackColor2 { get; set; }
    public Color ButtonNormalBorderColor1 { get; set; }
    public Color ButtonNormalBorderColor2 { get; set; }
    public Color ButtonNormalSurfaceColor1 { get; set; }
    public Color ButtonNormalSurfaceColor2 { get; set; }
    public Color ButtonHoverBorderColor1 { get; set; }
    public Color ButtonHoverBorderColor2 { get; set; }
    public Color ButtonHoverSurfaceColor1 { get; set; }
    public Color ButtonHoverSurfaceColor2 { get; set; }
    public Color ButtonPressedBorderColor1 { get; set; }
    public Color ButtonPressedBorderColor2 { get; set; }
    public Color ButtonPressedSurfaceColor1 { get; set; }
    public Color ButtonPressedSurfaceColor2 { get; set; }
    public Color ButtonShadowColor1 { get; set; }
    public Color ButtonShadowColor2 { get; set; }

    public int ButtonShadowWidth { get; set; }
    public int CornerRadius { get; set; }

    #endregion Public Properties

    #region Render Method Implementations

    public override void RenderBorder(Graphics g, Rectangle borderRectangle)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;

        //Draw outer border
        using (var outerBorderPath = GetRoundedRectanglePath(borderRectangle, CornerRadius))
        {
            g.SetClip(outerBorderPath);

            var outerBorderColor = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? OuterBorderColor.ToGrayScale()
                : OuterBorderColor;

            using (Brush outerBorderBrush = new SolidBrush(outerBorderColor))
            {
                g.FillPath(outerBorderBrush, outerBorderPath);
            }

            g.ResetClip();
        }

        //Draw inner border
        var innerborderRectangle = new Rectangle(borderRectangle.X + 1, borderRectangle.Y + 1, borderRectangle.Width - 2,
            borderRectangle.Height - 2);

        using (var innerBorderPath = GetRoundedRectanglePath(innerborderRectangle, CornerRadius))
        {
            g.SetClip(innerBorderPath);

            var borderColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? InnerBorderColor1.ToGrayScale()
                : InnerBorderColor1;
            var borderColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? InnerBorderColor2.ToGrayScale()
                : InnerBorderColor2;

            using (Brush borderBrush =
                   new LinearGradientBrush(borderRectangle, borderColor1, borderColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(borderBrush, innerBorderPath);
            }

            g.ResetClip();
        }

        //Draw inner background
        var backgroundRectangle = new Rectangle(borderRectangle.X + 2, borderRectangle.Y + 2, borderRectangle.Width - 4,
            borderRectangle.Height - 4);

        _innerControlPath = GetRoundedRectanglePath(backgroundRectangle, CornerRadius);

        g.SetClip(_innerControlPath);

        var backColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled ? BackColor1.ToGrayScale() : BackColor1;
        var backColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled ? BackColor2.ToGrayScale() : BackColor2;

        using (Brush backgroundBrush =
               new LinearGradientBrush(borderRectangle, backColor1, backColor2, LinearGradientMode.Vertical))
        {
            g.FillPath(backgroundBrush, _innerControlPath);
        }

        g.ResetClip();
    }

    public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;

        var leftShadowRectangle = new Rectangle();
        leftShadowRectangle.X = leftRectangle.X + leftRectangle.Width - ButtonShadowWidth;
        leftShadowRectangle.Y = leftRectangle.Y;
        leftShadowRectangle.Width = ButtonShadowWidth + CornerRadius;
        leftShadowRectangle.Height = leftRectangle.Height;

        if (_innerControlPath != null)
        {
            g.SetClip(_innerControlPath);
            g.IntersectClip(leftShadowRectangle);
        }
        else
        {
            g.SetClip(leftShadowRectangle);
        }

        using (Brush buttonShadowBrush = new LinearGradientBrush(leftShadowRectangle, ButtonShadowColor2, ButtonShadowColor1,
                   LinearGradientMode.Horizontal))
        {
            g.FillRectangle(buttonShadowBrush, leftShadowRectangle);
        }

        g.ResetClip();

        //Draw image or text
        if (ToggleSwitch.OnSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OnText))
        {
            var fullRectangle = new RectangleF(leftRectangle.X + 1 - (totalToggleFieldWidth - leftRectangle.Width), 1,
                totalToggleFieldWidth - 1, ToggleSwitch.Height - 2);

            if (_innerControlPath != null)
            {
                g.SetClip(_innerControlPath);
                g.IntersectClip(fullRectangle);
            }
            else
            {
                g.SetClip(fullRectangle);
            }

            if (ToggleSwitch.OnSideImage != null)
            {
                var imageSize = ToggleSwitch.OnSideImage.Size;
                Rectangle imageRectangle;

                var imageXPos = (int)fullRectangle.X;

                if (ToggleSwitch.OnSideScaleImageToFit)
                {
                    var canvasSize = new Size((int)fullRectangle.Width, (int)fullRectangle.Height);
                    var resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                    if (ToggleSwitch.OnSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Center)
                        imageXPos = (int)(fullRectangle.X + (fullRectangle.Width - resizedImageSize.Width) / 2);
                    else if (ToggleSwitch.OnSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Near)
                        imageXPos = (int)(fullRectangle.X + fullRectangle.Width - resizedImageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(fullRectangle.Y + (fullRectangle.Height - resizedImageSize.Height) / 2), resizedImageSize.Width,
                        resizedImageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width,
                            ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle);
                }
                else
                {
                    if (ToggleSwitch.OnSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Center)
                        imageXPos = (int)(fullRectangle.X + (fullRectangle.Width - imageSize.Width) / 2);
                    else if (ToggleSwitch.OnSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Near)
                        imageXPos = (int)(fullRectangle.X + fullRectangle.Width - imageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(fullRectangle.Y + (fullRectangle.Height - imageSize.Height) / 2), imageSize.Width, imageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width,
                            ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImageUnscaled(ToggleSwitch.OnSideImage, imageRectangle);
                }
            }
            else if (!string.IsNullOrEmpty(ToggleSwitch.OnText))
            {
                var textSize = g.MeasureString(ToggleSwitch.OnText, ToggleSwitch.OnFont);

                var textXPos = fullRectangle.X;

                if (ToggleSwitch.OnSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Center)
                    textXPos = fullRectangle.X + (fullRectangle.Width - textSize.Width) / 2;
                else if (ToggleSwitch.OnSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Near)
                    textXPos = fullRectangle.X + fullRectangle.Width - textSize.Width;

                var textRectangle = new RectangleF(textXPos, fullRectangle.Y + (fullRectangle.Height - textSize.Height) / 2,
                    textSize.Width, textSize.Height);

                var textForeColor = ToggleSwitch.OnForeColor;

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                    textForeColor = textForeColor.ToGrayScale();

                using (Brush textBrush = new SolidBrush(textForeColor))
                {
                    g.DrawString(ToggleSwitch.OnText, ToggleSwitch.OnFont, textBrush, textRectangle);
                }
            }

            g.ResetClip();
        }
    }

    public override void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;

        var rightShadowRectangle = new Rectangle();
        rightShadowRectangle.X = rightRectangle.X - CornerRadius;
        rightShadowRectangle.Y = rightRectangle.Y;
        rightShadowRectangle.Width = ButtonShadowWidth + CornerRadius;
        rightShadowRectangle.Height = rightRectangle.Height;

        if (_innerControlPath != null)
        {
            g.SetClip(_innerControlPath);
            g.IntersectClip(rightShadowRectangle);
        }
        else
        {
            g.SetClip(rightShadowRectangle);
        }

        using (Brush buttonShadowBrush = new LinearGradientBrush(rightShadowRectangle, ButtonShadowColor1, ButtonShadowColor2,
                   LinearGradientMode.Horizontal))
        {
            g.FillRectangle(buttonShadowBrush, rightShadowRectangle);
        }

        g.ResetClip();

        //Draw image or text
        if (ToggleSwitch.OffSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OffText))
        {
            var fullRectangle = new RectangleF(rightRectangle.X, 1, totalToggleFieldWidth - 1, ToggleSwitch.Height - 2);

            if (_innerControlPath != null)
            {
                g.SetClip(_innerControlPath);
                g.IntersectClip(fullRectangle);
            }
            else
            {
                g.SetClip(fullRectangle);
            }

            if (ToggleSwitch.OffSideImage != null)
            {
                var imageSize = ToggleSwitch.OffSideImage.Size;
                Rectangle imageRectangle;

                var imageXPos = (int)fullRectangle.X;

                if (ToggleSwitch.OffSideScaleImageToFit)
                {
                    var canvasSize = new Size((int)fullRectangle.Width, (int)fullRectangle.Height);
                    var resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                    if (ToggleSwitch.OffSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Center)
                        imageXPos = (int)(fullRectangle.X + (fullRectangle.Width - resizedImageSize.Width) / 2);
                    else if (ToggleSwitch.OffSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Far)
                        imageXPos = (int)(fullRectangle.X + fullRectangle.Width - resizedImageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(fullRectangle.Y + (fullRectangle.Height - resizedImageSize.Height) / 2), resizedImageSize.Width,
                        resizedImageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width,
                            ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle);
                }
                else
                {
                    if (ToggleSwitch.OffSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Center)
                        imageXPos = (int)(fullRectangle.X + (fullRectangle.Width - imageSize.Width) / 2);
                    else if (ToggleSwitch.OffSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Far)
                        imageXPos = (int)(fullRectangle.X + fullRectangle.Width - imageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(fullRectangle.Y + (fullRectangle.Height - imageSize.Height) / 2), imageSize.Width, imageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width,
                            ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImageUnscaled(ToggleSwitch.OffSideImage, imageRectangle);
                }
            }
            else if (!string.IsNullOrEmpty(ToggleSwitch.OffText))
            {
                var textSize = g.MeasureString(ToggleSwitch.OffText, ToggleSwitch.OffFont);

                var textXPos = fullRectangle.X;

                if (ToggleSwitch.OffSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Center)
                    textXPos = fullRectangle.X + (fullRectangle.Width - textSize.Width) / 2;
                else if (ToggleSwitch.OffSideAlignment == ToggleSwitch.ToggleSwitchAlignment.Far)
                    textXPos = fullRectangle.X + fullRectangle.Width - textSize.Width;

                var textRectangle = new RectangleF(textXPos, fullRectangle.Y + (fullRectangle.Height - textSize.Height) / 2,
                    textSize.Width, textSize.Height);

                var textForeColor = ToggleSwitch.OffForeColor;

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                    textForeColor = textForeColor.ToGrayScale();

                using (Brush textBrush = new SolidBrush(textForeColor))
                {
                    g.DrawString(ToggleSwitch.OffText, ToggleSwitch.OffFont, textBrush, textRectangle);
                }
            }

            g.ResetClip();
        }
    }

    public override void RenderButton(Graphics g, Rectangle buttonRectangle)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;

        buttonRectangle.Inflate(-1, -1);

        using (var buttonPath = GetRoundedRectanglePath(buttonRectangle, CornerRadius))
        {
            g.SetClip(buttonPath);

            //Draw button surface
            var buttonSurfaceColor1 = ButtonNormalSurfaceColor1;
            var buttonSurfaceColor2 = ButtonNormalSurfaceColor2;

            if (ToggleSwitch.IsButtonPressed)
            {
                buttonSurfaceColor1 = ButtonPressedSurfaceColor1;
                buttonSurfaceColor2 = ButtonPressedSurfaceColor2;
            }
            else if (ToggleSwitch.IsButtonHovered)
            {
                buttonSurfaceColor1 = ButtonHoverSurfaceColor1;
                buttonSurfaceColor2 = ButtonHoverSurfaceColor2;
            }

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            {
                buttonSurfaceColor1 = buttonSurfaceColor1.ToGrayScale();
                buttonSurfaceColor2 = buttonSurfaceColor2.ToGrayScale();
            }

            using (Brush buttonSurfaceBrush = new LinearGradientBrush(buttonRectangle, buttonSurfaceColor1, buttonSurfaceColor2,
                       LinearGradientMode.Vertical))
            {
                g.FillPath(buttonSurfaceBrush, buttonPath);
            }

            //Draw button border
            var buttonBorderColor1 = ButtonNormalBorderColor1;
            var buttonBorderColor2 = ButtonNormalBorderColor2;

            if (ToggleSwitch.IsButtonPressed)
            {
                buttonBorderColor1 = ButtonPressedBorderColor1;
                buttonBorderColor2 = ButtonPressedBorderColor2;
            }
            else if (ToggleSwitch.IsButtonHovered)
            {
                buttonBorderColor1 = ButtonHoverBorderColor1;
                buttonBorderColor2 = ButtonHoverBorderColor2;
            }

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            {
                buttonBorderColor1 = buttonBorderColor1.ToGrayScale();
                buttonBorderColor2 = buttonBorderColor2.ToGrayScale();
            }

            using (Brush buttonBorderBrush = new LinearGradientBrush(buttonRectangle, buttonBorderColor1, buttonBorderColor2,
                       LinearGradientMode.Vertical))
            {
                using (var buttonBorderPen = new Pen(buttonBorderBrush))
                {
                    g.DrawPath(buttonBorderPen, buttonPath);
                }
            }

            g.ResetClip();

            //Draw button image
            var buttonImage = ToggleSwitch.ButtonImage ??
                              (ToggleSwitch.Checked ? ToggleSwitch.OnButtonImage : ToggleSwitch.OffButtonImage);

            if (buttonImage != null)
            {
                g.SetClip(buttonPath);

                var alignment = ToggleSwitch.ButtonImage != null ? ToggleSwitch.ButtonAlignment :
                    ToggleSwitch.Checked ? ToggleSwitch.OnButtonAlignment : ToggleSwitch.OffButtonAlignment;

                var imageSize = buttonImage.Size;

                Rectangle imageRectangle;

                var imageXPos = buttonRectangle.X;

                var scaleImage = ToggleSwitch.ButtonImage != null ? ToggleSwitch.ButtonScaleImageToFit :
                    ToggleSwitch.Checked ? ToggleSwitch.OnButtonScaleImageToFit : ToggleSwitch.OffButtonScaleImageToFit;

                if (scaleImage)
                {
                    var canvasSize = buttonRectangle.Size;
                    var resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                    if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Center)
                        imageXPos = (int)(buttonRectangle.X + (buttonRectangle.Width - (float)resizedImageSize.Width) / 2);
                    else if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Right)
                        imageXPos = (int)(buttonRectangle.X + (float)buttonRectangle.Width - resizedImageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(buttonRectangle.Y + (buttonRectangle.Height - (float)resizedImageSize.Height) / 2),
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
                        imageXPos = (int)(buttonRectangle.X + (buttonRectangle.Width - (float)imageSize.Width) / 2);
                    else if (alignment == ToggleSwitch.ToggleSwitchButtonAlignment.Right)
                        imageXPos = (int)(buttonRectangle.X + (float)buttonRectangle.Width - imageSize.Width);

                    imageRectangle = new Rectangle(imageXPos,
                        (int)(buttonRectangle.Y + (buttonRectangle.Height - (float)imageSize.Height) / 2), imageSize.Width,
                        imageSize.Height);

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        g.DrawImage(buttonImage, imageRectangle, 0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel,
                            ImageHelper.GetGrayscaleAttributes());
                    else
                        g.DrawImageUnscaled(buttonImage, imageRectangle);
                }

                g.ResetClip();
            }
        }
    }

    #endregion Render Method Implementations

    #region Helper Method Implementations

    public GraphicsPath GetRoundedRectanglePath(Rectangle rectangle, int radius)
    {
        var gp = new GraphicsPath();
        var diameter = 2 * radius;

        if (diameter > ToggleSwitch.Height)
            diameter = ToggleSwitch.Height;

        if (diameter > ToggleSwitch.Width)
            diameter = ToggleSwitch.Width;

        gp.AddArc(rectangle.X, rectangle.Y, diameter, diameter, 180, 90);
        gp.AddArc(rectangle.X + rectangle.Width - diameter, rectangle.Y, diameter, diameter, 270, 90);
        gp.AddArc(rectangle.X + rectangle.Width - diameter, rectangle.Y + rectangle.Height - diameter, diameter, diameter, 0, 90);
        gp.AddArc(rectangle.X, rectangle.Y + rectangle.Height - diameter, diameter, diameter, 90, 90);
        gp.CloseFigure();

        return gp;
    }

    public override int GetButtonWidth()
    {
        var buttonWidth = 1.53f * (ToggleSwitch.Height - 2);
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