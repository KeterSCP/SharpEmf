using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYBEZIERTO"/>
[PublicAPI]
public record EmrPolyBezierTo : EnhancedMetafileRecord, IEmfParsable<EmrPolyBezierTo>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the <see cref="APoints"/> array
    /// </summary>
    /// <remarks>
    /// The first curve MUST be drawn from the current position to the third point by using the first two points
    /// as control points. For each subsequent curve, exactly three more points MUST be specified, and
    /// the ending point of the previous curve MUST be used as the starting point for the next
    /// </remarks>
    public uint Count { get; }

    /// <summary>
    /// Specifies the endpoints and control points of the Bezier curves in logical units
    /// </summary>
    public IReadOnlyList<PointL> APoints { get; }

    private EmrPolyBezierTo(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyBezierTo Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();

        var points = new PointL[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointL.Parse(stream);
        }

        return new EmrPolyBezierTo(recordType, size, bounds, count, points);
    }
}