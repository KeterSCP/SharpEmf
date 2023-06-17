using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_EXTFLOODFILL"/>
[PublicAPI]
public record EmrExtFloodFill : EnhancedMetafileRecord, IEmfParsable<EmrExtFloodFill>
{
    /// <summary>
    /// Specifies the coordinates, in logical units, where filling begins
    /// </summary>
    public PointL Start { get; }

    /// <summary>
    /// Used with the <see cref="FloodFillMode"/> to determine the area to fill
    /// </summary>
    public ColorRef Color { get; }

    /// <summary>
    /// Specifies how to use the <see cref="Color"/> value to determine the area for the flood fill operation
    /// </summary>
    public FloodFillMode FloodFillMode { get; }

    private EmrExtFloodFill(
        EmfRecordType recordType,
        uint size,
        PointL start,
        ColorRef color,
        FloodFillMode floodFillMode) : base(recordType, size)
    {
        Start = start;
        Color = color;
        FloodFillMode = floodFillMode;
    }

    public static EmrExtFloodFill Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var start = PointL.Parse(stream);
        var color = ColorRef.Parse(stream);
        var floodFillMode = stream.ReadEnum<FloodFillMode>();

        return new EmrExtFloodFill(recordType, size, start, color, floodFillMode);
    }
}