using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYBEZIERTO16"/>
[PublicAPI]
public record EmrPolyBezierTo16 : EnhancedMetafileRecord, IEmfParsable<EmrPolyBezierTo16>
{
    public override EmfRecordType Type => EmfRecordType.EMR_POLYBEZIERTO16;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the total number of points in the <see cref="APoints"/> array
    /// <para />
    /// The first curve is drawn from the current position to the third point by using the first two points as control points.
    /// For each subsequent curve, three more points MUST be specified, and the ending point of the previous curve MUST be used as the starting point for the next
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Specifies the points of the Bezier curves in logical units
    /// </summary>
    public IReadOnlyList<PointS> APoints { get; }

    private EmrPolyBezierTo16(uint size, RectL bounds, uint count, IReadOnlyList<PointS> aPoints)
    {
        Size = size;
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolyBezierTo16 Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);
        var count = stream.ReadUInt32();
        var aPoints = new PointS[count];
        for (var i = 0; i < count; i++)
        {
            aPoints[i] = PointS.Parse(stream);
        }

        return new EmrPolyBezierTo16(size, bounds, count, aPoints);
    }
}