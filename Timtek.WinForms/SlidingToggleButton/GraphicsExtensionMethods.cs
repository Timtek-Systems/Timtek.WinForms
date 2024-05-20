namespace Timtek.WinForms.SlidingToggleButton;

public static class GraphicsExtensionMethods
{
    public static Color ToGrayScale(this Color originalColor)
    {
        if (originalColor.Equals(Color.Transparent))
            return originalColor;

        var grayScale = (int)(originalColor.R * .299 + originalColor.G * .587 + originalColor.B * .114);
        return Color.FromArgb(grayScale, grayScale, grayScale);
    }
}