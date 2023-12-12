using System.Text;
using SharpEmf.Enums;
using SharpEmf.Records.Control.Header;
using SharpEmf.Records.Drawing;
using SharpEmf.Records.ObjectCreation;
using SharpEmf.Records.ObjectManipulation;
using SharpEmf.Records.State;

namespace SharpEmf.Svg;

public static class EmfSvgWriter
{
    public static string ConvertToSvg(EnhancedMetafile emf)
    {
        var sb = new StringBuilder();
        var state = new EmfState();

        HandleHeaderRecord(sb, state, emf.Header);

        foreach (var record in emf.Records)
        {
            switch (record)
            {
                case EmrSetMapMode setMapMode:
                    HandleSetMapModeRecord(state, setMapMode);
                    break;
                case EmrSetBkMode setBkMode:
                    HandleSetBkModeRecord(state, setBkMode);
                    break;
                case EmrSetWindowOrgEx setWindowOrgEx:
                    HandleSetWindowOrgExRecord(state, setWindowOrgEx);
                    break;
                case EmrSetViewportOrgEx setViewportOrgEx:
                    HandleSetViewportOrgExRecord(state, setViewportOrgEx);
                    break;
                case EmrSetWindowExtEx setWindowExtEx:
                    HandleSetWindowExtExRecord(state, setWindowExtEx);
                    break;
                case EmrSetViewportExtEx setViewportExtEx:
                    HandleSetViewportExtExRecord(state, setViewportExtEx);
                    break;
                case EmrSetPolyfillMode setPolyfillMode:
                    HandleSetPolyfillMode(state, setPolyfillMode);
                    break;
                case EmrCreateBrushIndirect createBrushIndirect:
                    HandleCreateBrushIndirect(state, createBrushIndirect);
                    break;
                case EmrSelectObject selectObject:
                    HandleSelectObject(state, selectObject);
                    break;
                case EmrExtCreatePen extCreatePen:
                    HandleExtCreatePen(state, extCreatePen);
                    break;
                case EmrPolyPolygon16 polyPolygon16:
                    HandlePolyPolygon16(sb, state, polyPolygon16);
                    break;
            }
        }

        sb.AppendLine("</g>");
        sb.AppendLine("</svg>");
        return sb.ToString();
    }

    private static void HandleHeaderRecord(StringBuilder svgSb, EmfState state, EmfMetafileHeader header)
    {
        var width = header.Bounds.Right - header.Bounds.Left;
        var height = header.Bounds.Bottom - header.Bounds.Top;
        var gTransform = $"translate({-header.Bounds.Left},{-header.Bounds.Top})";

        state.Scaling = width / MathF.Abs(header.Bounds.Right - header.Bounds.Left);

        // TODO: +1 is a hack to make the object table start at index 1
        state.ObjectTable = new GraphicsObject[header.Handles + 1];

        svgSb.AppendLine(
            $"""
             <?xml version="1.0" encoding="UTF-8" standalone="no"?>
             <svg xmlns="http://www.w3.org/2000/svg" width="{width + 1}" height="{height + 1}">
             <g transform="{gTransform}">
             """);
    }

    private static void HandleSetMapModeRecord(EmfState state, EmrSetMapMode setMapMode)
    {
        state.MapMode = setMapMode.MapMode;
    }

    private static void HandleSetBkModeRecord(EmfState state, EmrSetBkMode setBkMode)
    {
        state.CurrentPlaybackDeviceContext.BkMode = setBkMode.BackgroundMode;
    }

    private static void HandleSetWindowOrgExRecord(EmfState state, EmrSetWindowOrgEx setWindowOrgEx)
    {
        state.WindowOrigin = setWindowOrgEx.Origin;
    }

    private static void HandleSetViewportOrgExRecord(EmfState state, EmrSetViewportOrgEx setViewportOrgEx)
    {
        state.ViewportOrigin = setViewportOrgEx.Origin;
    }

    private static void HandleSetWindowExtExRecord(EmfState state, EmrSetWindowExtEx setWindowExtEx)
    {
        state.WindowExtent = setWindowExtEx.Extent;
    }

    private static void HandleSetViewportExtExRecord(EmfState state, EmrSetViewportExtEx setViewportExtEx)
    {
        state.ViewportExtent = setViewportExtEx.Extent;
    }

    private static void HandleSetPolyfillMode(EmfState state, EmrSetPolyfillMode setPolyfillMode)
    {
        state.CurrentPlaybackDeviceContext.PolyFillMode = setPolyfillMode.PolygonFillMode;
    }

    private static void HandleCreateBrushIndirect(EmfState state, EmrCreateBrushIndirect createBrushIndirect)
    {
        var index = createBrushIndirect.IHBrush;

        state.ObjectTable[index].LogBrush = createBrushIndirect.LogBrush;
        state.ObjectTable[index].Type = GraphicsObjectType.Brush;
    }

