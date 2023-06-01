using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_ARC"/>
[PublicAPI]
public record EmrArc : EnhancedMetafileRecord, IEmfParsable<EmrArc>
{
    public override EmfRecordType Type => EmfRecordType.EMR_ARC;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    /// <summary>
    ///  Specifies the coordinates in logical units of the ending point of the radial line defining the starting point of the arc
    /// </summary>
    public PointL Start { get; }

    /// <summary>
    /// Specifies the coordinates in logical units of the ending point of the radial line defining the ending point of the arc
    /// </summary>
    public PointL End { get; }

    private EmrArc(uint size, RectL box, PointL start, PointL end)
    {
        Size = size;
        Box = box;
        Start = start;
        End = end;
    }

    public static EmrArc Parse(Stream stream, uint size)
    {
        var box = RectL.Parse(stream);
        var start = PointL.Parse(stream);
        var end = PointL.Parse(stream);

        return new EmrArc(size, box, start, end);
    }
}