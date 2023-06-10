using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYGON"/>
[PublicAPI]
public record EmrPolygon : EnhancedMetafileRecord, IEmfParsable<EmrPolygon>
{
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

    private EmrPolygon(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolygon Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);

        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();
        var points = new PointL[(int)count];

        for (var i = 0; i < count; i++)
        {
            points[i] = PointL.Parse(stream);
        }

        return new EmrPolygon(recordType, size, bounds, count, points);
    }
}