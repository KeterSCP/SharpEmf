using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_PIE"/>
[PublicAPI]
public record EmrPie : EnhancedMetafileRecord, IEmfParsable<EmrPie>
{
    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    /// <summary>
    /// Specifies the coordinates, in logical units, of the endpoint of the first radial
    /// </summary>
    public PointL Start { get; }

    /// <summary>
    /// Specifies the coordinates, in logical units, of the endpoint of the second radial
    /// </summary>
    public PointL End { get; }

    private EmrPie(EmfRecordType recordType, uint size, RectL box, PointL start, PointL end) : base(recordType, size)
    {
        Box = box;
        Start = start;
        End = end;
    }

    public static EmrPie Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var box = RectL.Parse(stream);
        var start = PointL.Parse(stream);
        var end = PointL.Parse(stream);

        return new EmrPie(recordType, size, box, start, end);
    }
}