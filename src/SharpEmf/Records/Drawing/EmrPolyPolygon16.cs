using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYPOLYGON16"/>
[PublicAPI]
public record EmrPolyPolygon16 : EnhancedMetafileRecord, IEmfParsable<EmrPolyPolygon16>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of polygons
    /// </summary>
    public uint NumberOfPolygons { get; }

    /// <summary>
    /// Specifies the total number of points in all polygons
    /// </summary>
    /// <remarks>
    /// Any extra points MUST be ignored. To draw a line with more points, the data SHOULD be divided
    /// into groups that have less than the maximum number of points, and an EMR_POLYPOLYGON
    /// operation SHOULD be performed for each group of point
    /// </remarks>
    public uint Count { get; }

    /// <summary>
    /// Specifies the point count for each polygon
    /// </summary>
    public IReadOnlyList<uint> PolygonPointCount { get; }

    /// <summary>
    /// Specifies the points for all polygons in logical units
    /// </summary>
    /// <remarks>
    /// The number of points is specified by the <see cref="Count"/> field value
    /// </remarks>
    public IReadOnlyList<PointS> APoints { get; }

    private EmrPolyPolygon16(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        uint numberOfPolygons,
        uint count,
        IReadOnlyList<uint> polygonPointCount,
        IReadOnlyList<PointS> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        NumberOfPolygons = numberOfPolygons;
        Count = count;
        PolygonPointCount = polygonPointCount;
        APoints = aPoints;
    }

    public static EmrPolyPolygon16 Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var numberOfPolygons = stream.ReadUInt32();
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();

        var polygonPointCount = new uint[(int)numberOfPolygons];
        for (var i = 0; i < numberOfPolygons; i++)
        {
            polygonPointCount[i] = stream.ReadUInt32();
        }

        var points = new PointS[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointS.Parse(stream);
        }

        return new EmrPolyPolygon16(recordType, size, bounds, numberOfPolygons, count, polygonPointCount, points);
    }
}