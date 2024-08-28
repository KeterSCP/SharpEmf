using System.Text;
using SharpEmf.Records.Control.Header;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfControlRecordsHandlers
{
    public static void HandleHeaderRecord(StringBuilder svgSb, EmfState state, EmfMetafileHeader header)
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
}