using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYLINE16"/>
[PublicAPI]
public record EmrPolyline16 : EnhancedMetafileRecord, IEmfParsable<EmrPolyline16>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the total number of points in <see cref="APoints"/> array
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// A <see cref="Count"/> length array, which specifies the array of points
    /// </summary>
    public IReadOnlyList<PointS> APoints { get; }

    private EmrPolyline16(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointS> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyline16 Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var count = stream.ReadUInt32();
        var points = new PointS[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointS.Parse(stream);
        }

        return new EmrPolyline16(recordType, size, bounds, count, points);
    }
}