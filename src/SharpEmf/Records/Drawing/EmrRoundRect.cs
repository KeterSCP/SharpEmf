using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_ROUNDRECT"/>
[PublicAPI]
public record EmrRoundRect : EnhancedMetafileRecord, IEmfParsable<EmrRoundRect>
{
    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    /// <summary>
    /// Specifies the width and height, in logical coordinates, of the ellipse used to draw the rounded corners
    /// </summary>
    public PointL Corner { get; }

    private EmrRoundRect(EmfRecordType recordType, uint size, RectL box, PointL corner) : base(recordType, size)
    {
        Box = box;
        Corner = corner;
    }

    public static EmrRoundRect Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var box = RectL.Parse(stream);
        var corner = PointL.Parse(stream);

        return new EmrRoundRect(recordType, size, box, corner);
    }
}