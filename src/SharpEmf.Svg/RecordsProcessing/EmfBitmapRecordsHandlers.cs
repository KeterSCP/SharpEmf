using System.Text;
using SharpEmf.Records.Bitmap;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfBitmapRecordsHandlers
{
    public static void HandleStretchDIBits(StringBuilder svgSb, EmfState state, EmrStretchDiBits stretchDiBits)
    {
        // TODO: fix this
        var bitmapBase64 = Convert.ToBase64String(stretchDiBits.BitsSrc);

        var scalingForMapMode = state.GetScalingForCurrentMapMode();
        var scaleMatrix = $"matrix({scalingForMapMode.X},0,0,{scalingForMapMode.Y},0,0)";

        var x = stretchDiBits.XDest;
        var y = stretchDiBits.YDest;
        var width = stretchDiBits.CXDest;
        var height = stretchDiBits.CYDest;


        svgSb.AppendLine($"<image transform=\"{scaleMatrix}\" x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" xlink:href=\"data:image/bmp;base64,{bitmapBase64}\" />");
    }
}