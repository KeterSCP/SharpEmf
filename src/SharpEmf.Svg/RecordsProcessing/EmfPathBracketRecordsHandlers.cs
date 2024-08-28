using System.Text;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfPathBracketRecordsHandlers
{
    public static void HandleBeginPath(StringBuilder svgSb, EmfState state)
    {
        state.InPath = true;

        var scalingForMapMode = state.GetScalingForCurrentMapMode();
        var scaleMatrix = $"matrix({scalingForMapMode.X},0,0,{scalingForMapMode.Y},0,0)";

        svgSb.Append($"<path transform=\"{scaleMatrix}\" d=\"");
    }

    public static void HandleEndPath(StringBuilder svgSb, EmfState state)
    {
        state.InPath = false;
        svgSb.Append("\" ");

        Utils.AppendFill(svgSb, state);
        Utils.AppendStroke(svgSb, state);

        svgSb.AppendLine(" />");
    }

    public static void HandleCloseFigure(StringBuilder svgSb, EmfState state)
    {
        svgSb.Append("Z ");
    }
}