    private static void HandleSelectObject(EmfState state, EmrSelectObject selectObject)
    {
        var index = selectObject.IHObject;

        // TODO: Handle stock objects

        var graphicsObject = state.ObjectTable[index];

        if (graphicsObject.Type is GraphicsObjectType.Pen)
        {
            state.CurrentPlaybackDeviceContext.SelectedPen = graphicsObject.LogPen;
        }
        else if (graphicsObject.Type is GraphicsObjectType.Brush)
        {
            state.CurrentPlaybackDeviceContext.SelectedBrush = graphicsObject.LogBrush;
        }
        else if (graphicsObject.Type is GraphicsObjectType.Unknown)
        {
            Console.WriteLine("Warning: Unknown object type selected");
        }
    }

    private static void HandleExtCreatePen(EmfState state, EmrExtCreatePen extCreatePen)
    {
        var index = extCreatePen.IHPen;

        state.ObjectTable[index].LogPen = extCreatePen.Elp;
        state.ObjectTable[index].Type = GraphicsObjectType.Pen;
    }

    private static void HandlePolyPolygon16(StringBuilder svgSb, EmfState state, EmrPolyPolygon16 polyPolygon16)
    {
        var points = polyPolygon16.APoints;
        var polygonPointCounts = polyPolygon16.PolygonPointCount;

        var scalingForMapMode = state.GetScalingForCurrentMapMode();
        var scaleMatrix = $"matrix({scalingForMapMode.X},0,0,{scalingForMapMode.Y},0,0)";

        svgSb.Append($"<path transform=\"{scaleMatrix}\" d=\"");

        int totalPointsProcessed = 0;
        foreach (var pointCount in polygonPointCounts)
        {
            var polygonPoints = points.Skip(totalPointsProcessed).Take((int)pointCount).ToList();

            var scaledPoint = (polygonPoints[0].X, polygonPoints[0].Y);
            svgSb.Append($"M {scaledPoint.X} {scaledPoint.Y} ");

            for (var j = 1; j < polygonPoints.Count; j++)
            {
                scaledPoint = (polygonPoints[j].X, polygonPoints[j].Y);
                svgSb.Append($"L {scaledPoint.X} {scaledPoint.Y} ");
            }

            svgSb.Append('Z');

            totalPointsProcessed += (int)pointCount;
        }

        svgSb.Append("\" ");

        AppendFill(svgSb, state);
        AppendStroke(svgSb, state);

        // switch (state.CurrentPlaybackDeviceContext.SelectedGraphicsObject.LogPen.PenStyle)
        // {
        //     case PenStyle.PS_COSMETIC:
        //         sb.Append($"stroke-width=\"{stroke}\"");
        //         break;
        //     case PenStyle.PS_GEOMETRIC:
        //         sb.Append($"stroke-width=\"{stroke}\"");
        //         break;
        // }


        svgSb.AppendLine(" />");
    }

    private static void AppendFill(StringBuilder svgSb, EmfState state)
    {
        var fillRuleStr = state.CurrentPlaybackDeviceContext.PolyFillMode switch
        {
            PolygonFillMode.WINDING => "fill-rule=\"nonzero\" ",
            PolygonFillMode.ALTERNATE => "fill-rule=\"evenodd\" ",
            _ => ""
        };

        switch (state.CurrentPlaybackDeviceContext.SelectedBrush.BrushStyle)
        {
            case BrushStyle.BS_NULL:
                svgSb.Append("fill=\"none\" ");
                break;
            case BrushStyle.BS_SOLID:
                svgSb.Append(fillRuleStr);

                svgSb.Append($"fill=\"#{state.CurrentPlaybackDeviceContext.SelectedBrush.Color.Red:X2}");
                svgSb.Append($"{state.CurrentPlaybackDeviceContext.SelectedBrush.Color.Green:X2}");
                svgSb.Append($"{state.CurrentPlaybackDeviceContext.SelectedBrush.Color.Blue:X2}\" ");
                break;
            case BrushStyle.BS_HATCHED:
            case BrushStyle.BS_PATTERN:
            case BrushStyle.BS_INDEXED:
            case BrushStyle.BS_DIBPATTERN:
            case BrushStyle.BS_DIBPATTERNPT:
            case BrushStyle.BS_PATTERN8X8:
            case BrushStyle.BS_DIBPATTERN8X8:
            default:
                svgSb.Append($"fill=\"#{state.CurrentPlaybackDeviceContext.SelectedBrush.Color.Red:X2}");
                svgSb.Append($"{state.CurrentPlaybackDeviceContext.SelectedBrush.Color.Green:X2}");
                svgSb.Append($"{state.CurrentPlaybackDeviceContext.SelectedBrush.Color.Blue:X2}\" ");
                break;
        }
    }

    private static void AppendStroke(StringBuilder svgSb, EmfState state)
    {
        // TODO: use scaling from state
        if (state.CurrentPlaybackDeviceContext.SelectedPen.PenStyle is PenStyle.PS_NULL)
        {
            svgSb.Append("stroke=\"none\" ");
        }
    }
}