using System.Drawing.Drawing2D;

namespace Timtek.WinForms.SlidingToggleButton.Renderers;

public class ToggleSwitchIOS5Renderer : ToggleSwitchRendererBase
{
    #region Constructor

    public ToggleSwitchIOS5Renderer()
    {
        BorderColor = Color.FromArgb(255, 202, 202, 202);
        LeftSideUpperColor1 = Color.FromArgb(255, 48, 115, 189);
        LeftSideUpperColor2 = Color.FromArgb(255, 17, 123, 220);
        LeftSideLowerColor1 = Color.FromArgb(255, 65, 143, 218);
        LeftSideLowerColor2 = Color.FromArgb(255, 130, 190, 243);
        LeftSideUpperBorderColor = Color.FromArgb(150, 123, 157, 196);
        LeftSideLowerBorderColor = Color.FromArgb(150, 174, 208, 241);
        RightSideUpperColor1 = Color.FromArgb(255, 191, 191, 191);
        RightSideUpperColor2 = Color.FromArgb(255, 229, 229, 229);
        RightSideLowerColor1 = Color.FromArgb(255, 232, 232, 232);
        RightSideLowerColor2 = Color.FromArgb(255, 251, 251, 251);
        RightSideUpperBorderColor = Color.FromArgb(150, 175, 175, 175);
        RightSideLowerBorderColor = Color.FromArgb(150, 229, 230, 233);
        ButtonShadowColor = Color.Transparent;
        ButtonNormalOuterBorderColor = Color.FromArgb(255, 149, 172, 194);
        ButtonNormalInnerBorderColor = Color.FromArgb(255, 235, 235, 235);
        ButtonNormalSurfaceColor1 = Color.FromArgb(255, 216, 215, 216);
        ButtonNormalSurfaceColor2 = Color.FromArgb(255, 251, 250, 251);
        ButtonHoverOuterBorderColor = Color.FromArgb(255, 141, 163, 184);
        ButtonHoverInnerBorderColor = Color.FromArgb(255, 223, 223, 223);
        ButtonHoverSurfaceColor1 = Color.FromArgb(255, 205, 204, 205);
        ButtonHoverSurfaceColor2 = Color.FromArgb(255, 239, 238, 239);
        ButtonPressedOuterBorderColor = Color.FromArgb(255, 111, 128, 145);
        ButtonPressedInnerBorderColor = Color.FromArgb(255, 176, 176, 176);
        ButtonPressedSurfaceColor1 = Color.FromArgb(255, 162, 161, 162);
        ButtonPressedSurfaceColor2 = Color.FromArgb(255, 187, 187, 187);
    }

    #endregion Constructor

    #region Public Properties

    public Color BorderColor { get; set; }
    public Color LeftSideUpperColor1 { get; set; }
    public Color LeftSideUpperColor2 { get; set; }
    public Color LeftSideLowerColor1 { get; set; }
    public Color LeftSideLowerColor2 { get; set; }
    public Color LeftSideUpperBorderColor { get; set; }
    public Color LeftSideLowerBorderColor { get; set; }
    public Color RightSideUpperColor1 { get; set; }
    public Color RightSideUpperColor2 { get; set; }
    public Color RightSideLowerColor1 { get; set; }
    public Color RightSideLowerColor2 { get; set; }
    public Color RightSideUpperBorderColor { get; set; }
    public Color RightSideLowerBorderColor { get; set; }
    public Color ButtonShadowColor { get; set; }
    public Color ButtonNormalOuterBorderColor { get; set; }
    public Color ButtonNormalInnerBorderColor { get; set; }
    public Color ButtonNormalSurfaceColor1 { get; set; }
    public Color ButtonNormalSurfaceColor2 { get; set; }
    public Color ButtonHoverOuterBorderColor { get; set; }
    public Color ButtonHoverInnerBorderColor { get; set; }
    public Color ButtonHoverSurfaceColor1 { get; set; }
    public Color ButtonHoverSurfaceColor2 { get; set; }
    public Color ButtonPressedOuterBorderColor { get; set; }
    public Color ButtonPressedInnerBorderColor { get; set; }
    public Color ButtonPressedSurfaceColor1 { get; set; }
    public Color ButtonPressedSurfaceColor2 { get; set; }

