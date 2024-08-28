using System.Text;
using SharpEmf.Enums;
using SharpEmf.Records.Clipping;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfClippingRecordsHandlers
{
    public static void HandleEmrSelectClipPath(StringBuilder svgSb, EmfState state, EmrSelectClipPath selectClipPath)
    {
        if (state.InPath) return;


        svgSb.AppendLine("<defs>");
        svgSb.AppendLine($"<clipPath id=\"{PlaybackDeviceContext.ClipId++}\">");

        switch (selectClipPath.RegionMode)
        {
            case RegionMode.RGN_AND:
                break;
        }

        svgSb.AppendLine("</clipPath>");
        svgSb.AppendLine("</defs>");
    }
}