using System.Text;
using SharpEmf.Enums;

namespace SharpEmf.Svg;

internal static class Utils
{
    public static void AppendFill(StringBuilder svgSb, EmfState state)
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

    public static void AppendStroke(StringBuilder svgSb, EmfState state)
    {
        // TODO: use scaling from state
        var selectedPen = state.CurrentPlaybackDeviceContext.SelectedPen;
        if (selectedPen.PenStyle is PenStyle.PS_NULL)
        {
            svgSb.Append("stroke=\"none\" ");
        }

        // switch (state.CurrentPlaybackDeviceContext.SelectedGraphicsObject.LogPen.PenStyle)
        // {
        //     case PenStyle.PS_COSMETIC:
        //         sb.Append($"stroke-width=\"{stroke}\"");
        //         break;
        //     case PenStyle.PS_GEOMETRIC:
        //         sb.Append($"stroke-width=\"{stroke}\"");
        //         break;
        // }

        // TODO: handle other pen styles
    }
}