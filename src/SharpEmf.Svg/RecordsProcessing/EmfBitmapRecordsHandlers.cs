using System.Text;
using SharpEmf.Records.Bitmap;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfBitmapRecordsHandlers
{
    public static void HandleStretchDIBits(StringBuilder svgSb, EmfState state, EmrStretchDiBits stretchDiBits)
    {
        var bitmapBase64 = BitmapUtils.DibToPngBase64(stretchDiBits.BitsSrc, stretchDiBits.BmiHeader);

        var x = stretchDiBits.XDest;
        var y = stretchDiBits.YDest;
        var width = stretchDiBits.CXDest;
        var height = stretchDiBits.CYDest;

        svgSb.AppendLine($"<image x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" href=\"data:image/png;base64,{bitmapBase64}\" />");
    }
}