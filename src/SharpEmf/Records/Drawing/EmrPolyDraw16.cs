using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYDRAW16"/>
[PublicAPI]
public record EmrPolyDraw16 : EnhancedMetafileRecord, IEmfParsable<EmrPolyDraw16>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the <see cref="APoints"/> property
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Points in logical units
    /// </summary>
    public IReadOnlyList<PointS> APoints { get; }

    /// <summary>
    /// A <see cref="Count"/> length collection of byte values that specifies how each point in the <see cref="APoints"/> collection is used
    /// </summary>
    public IReadOnlyList<Point> ABTypes { get; }

    private EmrPolyDraw16(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        uint count,
        IReadOnlyList<PointS> aPoints,
        IReadOnlyList<Point> abTypes) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
        ABTypes = abTypes;
    }

    public static EmrPolyDraw16 Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);

        var count = stream.ReadUInt32();
        var points = new PointS[(int)count];
        var abTypes = new Point[(int)count];

        for (var i = 0; i < count; i++)
        {
            points[i] = PointS.Parse(stream);
        }

        for (var i = 0; i < count; i++)
        {
            abTypes[i] = stream.ReadEnum<Point>();
        }

        return new EmrPolyDraw16(recordType, size, bounds, count, points, abTypes);
    }
}