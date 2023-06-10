using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYPOLYLINE16"/>
[PublicAPI]
public record EmrPolyPolyline16 : EnhancedMetafileRecord, IEmfParsable<EmrPolyPolyline16>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of polylines, which is the number of elements in the <see cref="APolylinePointCount"/> array
    /// </summary>
    public uint NumberOfPolylines { get; }

    /// <summary>
    /// Specifies the total number of points in all polylines, which is the number of elements in the <see cref="APoints"/> array
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// A <see cref="NumberOfPolylines"/> length array of values that specify the point counts for all polylines
    /// </summary>
    /// <remarks>
    /// Each value MUST be >= 0x00000002
    /// </remarks>
    public IReadOnlyList<uint> APolylinePointCount { get; }

    /// <summary>
    /// A <see cref="Count"/> length array of objects that specify the point data, in logical units
    /// </summary>
    public IReadOnlyList<PointS> APoints { get; }

    private EmrPolyPolyline16(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        uint numberOfPolylines,
        uint count,
        IReadOnlyList<uint> aPolylinePointCount,
        IReadOnlyList<PointS> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        NumberOfPolylines = numberOfPolylines;
        Count = count;
        APolylinePointCount = aPolylinePointCount;
        APoints = aPoints;
    }

    public static EmrPolyPolyline16 Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var numberOfPolylines = stream.ReadUInt32();
        var count = stream.ReadUInt32();

        var aPolylinePointCount = new List<uint>();
        for (var i = 0; i < numberOfPolylines; i++)
        {
            aPolylinePointCount.Add(stream.ReadUInt32());
        }

        var aPoints = new List<PointS>();
        for (var i = 0; i < count; i++)
        {
            aPoints.Add(PointS.Parse(stream));
        }

        return new EmrPolyPolyline16(
            recordType,
            size,
            bounds,
            numberOfPolylines,
            count,
            aPolylinePointCount,
            aPoints
        );
    }
}