    #endregion Public Properties

    #region Render Method Implementations

    public override void RenderBorder(Graphics g, Rectangle borderRectangle)
    {
        //Draw this one AFTER the button is drawn in the RenderButton method
    }

    public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        var buttonWidth = GetButtonWidth();

        //Draw upper gradient field
        var gradientRectWidth = leftRectangle.Width + buttonWidth / 2;
        var upperGradientRectHeight = (int)(0.8 * (leftRectangle.Height - 2));

        var controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
        var controlClipPath = GetControlClipPath(controlRectangle);

        var upperGradientRectangle =
            new Rectangle(leftRectangle.X, leftRectangle.Y + 1, gradientRectWidth, upperGradientRectHeight - 1);

        g.SetClip(controlClipPath);
        g.IntersectClip(upperGradientRectangle);

        using (var upperGradientPath = new GraphicsPath())
        {
            upperGradientPath.AddArc(upperGradientRectangle.X, upperGradientRectangle.Y, ToggleSwitch.Height, ToggleSwitch.Height,
                135, 135);
            upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y,
                upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y);
            upperGradientPath.AddLine(upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y,
                upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y + upperGradientRectangle.Height);
            upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y + upperGradientRectangle.Height,
                upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y + upperGradientRectangle.Height);

            var upperColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? LeftSideUpperColor1.ToGrayScale()
                : LeftSideUpperColor1;
            var upperColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? LeftSideUpperColor2.ToGrayScale()
                : LeftSideUpperColor2;

            using (Brush upperGradientBrush =
                   new LinearGradientBrush(upperGradientRectangle, upperColor1, upperColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(upperGradientBrush, upperGradientPath);
            }
        }

        g.ResetClip();

        //Draw lower gradient field
        var lowerGradientRectHeight = (int)Math.Ceiling(0.5 * (leftRectangle.Height - 2));

        var lowerGradientRectangle = new Rectangle(leftRectangle.X, leftRectangle.Y + leftRectangle.Height / 2, gradientRectWidth,
            lowerGradientRectHeight);

        g.SetClip(controlClipPath);
        g.IntersectClip(lowerGradientRectangle);

        using (var lowerGradientPath = new GraphicsPath())
        {
            lowerGradientPath.AddArc(1, lowerGradientRectangle.Y, (int)(0.75 * (ToggleSwitch.Height - 1)), ToggleSwitch.Height - 1,
                215, 45); //Arc from side to top
            lowerGradientPath.AddLine(lowerGradientRectangle.X + buttonWidth / 2, lowerGradientRectangle.Y,
                lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y);
            lowerGradientPath.AddLine(lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y,
                lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y + lowerGradientRectangle.Height);
            lowerGradientPath.AddLine(lowerGradientRectangle.X + buttonWidth / 4,
                lowerGradientRectangle.Y + lowerGradientRectangle.Height, lowerGradientRectangle.X + lowerGradientRectangle.Width,
                lowerGradientRectangle.Y + lowerGradientRectangle.Height);
            lowerGradientPath.AddArc(1, 1, ToggleSwitch.Height - 1, ToggleSwitch.Height - 1, 90, 70); //Arc from side to bottom

            var lowerColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? LeftSideLowerColor1.ToGrayScale()
                : LeftSideLowerColor1;
            var lowerColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? LeftSideLowerColor2.ToGrayScale()
                : LeftSideLowerColor2;

            using (Brush lowerGradientBrush =
                   new LinearGradientBrush(lowerGradientRectangle, lowerColor1, lowerColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(lowerGradientBrush, lowerGradientPath);
            }
        }

        g.ResetClip();

        controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
        controlClipPath = GetControlClipPath(controlRectangle);

        g.SetClip(controlClipPath);

        //Draw upper inside border
        var upperBordercolor = LeftSideUpperBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            upperBordercolor = upperBordercolor.ToGrayScale();

        using (var upperBorderPen = new Pen(upperBordercolor))
        {
            g.DrawLine(upperBorderPen, leftRectangle.X, leftRectangle.Y + 1,
                leftRectangle.X + leftRectangle.Width + buttonWidth / 2, leftRectangle.Y + 1);
        }

        //Draw lower inside border
        var lowerBordercolor = LeftSideLowerBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            lowerBordercolor = lowerBordercolor.ToGrayScale();

        using (var lowerBorderPen = new Pen(lowerBordercolor))
        {
            g.DrawLine(lowerBorderPen, leftRectangle.X, leftRectangle.Y + leftRectangle.Height - 1,
                leftRectangle.X + leftRectangle.Width + buttonWidth / 2, leftRectangle.Y + leftRectangle.Height - 1);
        }

        //Draw image or text
        if (ToggleSwitch.OnSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OnText))
        {
            var fullRectangle = new RectangleF(leftRectangle.X + 2 - (totalToggleFieldWidth - leftRectangle.Width), 2,
                totalToggleFieldWidth - 2, ToggleSwitch.Height - 4);

            g.IntersectClip(fullRectangle);

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
        }

        g.ResetClip();
    }

    public override void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth)
    {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        var buttonRectangle = GetButtonRectangle();

        var controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
        var controlClipPath = GetControlClipPath(controlRectangle);

        //Draw upper gradient field
        var gradientRectWidth = rightRectangle.Width + buttonRectangle.Width / 2;
        var upperGradientRectHeight = (int)(0.8 * (rightRectangle.Height - 2));

        var upperGradientRectangle = new Rectangle(rightRectangle.X - buttonRectangle.Width / 2, rightRectangle.Y + 1,
            gradientRectWidth - 1, upperGradientRectHeight - 1);

        g.SetClip(controlClipPath);
        g.IntersectClip(upperGradientRectangle);

        using (var upperGradientPath = new GraphicsPath())
        {
            upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y,
                upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y);
            upperGradientPath.AddArc(upperGradientRectangle.X + upperGradientRectangle.Width - ToggleSwitch.Height + 1,
                upperGradientRectangle.Y - 1, ToggleSwitch.Height, ToggleSwitch.Height, 270, 115);
            upperGradientPath.AddLine(upperGradientRectangle.X + upperGradientRectangle.Width,
                upperGradientRectangle.Y + upperGradientRectangle.Height, upperGradientRectangle.X,
                upperGradientRectangle.Y + upperGradientRectangle.Height);
            upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y + upperGradientRectangle.Height,
                upperGradientRectangle.X, upperGradientRectangle.Y);

            var upperColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? RightSideUpperColor1.ToGrayScale()
                : RightSideUpperColor1;
            var upperColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? RightSideUpperColor2.ToGrayScale()
                : RightSideUpperColor2;

            using (Brush upperGradientBrush =
                   new LinearGradientBrush(upperGradientRectangle, upperColor1, upperColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(upperGradientBrush, upperGradientPath);
            }
        }

        g.ResetClip();

        //Draw lower gradient field
        var lowerGradientRectHeight = (int)Math.Ceiling(0.5 * (rightRectangle.Height - 2));

        var lowerGradientRectangle = new Rectangle(rightRectangle.X - buttonRectangle.Width / 2,
            rightRectangle.Y + rightRectangle.Height / 2, gradientRectWidth - 1, lowerGradientRectHeight);

        g.SetClip(controlClipPath);
        g.IntersectClip(lowerGradientRectangle);

        using (var lowerGradientPath = new GraphicsPath())
        {
            lowerGradientPath.AddLine(lowerGradientRectangle.X, lowerGradientRectangle.Y,
                lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y);
            lowerGradientPath.AddArc(
                lowerGradientRectangle.X + lowerGradientRectangle.Width - (int)(0.75 * (ToggleSwitch.Height - 1)),
                lowerGradientRectangle.Y, (int)(0.75 * (ToggleSwitch.Height - 1)), ToggleSwitch.Height - 1, 270,
                45); //Arc from top to side
            lowerGradientPath.AddArc(ToggleSwitch.Width - ToggleSwitch.Height, 0, ToggleSwitch.Height, ToggleSwitch.Height, 20,
                70); //Arc from side to bottom
            lowerGradientPath.AddLine(lowerGradientRectangle.X + lowerGradientRectangle.Width,
                lowerGradientRectangle.Y + lowerGradientRectangle.Height, lowerGradientRectangle.X,
                lowerGradientRectangle.Y + lowerGradientRectangle.Height);
            lowerGradientPath.AddLine(lowerGradientRectangle.X, lowerGradientRectangle.Y + lowerGradientRectangle.Height,
                lowerGradientRectangle.X, lowerGradientRectangle.Y);

            var lowerColor1 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? RightSideLowerColor1.ToGrayScale()
                : RightSideLowerColor1;
            var lowerColor2 = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
                ? RightSideLowerColor2.ToGrayScale()
                : RightSideLowerColor2;

            using (Brush lowerGradientBrush =
                   new LinearGradientBrush(lowerGradientRectangle, lowerColor1, lowerColor2, LinearGradientMode.Vertical))
            {
                g.FillPath(lowerGradientBrush, lowerGradientPath);
            }
        }

        g.ResetClip();

        controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
        controlClipPath = GetControlClipPath(controlRectangle);

        g.SetClip(controlClipPath);

        //Draw upper inside border
        var upperBordercolor = RightSideUpperBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            upperBordercolor = upperBordercolor.ToGrayScale();

        using (var upperBorderPen = new Pen(upperBordercolor))
        {
            g.DrawLine(upperBorderPen, rightRectangle.X - buttonRectangle.Width / 2, rightRectangle.Y + 1,
                rightRectangle.X + rightRectangle.Width, rightRectangle.Y + 1);
        }

        //Draw lower inside border
        var lowerBordercolor = RightSideLowerBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            lowerBordercolor = lowerBordercolor.ToGrayScale();

        using (var lowerBorderPen = new Pen(lowerBordercolor))
        {
            g.DrawLine(lowerBorderPen, rightRectangle.X - buttonRectangle.Width / 2, rightRectangle.Y + rightRectangle.Height - 1,
                rightRectangle.X + rightRectangle.Width, rightRectangle.Y + rightRectangle.Height - 1);
        }

        //Draw image or text
        if (ToggleSwitch.OffSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OffText))
        {
            var fullRectangle = new RectangleF(rightRectangle.X, 2, totalToggleFieldWidth - 2, ToggleSwitch.Height - 4);

            g.IntersectClip(fullRectangle);

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
        }

        g.ResetClip();
    }

    public override void RenderButton(Graphics g, Rectangle buttonRectangle)
    {
        if (ToggleSwitch.IsButtonOnLeftSide)
            buttonRectangle.X += 1;
        else if (ToggleSwitch.IsButtonOnRightSide)
            buttonRectangle.X -= 1;

        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //Draw button shadow
        buttonRectangle.Inflate(1, 1);

        var shadowClipRectangle = new Rectangle(buttonRectangle.Location, buttonRectangle.Size);
        shadowClipRectangle.Inflate(0, -1);

        if (ToggleSwitch.IsButtonOnLeftSide)
        {
            shadowClipRectangle.X += shadowClipRectangle.Width / 2;
            shadowClipRectangle.Width = shadowClipRectangle.Width / 2;
        }
        else if (ToggleSwitch.IsButtonOnRightSide)
        {
            shadowClipRectangle.Width = shadowClipRectangle.Width / 2;
        }

        g.SetClip(shadowClipRectangle);

        var buttonShadowColor = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled
            ? ButtonShadowColor.ToGrayScale()
            : ButtonShadowColor;

        using (var buttonShadowPen = new Pen(buttonShadowColor))
        {
            g.DrawEllipse(buttonShadowPen, buttonRectangle);
        }

        g.ResetClip();

        buttonRectangle.Inflate(-1, -1);

        //Draw outer button border
        var buttonOuterBorderColor = ButtonNormalOuterBorderColor;

        if (ToggleSwitch.IsButtonPressed)
            buttonOuterBorderColor = ButtonPressedOuterBorderColor;
        else if (ToggleSwitch.IsButtonHovered)
            buttonOuterBorderColor = ButtonHoverOuterBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            buttonOuterBorderColor = buttonOuterBorderColor.ToGrayScale();

        using (Brush outerBorderBrush = new SolidBrush(buttonOuterBorderColor))
        {
            g.FillEllipse(outerBorderBrush, buttonRectangle);
        }

        //Draw inner button border
        buttonRectangle.Inflate(-1, -1);

        var buttonInnerBorderColor = ButtonNormalInnerBorderColor;

        if (ToggleSwitch.IsButtonPressed)
            buttonInnerBorderColor = ButtonPressedInnerBorderColor;
        else if (ToggleSwitch.IsButtonHovered)
            buttonInnerBorderColor = ButtonHoverInnerBorderColor;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            buttonInnerBorderColor = buttonInnerBorderColor.ToGrayScale();

        using (Brush innerBorderBrush = new SolidBrush(buttonInnerBorderColor))
        {
            g.FillEllipse(innerBorderBrush, buttonRectangle);
        }

        //Draw button surface
        buttonRectangle.Inflate(-1, -1);

        var buttonUpperSurfaceColor = ButtonNormalSurfaceColor1;

        if (ToggleSwitch.IsButtonPressed)
            buttonUpperSurfaceColor = ButtonPressedSurfaceColor1;
        else if (ToggleSwitch.IsButtonHovered)
            buttonUpperSurfaceColor = ButtonHoverSurfaceColor1;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            buttonUpperSurfaceColor = buttonUpperSurfaceColor.ToGrayScale();

        var buttonLowerSurfaceColor = ButtonNormalSurfaceColor2;

        if (ToggleSwitch.IsButtonPressed)
            buttonLowerSurfaceColor = ButtonPressedSurfaceColor2;
        else if (ToggleSwitch.IsButtonHovered)
            buttonLowerSurfaceColor = ButtonHoverSurfaceColor2;

        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
            buttonLowerSurfaceColor = buttonLowerSurfaceColor.ToGrayScale();

        using (Brush buttonSurfaceBrush = new LinearGradientBrush(buttonRectangle, buttonUpperSurfaceColor, buttonLowerSurfaceColor,
                   LinearGradientMode.Vertical))
        {
            g.FillEllipse(buttonSurfaceBrush, buttonRectangle);
        }

        g.CompositingMode = CompositingMode.SourceOver;
        g.CompositingQuality = CompositingQuality.HighQuality;

        //Draw outer control border
        var controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);

        using (var borderPath = GetControlClipPath(controlRectangle))
        {
            var controlBorderColor =
                !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled ? BorderColor.ToGrayScale() : BorderColor;

            using (var borderPen = new Pen(controlBorderColor))
            {
                g.DrawPath(borderPen, borderPath);
            }
        }

        g.ResetClip();

        //Draw button image
        var buttonImage = ToggleSwitch.ButtonImage ??
                          (ToggleSwitch.Checked ? ToggleSwitch.OnButtonImage : ToggleSwitch.OffButtonImage);

        if (buttonImage != null)
        {
            g.SetClip(GetButtonClipPath());

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

    #endregion Render Method Implementations

    #region Helper Method Implementations

    public GraphicsPath GetControlClipPath(Rectangle controlRectangle)
    {
        var borderPath = new GraphicsPath();
        borderPath.AddArc(controlRectangle.X, controlRectangle.Y, controlRectangle.Height, controlRectangle.Height, 90, 180);
        borderPath.AddArc(controlRectangle.X + controlRectangle.Width - controlRectangle.Height, controlRectangle.Y,
            controlRectangle.Height, controlRectangle.Height, 270, 180);
        borderPath.CloseFigure();

        return borderPath;
    }

    public GraphicsPath GetButtonClipPath()
    {
        var buttonRectangle = GetButtonRectangle();

        var buttonPath = new GraphicsPath();

        buttonPath.AddArc(buttonRectangle.X, buttonRectangle.Y, buttonRectangle.Height, buttonRectangle.Height, 0, 360);

        return buttonPath;
    }

    public override int GetButtonWidth()
    {
        return ToggleSwitch.Height - 2;
    }

    public override Rectangle GetButtonRectangle()
    {
        var buttonWidth = GetButtonWidth();
        return GetButtonRectangle(buttonWidth);
    }

    public override Rectangle GetButtonRectangle(int buttonWidth)
    {
        var buttonRect = new Rectangle(ToggleSwitch.ButtonValue, 1, buttonWidth, buttonWidth);
        return buttonRect;
    }

    #endregion Helper Method Implementations
}