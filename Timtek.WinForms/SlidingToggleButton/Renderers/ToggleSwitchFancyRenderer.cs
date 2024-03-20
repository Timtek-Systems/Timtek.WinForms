using System.Drawing.Drawing2D;

namespace Timtek.WinForms.SlidingToggleButton.Renderers;

public class ToggleSwitchFancyRenderer : ToggleSwitchRendererBase, IDisposable
{
    #region Constructor

    private GraphicsPath _innerControlPath;

    public ToggleSwitchFancyRenderer()
    {
        OuterBorderColor1 = Color.FromArgb(255, 197, 199, 201);
        OuterBorderColor2 = Color.FromArgb(255, 207, 209, 212);
        InnerBorderColor1 = Color.FromArgb(200, 205, 208, 207);
        InnerBorderColor2 = Color.FromArgb(255, 207, 209, 212);
        LeftSideBackColor1 = Color.FromArgb(255, 61, 110, 6);
        LeftSideBackColor2 = Color.FromArgb(255, 93, 170, 9);
        RightSideBackColor1 = Color.FromArgb(255, 149, 0, 0);
        RightSideBackColor2 = Color.FromArgb(255, 198, 0, 0);
        ButtonNormalBorderColor1 = Color.FromArgb(255, 212, 209, 211);
        ButtonNormalBorderColor2 = Color.FromArgb(255, 197, 199, 201);
        ButtonNormalUpperSurfaceColor1 = Color.FromArgb(255, 252, 251, 252);
        ButtonNormalUpperSurfaceColor2 = Color.FromArgb(255, 247, 247, 247);
        ButtonNormalLowerSurfaceColor1 = Color.FromArgb(255, 233, 233, 233);
        ButtonNormalLowerSurfaceColor2 = Color.FromArgb(255, 225, 225, 225);
        ButtonHoverBorderColor1 = Color.FromArgb(255, 212, 207, 209);
        ButtonHoverBorderColor2 = Color.FromArgb(255, 223, 223, 223);
        ButtonHoverUpperSurfaceColor1 = Color.FromArgb(255, 240, 239, 240);
        ButtonHoverUpperSurfaceColor2 = Color.FromArgb(255, 235, 235, 235);
        ButtonHoverLowerSurfaceColor1 = Color.FromArgb(255, 221, 221, 221);
        ButtonHoverLowerSurfaceColor2 = Color.FromArgb(255, 214, 214, 214);
        ButtonPressedBorderColor1 = Color.FromArgb(255, 176, 176, 176);
        ButtonPressedBorderColor2 = Color.FromArgb(255, 176, 176, 176);
        ButtonPressedUpperSurfaceColor1 = Color.FromArgb(255, 189, 188, 189);
        ButtonPressedUpperSurfaceColor2 = Color.FromArgb(255, 185, 185, 185);
        ButtonPressedLowerSurfaceColor1 = Color.FromArgb(255, 175, 175, 175);
        ButtonPressedLowerSurfaceColor2 = Color.FromArgb(255, 169, 169, 169);
        ButtonShadowColor1 = Color.FromArgb(50, 0, 0, 0);
        ButtonShadowColor2 = Color.FromArgb(0, 0, 0, 0);

        ButtonShadowWidth = 7;
        CornerRadius = 6;
    }

    public void Dispose()
    {
        if (_innerControlPath != null)
            _innerControlPath.Dispose();
    }

    #endregion Constructor

    #region Public Properties

    public Color OuterBorderColor1 { get; set; }
    public Color OuterBorderColor2 { get; set; }
    public Color InnerBorderColor1 { get; set; }
    public Color InnerBorderColor2 { get; set; }
    public Color LeftSideBackColor1 { get; set; }
    public Color LeftSideBackColor2 { get; set; }
    public Color RightSideBackColor1 { get; set; }
    public Color RightSideBackColor2 { get; set; }
    public Color ButtonNormalBorderColor1 { get; set; }
    public Color ButtonNormalBorderColor2 { get; set; }
    public Color ButtonNormalUpperSurfaceColor1 { get; set; }
    public Color ButtonNormalUpperSurfaceColor2 { get; set; }
    public Color ButtonNormalLowerSurfaceColor1 { get; set; }
    public Color ButtonNormalLowerSurfaceColor2 { get; set; }
    public Color ButtonHoverBorderColor1 { get; set; }
    public Color ButtonHoverBorderColor2 { get; set; }
    public Color ButtonHoverUpperSurfaceColor1 { get; set; }
    public Color ButtonHoverUpperSurfaceColor2 { get; set; }
    public Color ButtonHoverLowerSurfaceColor1 { get; set; }
    public Color ButtonHoverLowerSurfaceColor2 { get; set; }
    public Color ButtonPressedBorderColor1 { get; set; }
    public Color ButtonPressedBorderColor2 { get; set; }
    public Color ButtonPressedUpperSurfaceColor1 { get; set; }
    public Color ButtonPressedUpperSurfaceColor2 { get; set; }
    public Color ButtonPressedLowerSurfaceColor1 { get; set; }
    public Color ButtonPressedLowerSurfaceColor2 { get; set; }
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

            var outerBorderColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? OuterBorderColor1.ToGrayScale()
                : OuterBorderColor1;
            var outerBorderColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? OuterBorderColor2.ToGrayScale()
                : OuterBorderColor2;

            using (Brush outerBorderBrush = new LinearGradientBrush(borderRectangle, outerBorderColor1, outerBorderColor2,
                       LinearGradientMode.Vertical))
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

            var innerBorderColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? InnerBorderColor1.ToGrayScale()
                : InnerBorderColor1;
            var innerBorderColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? InnerBorderColor2.ToGrayScale()
                : InnerBorderColor2;

