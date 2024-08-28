using System.Text;
using SharpEmf.Records.State;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfStateRecordsHandlers
{
    public static void HandleSetMapModeRecord(EmfState state, EmrSetMapMode setMapMode)
    {
        state.MapMode = setMapMode.MapMode;
    }

    public static void HandleSetBkModeRecord(EmfState state, EmrSetBkMode setBkMode)
    {
        state.CurrentPlaybackDeviceContext.BkMode = setBkMode.BackgroundMode;
    }

    public static void HandleSetWindowOrgExRecord(EmfState state, EmrSetWindowOrgEx setWindowOrgEx)
    {
        state.WindowOrigin = setWindowOrgEx.Origin;
    }

    public static void HandleSetViewportOrgExRecord(EmfState state, EmrSetViewportOrgEx setViewportOrgEx)
    {
        state.ViewportOrigin = setViewportOrgEx.Origin;
    }

    public static void HandleSetWindowExtExRecord(EmfState state, EmrSetWindowExtEx setWindowExtEx)
    {
        state.WindowExtent = setWindowExtEx.Extent;
    }

    public static void HandleSetViewportExtExRecord(EmfState state, EmrSetViewportExtEx setViewportExtEx)
    {
        state.ViewportExtent = setViewportExtEx.Extent;
    }

    public static void HandleSetPolyfillMode(EmfState state, EmrSetPolyfillMode setPolyfillMode)
    {
        state.CurrentPlaybackDeviceContext.PolyFillMode = setPolyfillMode.PolygonFillMode;
    }

    public static void HandleMoveToEx(StringBuilder svgSb, EmfState state, EmrMoveToEx moveToEx)
    {
        if (state.InPath)
        {
            svgSb.Append($"M {moveToEx.Offset.X} {moveToEx.Offset.Y} ");
        }
        else
        {
            // TODO: set current position in state
        }
    }

    public static void HandleSetTextColor(EmfState state, EmrSetTextColor setTextColor)
    {
        state.CurrentPlaybackDeviceContext.TextColor = setTextColor.Color;
    }
}