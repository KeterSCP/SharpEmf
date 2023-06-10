using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYBEZIER16"/>
[PublicAPI]
public record EmrPolyBezier16 : EnhancedMetafileRecord, IEmfParsable<EmrPolyBezier16>
{
    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the <see cref="APoints"/> array
    /// </summary>
    /// <remarks>
    /// This value MUST be one more than three times the number of curves to be drawn because each Bezier
    /// curve requires two control points and an endpoint, and the initial curve requires an additional starting point
    /// </remarks>
    public uint Count { get; }

    /// <summary>
    /// Specifies the endpoints and control points of the Bezier curves in logical units
    /// </summary>
    public IReadOnlyList<PointS> APoints { get; }

    private EmrPolyBezier16(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointS> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyBezier16 Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();
        var aPoints = new PointS[count];
        for (var i = 0; i < count; i++)
        {
            aPoints[i] = PointS.Parse(stream);
        }

        return new EmrPolyBezier16(recordType, size, bounds, count, aPoints);
    }
}