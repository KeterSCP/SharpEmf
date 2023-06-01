using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYDRAW"/>
[PublicAPI]
public record EmrPolyDraw : EnhancedMetafileRecord, IEmfParsable<EmrPolyDraw>
{
    public override EmfRecordType Type => EmfRecordType.EMR_POLYDRAW;
    public override uint Size { get; }

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
    public IReadOnlyList<PointL> APoints { get; }

    /// <summary>
    /// A <see cref="Count"/> length collection of byte values that specifies how each point in the <see cref="APoints"/> collection is used
    /// </summary>
    public IReadOnlyList<Point> ABPoints { get; }

    private EmrPolyDraw(uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints, IReadOnlyList<Point> abPoints)
    {
        Size = size;
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
        ABPoints = abPoints;
    }

    public static EmrPolyDraw Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);

        var count = stream.ReadUInt32();
        var points = new PointL[(int)count];
        var abPoints = new Point[(int)count];

        for (var i = 0; i < count; i++)
        {
            points[i] = PointL.Parse(stream);
        }

        for (var i = 0; i < count; i++)
        {
            abPoints[i] = stream.ReadEnum<Point>();
        }

        return new EmrPolyDraw(size, bounds, count, points, abPoints);
    }
}