            using (Brush borderBrush = new LinearGradientBrush(borderRectangle, innerBorderColor1, innerBorderColor2,
                       LinearGradientMode.Vertical))
            {
                g.FillPath(borderBrush, innerBorderPath);
            }

            g.ResetClip();
        }

        var backgroundRectangle = new Rectangle(borderRectangle.X + 2, borderRectangle.Y + 2, borderRectangle.Width - 4,
            borderRectangle.Height - 4);
        _innerControlPath = GetRoundedRectanglePath(backgroundRectangle, CornerRadius);
    }

    public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;

        var buttonWidth = GetButtonWidth();

        //Draw inner background
        var gradientRectWidth = leftRectangle.Width + buttonWidth / 2;
        var gradientRectangle = new Rectangle(leftRectangle.X, leftRectangle.Y, gradientRectWidth, leftRectangle.Height);

        var leftSideBackColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
            ? LeftSideBackColor1.ToGrayScale()
            : LeftSideBackColor1;
        var leftSideBackColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
            ? LeftSideBackColor2.ToGrayScale()
            : LeftSideBackColor2;

        if (_innerControlPath != null)
        {
            g.SetClip(_innerControlPath);
            g.IntersectClip(gradientRectangle);
        }
        else
        {
            g.SetClip(gradientRectangle);
        }

        using (Brush backgroundBrush = new LinearGradientBrush(gradientRectangle, leftSideBackColor1, leftSideBackColor2,
                   LinearGradientMode.Vertical))
        {
            g.FillRectangle(backgroundBrush, gradientRectangle);
        }

        g.ResetClip();

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

        var buttonWidth = GetButtonWidth();

        //Draw inner background
        var gradientRectWidth = rightRectangle.Width + buttonWidth / 2;
        var gradientRectangle = new Rectangle(rightRectangle.X - buttonWidth / 2, rightRectangle.Y, gradientRectWidth,
            rightRectangle.Height);

        var rightSideBackColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
            ? RightSideBackColor1.ToGrayScale()
            : RightSideBackColor1;
        var rightSideBackColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
            ? RightSideBackColor2.ToGrayScale()
            : RightSideBackColor2;

        if (_innerControlPath != null)
        {
            g.SetClip(_innerControlPath);
            g.IntersectClip(gradientRectangle);
        }
        else
        {
            g.SetClip(gradientRectangle);
        }

        using (Brush backgroundBrush = new LinearGradientBrush(gradientRectangle, rightSideBackColor1, rightSideBackColor2,
                   LinearGradientMode.Vertical))
        {
            g.FillRectangle(backgroundBrush, gradientRectangle);
        }

        g.ResetClip();

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

        //Draw button surface
        var buttonUpperSurfaceColor1 = ButtonNormalUpperSurfaceColor1;
        var buttonUpperSurfaceColor2 = ButtonNormalUpperSurfaceColor2;
        var buttonLowerSurfaceColor1 = ButtonNormalLowerSurfaceColor1;
        var buttonLowerSurfaceColor2 = ButtonNormalLowerSurfaceColor2;

        if (ToggleSwitch.IsButtonPressed)
        {
            buttonUpperSurfaceColor1 = ButtonPressedUpperSurfaceColor1;
            buttonUpperSurfaceColor2 = ButtonPressedUpperSurfaceColor2;
            buttonLowerSurfaceColor1 = ButtonPressedLowerSurfaceColor1;
            buttonLowerSurfaceColor2 = ButtonPressedLowerSurfaceColor2;
        }
        else if (ToggleSwitch.IsButtonHovered)
        {
            buttonUpperSurfaceColor1 = ButtonHoverUpperSurfaceColor1;
            buttonUpperSurfaceColor2 = ButtonHoverUpperSurfaceColor2;
            buttonLowerSurfaceColor1 = ButtonHoverLowerSurfaceColor1;
            buttonLowerSurfaceColor2 = ButtonHoverLowerSurfaceColor2;
        }

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
        {
            buttonUpperSurfaceColor1 = buttonUpperSurfaceColor1.ToGrayScale();
            buttonUpperSurfaceColor2 = buttonUpperSurfaceColor2.ToGrayScale();
            buttonLowerSurfaceColor1 = buttonLowerSurfaceColor1.ToGrayScale();
            buttonLowerSurfaceColor2 = buttonLowerSurfaceColor2.ToGrayScale();
        }

        buttonRectangle.Inflate(-1, -1);

        var upperHeight = buttonRectangle.Height / 2;

        var upperGradientRect = new Rectangle(buttonRectangle.X, buttonRectangle.Y, buttonRectangle.Width, upperHeight);
        var lowerGradientRect = new Rectangle(buttonRectangle.X, buttonRectangle.Y + upperHeight, buttonRectangle.Width,
            buttonRectangle.Height - upperHeight);

        using (var buttonPath = GetRoundedRectanglePath(buttonRectangle, CornerRadius))
        {
            g.SetClip(buttonPath);
            g.IntersectClip(upperGradientRect);

            //Draw upper button surface gradient
            using (Brush buttonUpperSurfaceBrush = new LinearGradientBrush(buttonRectangle, buttonUpperSurfaceColor1,
                       buttonUpperSurfaceColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(buttonUpperSurfaceBrush, buttonPath);
            }

            g.ResetClip();

            g.SetClip(buttonPath);
            g.IntersectClip(lowerGradientRect);

            //Draw lower button surface gradient
            using (Brush buttonLowerSurfaceBrush = new LinearGradientBrush(buttonRectangle, buttonLowerSurfaceColor1,
                       buttonLowerSurfaceColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(buttonLowerSurfaceBrush, buttonPath);
            }

            g.ResetClip();

            g.SetClip(buttonPath);

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
        var buttonWidth = 1.61f * ToggleSwitch.Height;
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