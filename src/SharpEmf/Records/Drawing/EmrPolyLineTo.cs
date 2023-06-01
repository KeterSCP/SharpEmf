using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYLINETO"/>
[PublicAPI]
public record EmrPolyLineTo : EnhancedMetafileRecord, IEmfParsable<EmrPolyLineTo>
{
    public override EmfRecordType Type => EmfRecordType.EMR_POLYLINETO;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the <see cref="APoints"/> array
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Specifies the point data, in logical units
    /// </summary>
    public IReadOnlyList<PointL> APoints { get; }

    private EmrPolyLineTo(uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints)
    {
        Size = size;
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyLineTo Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();

        var points = new PointL[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointL.Parse(stream);
        }

        return new EmrPolyLineTo(size, bounds, count, points);
    }
}