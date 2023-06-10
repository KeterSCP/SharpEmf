using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYPOLYLINE"/>
[PublicAPI]
public record EmrPolyPolyLine : EnhancedMetafileRecord, IEmfParsable<EmrPolyPolyLine>
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
    public IReadOnlyList<PointL> APoints { get; }

    private EmrPolyPolyLine(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        uint numberOfPolylines,
        uint count,
        IReadOnlyList<uint> aPolylinePointCount,
        IReadOnlyList<PointL> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        NumberOfPolylines = numberOfPolylines;
        Count = count;
        APolylinePointCount = aPolylinePointCount;
        APoints = aPoints;
    }

    public static EmrPolyPolyLine Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var numberOfPolylines = stream.ReadUInt32();
        var count = stream.ReadUInt32();

        var polylinePointCount = new uint[(int)numberOfPolylines];
        for (var i = 0; i < numberOfPolylines; i++)
        {
            var polylinePointCountValue = stream.ReadUInt32();
            if (polylinePointCountValue < 0x00000002)
            {
                throw new EmfParseException($"Each value in {nameof(APolylinePointCount)} MUST be >= 0x00000002");
            }
            polylinePointCount[i] = polylinePointCountValue;
        }

        var points = new PointL[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointL.Parse(stream);
        }

        return new EmrPolyPolyLine(recordType, size, bounds, numberOfPolylines, count, polylinePointCount, points);
    }
}