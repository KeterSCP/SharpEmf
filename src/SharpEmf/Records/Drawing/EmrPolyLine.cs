using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYLINE"/>
[PublicAPI]
public record EmrPolyLine : EnhancedMetafileRecord, IEmfParsable<EmrPolyLine>
{
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

    private EmrPolyLine(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyLine Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();

        var points = new PointL[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointL.Parse(stream);
        }

        return new EmrPolyLine(recordType, size, bounds, count, points);
    }
}