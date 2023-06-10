using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYBEZIER"/>
[PublicAPI]
public record EmrPolyBezier : EnhancedMetafileRecord, IEmfParsable<EmrPolyBezier>
{
    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    /// <remarks>
    /// This value MUST be one more than three times the number of curves to be drawn because each Bezier
    /// curve requires two control points and an endpoint, and the initial curve requires an additional starting point
    /// </remarks>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the <see cref="APoints"/> array
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Specifies the endpoints and control points of the Bezier curves in logical units
    /// </summary>
    public IReadOnlyList<PointL> APoints { get; }

    private EmrPolyBezier(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointL> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyBezier Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();
        var aPoints = new PointL[count];
        for (var i = 0; i < count; i++)
        {
            aPoints[i] = PointL.Parse(stream);
        }

        return new EmrPolyBezier(recordType, size, bounds, count, aPoints);
    }
}