using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_ARCTO"/>
[PublicAPI]
public record EmrArcTo : EnhancedMetafileRecord, IEmfParsable<EmrArcTo>
{
    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    /// <summary>
    /// Specifies the coordinates, in logical units, of the first radial ending point, in logical units
    /// </summary>
    public PointL Start { get; }

    /// <summary>
    /// Specifies the coordinates, in logical units, of the second radial ending point, in logical units
    /// </summary>
    public PointL End { get; }

    private EmrArcTo(EmfRecordType recordType, uint size, RectL box, PointL start, PointL end) : base(recordType, size)
    {
        Box = box;
        Start = start;
        End = end;
    }

    public static EmrArcTo Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var box = RectL.Parse(stream);
        var start = PointL.Parse(stream);
        var end = PointL.Parse(stream);

        return new EmrArcTo(recordType, size, box, start, end);
    }
}