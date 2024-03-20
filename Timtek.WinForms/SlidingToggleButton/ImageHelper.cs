using System.Drawing.Imaging;

namespace Timtek.WinForms.SlidingToggleButton;

public static class ImageHelper
{
    private static readonly float[][] _colorMatrixElements =
    {
        new[] { (float)0.299, (float)0.299, (float)0.299, 0, 0 },
        new[] { (float)0.587, (float)0.587, (float)0.587, 0, 0 },
        new[] { (float)0.114, (float)0.114, (float)0.114, 0, 0 },
        new float[] { 0, 0, 0, 1, 0 },
        new float[] { 0, 0, 0, 0, 1 }
    };

    private static readonly ColorMatrix _grayscaleColorMatrix = new(_colorMatrixElements);

    public static ImageAttributes GetGrayscaleAttributes()
    {
        var attr = new ImageAttributes();
        attr.SetColorMatrix(_grayscaleColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        return attr;
    }

    public static Size RescaleImageToFit(Size imageSize, Size canvasSize)
    {
        //Code "borrowed" from http://stackoverflow.com/questions/1940581/c-sharp-image-resizing-to-different-size-while-preserving-aspect-ratio
        // and the Math.Min improvement from http://stackoverflow.com/questions/6501797/resize-image-proportionally-with-maxheight-and-maxwidth-constraints

        // Figure out the ratio
        var ratioX = canvasSize.Width / (double)imageSize.Width;
        var ratioY = canvasSize.Height / (double)imageSize.Height;

        // use whichever multiplier is smaller
        var ratio = Math.Min(ratioX, ratioY);

        // now we can get the new height and width
        var newHeight = Convert.ToInt32(imageSize.Height * ratio);
        var newWidth = Convert.ToInt32(imageSize.Width * ratio);

        var resizedSize = new Size(newWidth, newHeight);

        if (resizedSize.Width > canvasSize.Width || resizedSize.Height > canvasSize.Height)
            throw new Exception("ImageHelper.RescaleImageToFit - Resize failed!");

        return resizedSize;
    }
}