using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <summary>
/// Specifies a polygon consisting of two or more vertexes connected by straight lines
/// </summary>
[PublicAPI]
public record EmrPolygon : EnhancedMetafileRecord, IEmfParsable<EmrPolygon>
{
    public override EmfRecordType Type => EmfRecordType.EMR_POLYGON;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the APoints array
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Specifies the vertexes of the polygon in logical units
    /// </summary>
    public IReadOnlyList<PointL> APoints { get; }

    private EmrPolygon(uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints)
    {
        Size = size;
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolygon Parse(Stream stream, uint size)
    {
        var bounds = new RectL(
            left: stream.ReadInt32(),
            top: stream.ReadInt32(),
            right: stream.ReadInt32(),
            bottom: stream.ReadInt32());

        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();
        var points = new PointL[checked((int)count)];

        for (var i = 0; i < count; i++)
        {
            points[i] = new PointL(
                x: stream.ReadInt32(),
                y: stream.ReadInt32());
        }

        return new EmrPolygon(size, bounds, count, points);
    }
}