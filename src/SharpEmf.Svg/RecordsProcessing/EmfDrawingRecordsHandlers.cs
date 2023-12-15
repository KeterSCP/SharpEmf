using System.Text;
using SharpEmf.Records.Drawing;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfDrawingRecordsHandlers
{
    public static void HandlePolyPolygon16(StringBuilder svgSb, EmfState state, EmrPolyPolygon16 polyPolygon16)
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

        Utils.AppendFill(svgSb, state);
        Utils.AppendStroke(svgSb, state);

        svgSb.AppendLine(" />");
    }

    public static void HandlePolybezierTo16(StringBuilder svgSb, EmfState state, EmrPolyBezierTo16 polyBezierTo16)
    {
        if (!state.InPath)
        {
            return;
        }

        var currentPointCounter = 0;

        foreach (var point in polyBezierTo16.APoints)
        {
            if (currentPointCounter % 3 == 0)
            {
                svgSb.Append("C ");
            }
            svgSb.Append($"{point.X} {point.Y} ");

            currentPointCounter++;
        }
    }
}